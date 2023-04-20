using Application.Exceptions;
using Application.Wrappers;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.DeletePositionCommand;

public class DeletePositionCommandById : IRequest<Response<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommandById, Response<bool>>
{
	private readonly IPositionRepository _positionRepository;

	public DeletePositionCommandHandler(IPositionRepository positionRepository)
	{
		_positionRepository = positionRepository;
	}

	public Task<Response<bool>> Handle(DeletePositionCommandById request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Response<bool>> ProcessHandle(DeletePositionCommandById request, CancellationToken cancellationToken)
	{
		var position = await _positionRepository.GetEntityByIdAsync(request.Id, cancellationToken);

		if (position is null) throw new ApiException($"Position with id {request.Id} not found");

		position.State = false;

		var state = await _positionRepository.DeleteAsync(position.Id, cancellationToken);
		return new Response<bool>(state);
	}
}
