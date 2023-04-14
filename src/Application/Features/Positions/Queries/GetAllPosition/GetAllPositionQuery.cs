using Application.DTOs;
using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Queries.GetALlPosition;

public class GetAllPositionQuery : IRequest<PagedResponse<List<PositionDto>>>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public Guid Id { get; set; }
	public bool State { get; set; }
    public string? Description { get; set; }
    public Guid ClientId { get; set; }
    public string? ClientDescription { get; set; }
}

public class GetALlPositionQueryHandler : IRequestHandler<GetAllPositionQuery, PagedResponse<List<PositionDto>>>
{
	private readonly IPositionRepository _positionRepository;
	private readonly IMapper _mapper;

	public GetALlPositionQueryHandler(IPositionRepository positionRepository, IMapper mapper)
	{
		_positionRepository = positionRepository;
		_mapper = mapper;
	}

	public Task<PagedResponse<List<PositionDto>>> Handle(GetAllPositionQuery request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");
		return ProcessHandle(request, cancellationToken);
	}

	private async Task<PagedResponse<List<PositionDto>>> ProcessHandle(GetAllPositionQuery request, CancellationToken cancellationToken = default)
	{
		var record = await _positionRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
		var recordDto = _mapper.Map<List<PositionDto>>(record);

		return new PagedResponse<List<PositionDto>>(recordDto, request.PageNumber, request.PageSize);
	}

}
