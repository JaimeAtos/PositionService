using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;

public class UpdatePositionSkillCommand : IRequest<Response<Guid>>
{
	public Guid SkillId { get; set; }
	public Guid PositionId { get; set; }
	public string SkillName { get; set; }
	public byte? MinToAccept { get; set; }
	public byte PositionSkillType { get; set; }
}

public class UpdatePositionSkillCommandHandler : IRequestHandler<UpdatePositionSkillCommand, Response<Guid>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IMapper _mapper;

	public UpdatePositionSkillCommandHandler(IPositionSkillRepository repository, IMapper mapper)
	{
		_positionSkillRepository = repository;
		_mapper = mapper;
	}

	public Task<Response<Guid>> Handle(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken = default)
	{
		if (request is null)
			throw new NotImplementedException();
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Response<Guid>> ProcessHandler(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<PositionSkill>(request);
		var data = await _positionSkillRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);
		return new Response<Guid>(data);
	}
}