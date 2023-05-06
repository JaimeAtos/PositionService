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

	public DeletePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository)
	{
		_positionSkillRepository = positionSkillRepository;
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

		return new Wrappers.Response<bool>(state);
	}
}
