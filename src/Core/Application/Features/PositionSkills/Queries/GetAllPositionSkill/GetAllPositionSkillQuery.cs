using Application.DTOs;
using Application.Exceptions;
using Application.Extensions;
using Application.Extensions.Queries;
using Application.Wrappers;
using AutoMapper;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Queries.GetAllPositionSkill;

public class GetAllPositionSkillQuery : IRequest<PagedResponse<List<PositionSkillDto>>>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public Guid Id { get; set; }
	public bool State { get; set; }
	public Guid? SkillId { get; set; }
	public Guid? PositionId { get; set; }
	public string? SkillName { get; set; }
	public byte? MinToAccept { get; set; }
	public PositionSkillType? PositionSkillType { get; set; }
}

public class
	GetAllPositionSkillQueryHandle : IRequestHandler<GetAllPositionSkillQuery, PagedResponse<List<PositionSkillDto>>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IMapper _mapper;

	public GetAllPositionSkillQueryHandle(IPositionSkillRepository positionSkillRepository, IMapper mapper)
	{
		_positionSkillRepository = positionSkillRepository;
		_mapper = mapper;
	}

	public Task<PagedResponse<List<PositionSkillDto>>> Handle(GetAllPositionSkillQuery request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<PagedResponse<List<PositionSkillDto>>> ProcessHandle(GetAllPositionSkillQuery request,
		CancellationToken cancellationToken = default)
	{
		var record = await _positionSkillRepository.GetAllAsync(request.PageNumber, request.PageSize, request.GenerateParams(), cancellationToken);
		var recordDto = _mapper.Map<List<PositionSkillDto>>(record);

		return new PagedResponse<List<PositionSkillDto>>(recordDto, request.PageNumber, request.PageSize);
	}
}
