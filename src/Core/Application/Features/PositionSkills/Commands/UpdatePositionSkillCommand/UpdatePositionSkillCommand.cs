using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;

public class UpdatePositionSkillCommand : IRequest<Response<bool>>
{
	public Guid SkillId { get; set; }
	public Guid PositionId { get; set; }
	public string? SkillName { get; set; }
	public byte? MinToAccept { get; set; }
	public PositionSkillType PositionSkillType { get; set; }
}

public class UpdatePositionSkillCommandHandler : IRequestHandler<UpdatePositionSkillCommand, Response<bool>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IMapper _mapper;

	public UpdatePositionSkillCommandHandler(IPositionSkillRepository repository, IMapper mapper)
	{
		_positionSkillRepository = repository;
		_mapper = mapper;
	}

	public Task<Response<bool>> Handle(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("request is empty");
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Response<bool>> ProcessHandler(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<PositionSkill>(request);
		var data = await _positionSkillRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);
		return new Response<bool>(data);
	}
}
