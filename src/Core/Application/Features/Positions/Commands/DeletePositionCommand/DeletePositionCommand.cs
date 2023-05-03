using Application.Exceptions;
using Application.Extensions;
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
	private readonly IPublishEndpoint _publishEndpoint;

	public DeletePositionCommandHandler(IPositionRepository positionRepository, IPublishEndpoint publishEndpoint)
	{
		_positionRepository = positionRepository;
		_publishEndpoint = publishEndpoint;
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

		await PublishPositionDeleted(request.ToPositionDeleted(), cancellationToken);
		
		return new Wrappers.Response<bool>(state);
	}

	private async Task PublishPositionDeleted(PositionDeleted request, CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(
			request,
			ctx =>
			{
				ctx.MessageId = request.Id;
				ctx.SetRoutingKey("position.deleted");
			},
			cancellationToken);
	}
}
