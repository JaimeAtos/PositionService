using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Queries.GetAllPositionSkill;

public class GetAllPositionSkillQuery : IRequest<PagedResponse<List<PositionSkillDto>>>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public Guid Id { get; set; }
	public bool State { get; set; }
	public Guid SkillId { get; set; }
	public Guid PositionId { get; set; }
	public string SkillName { get; set; }
	public byte? MinToAccept { get; set; }
	public byte PositionSkillType { get; set; }
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
			throw new ArgumentNullException();

		return ProcessHandle(request, cancellationToken);
	}

	public async Task<PagedResponse<List<PositionSkillDto>>> ProcessHandle(GetAllPositionSkillQuery request,
		CancellationToken cancellationToken = default)
	{
		var record = await _positionSkillRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
		var recordDto = _mapper.Map<List<PositionSkillDto>>(record)
			.Where(ps => ps.State)
			.ToList();

		return new PagedResponse<List<PositionSkillDto>>(recordDto, request.PageNumber, request.PageSize);
	}
}