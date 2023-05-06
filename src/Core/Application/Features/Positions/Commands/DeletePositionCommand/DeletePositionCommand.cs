using Application.Exceptions;
using Application.Extensions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.Positions.Commands.DeletePositionCommand;

public class DeletePositionCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Wrappers.Response<bool>>
{
	private readonly IPositionRepository _positionRepository;

	public DeletePositionCommandHandler(IPositionRepository positionRepository)
	{
		_positionRepository = positionRepository;
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

		
		return new Wrappers.Response<bool>(state);
	}
}
