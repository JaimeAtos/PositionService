using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;

public class DeletePositionSkillCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePositionSkillCommandHandler : IRequestHandler<DeletePositionSkillCommand, Wrappers.Response<bool>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IPublishEndpoint _publishEndpoint;

	public DeletePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository,
		IPublishEndpoint publishEndpoint)
	{
		_positionSkillRepository = positionSkillRepository;
		_publishEndpoint = publishEndpoint;
	}

	public Task<Wrappers.Response<bool>> Handle(DeletePositionSkillCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new NotImplementedException();
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Wrappers.Response<bool>> ProcessHandler(DeletePositionSkillCommand request,
		CancellationToken cancellationToken)
	{
		var deleteRecord = await _positionSkillRepository.GetEntityByIdAsync(request.Id, cancellationToken);
		if (deleteRecord is null)
			throw new ApiException($"Position with id {request.Id} not found");

		var state = await _positionSkillRepository.DeleteAsync(deleteRecord.Id, cancellationToken);

		await PublishPositionSkillDeleted(request.ToPositionSkillDeleted(), cancellationToken);
		return new Wrappers.Response<bool>(state);
	}

	private async Task PublishPositionSkillDeleted(PositionSkillDeleted positionSkill,
		CancellationToken cancellationToken)
	{
		await _publishEndpoint.Publish(
			positionSkill,
			ctx =>
			{
				ctx.MessageId = positionSkill.Id;
				ctx.SetRoutingKey("positionSkill.deleted");
			},
			cancellationToken);
	}
}
