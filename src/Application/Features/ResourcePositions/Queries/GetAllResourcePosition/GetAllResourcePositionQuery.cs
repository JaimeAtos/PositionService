using Application.DTOs;
using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ResourcePositions.Queries.GetAllResourcePosition;

public class GetAllResourcePositionQuery : IRequest<PagedResponse<List<ResourcePositionDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Guid Id { get; set; }
    public bool State { get; set; }
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public byte PercentMathPosition { get; set; }
    public bool IsDefault { get; set; }
    public string? ResourceName { get; set; }
}

public class GetAllResourcePositionQueriesHandler : IRequestHandler<GetAllResourcePositionQuery, PagedResponse<List<ResourcePositionDto>>>
{
    private readonly IResourcePositionRepository _resourcePositionRepository;
    private readonly IMapper _mapper;

    public GetAllResourcePositionQueriesHandler(IResourcePositionRepository resourcePositionRepository, IMapper mapper)
    {
        _resourcePositionRepository = resourcePositionRepository;
        _mapper = mapper;
    }

    public Task<PagedResponse<List<ResourcePositionDto>>> Handle(GetAllResourcePositionQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ApiException("Request is empty");
        return ProcessHandle(request, cancellationToken);
    }

    private async Task<PagedResponse<List<ResourcePositionDto>>> ProcessHandle(GetAllResourcePositionQuery request, CancellationToken cancellationToken)
    {
        var record = await _resourcePositionRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        var recordDto = _mapper.Map<List<ResourcePositionDto>>(record);
        return new PagedResponse<List<ResourcePositionDto>>(recordDto, request.PageNumber, request.PageSize);
    }
}

