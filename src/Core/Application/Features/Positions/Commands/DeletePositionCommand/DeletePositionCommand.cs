using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.DeletePositionCommand;

public class DeletePositionCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Wrappers.Response<bool>>
{
	private readonly IPositionRepository _positionRepository;
	private readonly IPublisherCommands<PositionDeleted> _publisher;

	public DeletePositionCommandHandler(IPositionRepository positionRepository,
		IPublisherCommands<PositionDeleted> publisher)
	{
		_positionRepository = positionRepository;
		_publisher = publisher;
	}


	public Task<Wrappers.Response<bool>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<bool>> ProcessHandle(DeletePositionCommand request,
		CancellationToken cancellationToken)
	{
		var position = await _positionRepository.GetEntityByIdAsync(request.Id, cancellationToken);

		if (position is null) throw new ApiException($"Position with id {request.Id} not found");

		var state = await _positionRepository.DeleteAsync(position.Id, cancellationToken);

		await _publisher.PublishEntityMessage(request.ToPositionDeleted(), "position.deleted", request.Id,
			cancellationToken);

		return new Wrappers.Response<bool>(state);
	}
}