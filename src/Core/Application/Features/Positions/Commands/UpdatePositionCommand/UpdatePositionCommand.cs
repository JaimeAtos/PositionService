using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.UpdatePositionCommand;

public class UpdatePositionCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
	public string? Description { get; set; }
	public string? ClientDescription { get; set; }
	public string? CatalogLevelDescription { get; set; }
	public Guid CatalogLevelId { get; set; }
	public Guid ClientId { get; set; }
}

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Wrappers.Response<bool>>
{
	private readonly IPositionRepository _positionRepository;
	private readonly IMapper _mapper;
	private readonly IPublisherCommands<PositionUpdated> _publisher;


	public UpdatePositionCommandHandler(IPositionRepository positionRepository, IMapper mapper,
		IPublisherCommands<PositionUpdated> publisher)
	{
		_positionRepository = positionRepository;
		_mapper = mapper;
		_publisher = publisher;
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
		await _publisher.PublishEntityMessage(request.ToPositionUpdated(), "position.updated", request.Id,
			cancellationToken);
		return new Wrappers.Response<bool>(data);
	}
}