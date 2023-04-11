using Application.Exceptions;
using Application.Wrappers;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;

public class DeletePositionSkillCommand : IRequest<Response<Guid>>
{
	public Guid Id { get; set; }
}

public class DeletePositionSkillCommandHandler : IRequestHandler<DeletePositionSkillCommand, Response<Guid>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;

	public DeletePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository)
	{
		_positionSkillRepository = positionSkillRepository;
	}

	public Task<Response<Guid>> Handle(DeletePositionSkillCommand request, CancellationToken cancellationToken = default)
	{
		if (request is null)
			throw new NotImplementedException();
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Response<Guid>> ProcessHandler(DeletePositionSkillCommand request, CancellationToken cancellationToken = default)
	{
		var deleteRecord = await _positionSkillRepository.GetEntityByIdAsync(request.Id, cancellationToken);
		if (deleteRecord is null) throw new ApiException($"Position with id {request.Id} not found");

		deleteRecord.State = false;
		var state = await _positionSkillRepository.UpdateAsync(deleteRecord, deleteRecord.Id, cancellationToken);
		return new Response<Guid>(state);
	}
}
