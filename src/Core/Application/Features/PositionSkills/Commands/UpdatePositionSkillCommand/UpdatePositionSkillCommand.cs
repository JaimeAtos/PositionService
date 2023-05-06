using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;

public class UpdatePositionSkillCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
	public Guid SkillId { get; set; }
	public Guid PositionId { get; set; }
	public string? SkillName { get; set; }
	public byte MinToAccept { get; set; }
	public PositionSkillType PositionSkillType { get; set; }
}

public class UpdatePositionSkillCommandHandler : IRequestHandler<UpdatePositionSkillCommand, Wrappers.Response<bool>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IMapper _mapper;


	public UpdatePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository, IMapper mapper)
	{
		_positionSkillRepository = positionSkillRepository;
		_mapper = mapper;
	}

	public Task<Wrappers.Response<bool>> Handle(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("request is empty");
		return ProcessHandler(request, cancellationToken);
	}

	private async Task<Wrappers.Response<bool>> ProcessHandler(UpdatePositionSkillCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<PositionSkill>(request);
		var data = await _positionSkillRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);
		return new Wrappers.Response<bool>(data);
	}
}
