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
    public string? Description { get; set; }
    public Guid ClientId { get; set; }
    public string? ClientDescription { get; set; }
    public string? PositionLevel { get; set; }
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

	public Task<PagedResponse<List<PositionDto>>> Handle(GetAllPositionQuery request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");
		return ProcessHandle(request, cancellationToken);
	}

	private async Task<PagedResponse<List<PositionDto>>> ProcessHandle(GetAllPositionQuery request,
		CancellationToken cancellationToken = default)
	{
		var filters = GenerateParams(request);
		var record =
			await _positionRepository.GetAllAsync(request.PageNumber, request.PageSize, filters, cancellationToken);
		var recordDto = _mapper.Map<List<PositionDto>>(record);

		return new PagedResponse<List<PositionDto>>(recordDto, request.PageNumber, request.PageSize);
	}
	
	private Dictionary<string, object> GenerateParams(GetAllPositionQuery queryFields)
	{
		var filters = new Dictionary<string, object>();
		
		if (queryFields.PositionLevel is not null)
			filters.Add("PositionLevel", queryFields.PositionLevel);
		
		if (queryFields.Description is not null)
			filters.Add("Description", queryFields.Description);
		
		if (queryFields.ClientDescription is not null)
			filters.Add("ClientDescription", queryFields.ClientDescription);

		return filters;
	}

}
