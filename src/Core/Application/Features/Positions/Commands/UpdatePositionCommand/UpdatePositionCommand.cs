using Application.Exceptions;
using Application.Extensions;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.Positions.Commands.UpdatePositionCommand;

public class UpdatePositionCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
	public string? Description { get; set; }
	public string? CatalogLevelDescription { get; set; }
	public Guid CatalogLevelId { get; set; }
}

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Wrappers.Response<bool>>
{
	private readonly IPositionRepository _positionRepository;
	private readonly IMapper _mapper;
	private readonly IPublishEndpoint _publishEndpoint;

	public UpdatePositionCommandHandler(IPositionRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
	{
		_positionRepository = repository;
		_mapper = mapper;
		_publishEndpoint = publishEndpoint;
	}

	public Task<Wrappers.Response<bool>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<bool>> ProcessHandle(UpdatePositionCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<Position>(request);
		var data = await _positionRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);
		await PublishUpdatePositionCommand(request.ToPositionUpdated(), cancellationToken);
		return new Wrappers.Response<bool>(data);
	}

	private async Task PublishUpdatePositionCommand(PositionUpdated request, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(
			request,
			ctx =>
			{
				ctx.MessageId = request.Id;
				ctx.SetRoutingKey("position.updated");
			},
			cancellationToken);
	}
}
