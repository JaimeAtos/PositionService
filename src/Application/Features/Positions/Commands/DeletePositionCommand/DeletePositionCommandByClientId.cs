using Application.Exceptions;
using Application.Wrappers;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.DeletePositionCommand;

public class DeletePositionCommandByClientId : IRequest<Response<bool>>
{
	public Guid ClientId { get; set; }
}

public class DeletePositionCommandByClientIdHandler : IRequestHandler<DeletePositionCommandByClientId, Response<bool>>
{
	private readonly IPositionRepository _positionRepository;

	public DeletePositionCommandByClientIdHandler(IPositionRepository positionRepository)
	{
		_positionRepository = positionRepository;
	}

	public Task<Response<bool>> Handle(DeletePositionCommandByClientId request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Response<bool>> ProcessHandle(DeletePositionCommandByClientId request, CancellationToken cancellationToken)
	{
		var position = await _positionRepository.GetEntityByIdAsync(request.ClientId, cancellationToken);

		if (position is null) throw new ApiException($"Position with id {request.ClientId} not found");

		position.State = false;

		var state = await _positionRepository.DeleteAsync(position.Id, cancellationToken);
		return new Response<bool>(state);
	}
}
