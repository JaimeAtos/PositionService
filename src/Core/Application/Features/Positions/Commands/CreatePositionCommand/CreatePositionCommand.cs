using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.Positions.Commands.CreatePositionCommand;

public class CreatePositionCommand : IRequest<Wrappers.Response<Guid>>
{
	public string? Description { get; set; }
	public string? ClientDescription { get; set; }
	public string? CatalogLevelDescription { get; set; }
	public Guid CatalogLevelId { get; set; }
	public Guid ClientId { get; set; }
}

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Wrappers.Response<Guid>>
{
	private readonly IPositionRepository _positionRepository;
	private readonly IMapper _mapper;
	private readonly IPublishEndpoint _publishEndpoint;

	public CreatePositionCommandHandler(IPositionRepository questionRepository, IMapper mapper,
		IPublishEndpoint publishEndpoint)
	{
		_positionRepository = questionRepository;
		_mapper = mapper;
		_publishEndpoint = publishEndpoint;
	}

	public Task<Wrappers.Response<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<Guid>> ProcessHandle(CreatePositionCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<Position>(request);
		var data = await _positionRepository.CreateAsync(newRecord, cancellationToken);
		await PublishCreatePositionCommand(request.ToPositionCreated(data), cancellationToken);
		return new Wrappers.Response<Guid>(data);
	}

	private async Task PublishCreatePositionCommand(PositionCreated request, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(
			request,
			ctx =>
			{
				ctx.MessageId = request.Id;
				ctx.SetRoutingKey("position.created");
			},
			cancellationToken);
	}
}
