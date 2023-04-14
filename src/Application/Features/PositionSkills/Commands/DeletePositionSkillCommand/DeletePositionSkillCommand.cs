using Application.Exceptions;
using Application.Wrappers;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Commands.DeletePositionSkillCommand;

public class DeletePositionSkillCommand : IRequest<Response<bool>>
{
	public Guid Id { get; set; }
}

public class DeletePositionSkillCommandHandler : IRequestHandler<DeletePositionSkillCommand, Response<bool>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;

	public DeletePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository)
	{
		_positionSkillRepository = positionSkillRepository;
	}

	public Task<Response<bool>> Handle(DeletePositionSkillCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new NotImplementedException();
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Response<bool>> ProcessHandler(DeletePositionSkillCommand request, CancellationToken cancellationToken)
	{
		var deleteRecord = await _positionSkillRepository.GetEntityByIdAsync(request.Id, cancellationToken);
		if (deleteRecord is null) throw new ApiException($"Position with id {request.Id} not found");

		deleteRecord.State = false;
		var state = await _positionSkillRepository.DeleteAsync(deleteRecord.Id, cancellationToken);
		return new Response<bool>(state);
	}
}
