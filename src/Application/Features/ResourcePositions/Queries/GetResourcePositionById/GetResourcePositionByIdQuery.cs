using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ResourcePositions.Queries.GetResourcePositionById;

public class GetResourcePositionByIdQuery : IRequest<Response<ResourcePositionDto>>
{
    public Guid Id { get; set; }
}
public class GetResourcePositionByIdQueryHandler : IRequestHandler<GetResourcePositionByIdQuery, Response<ResourcePositionDto>>
{
    private readonly IResourcePositionRepository _resourcePositionRepository;
    private readonly IMapper _mapper;

    public GetResourcePositionByIdQueryHandler(IMapper mapper, IResourcePositionRepository resourcePositionRepository)
    {
        _mapper=mapper;
        _resourcePositionRepository=resourcePositionRepository;
    }

    public Task<Response<ResourcePositionDto>> Handle(GetResourcePositionByIdQuery request, CancellationToken cancellationToken)
    {
        
        if (request == null)
            throw new ArgumentNullException();
        return ProcessHandle(request, cancellationToken);
    }

    public async Task<Response<ResourcePositionDto>> ProcessHandle(GetResourcePositionByIdQuery request, CancellationToken cancellationToken)
    {
        var resourcePosition = await _resourcePositionRepository.GetEntityByIdAsync(request.Id, cancellationToken);
        if (resourcePosition == null)
            throw new KeyNotFoundException($"Record with id {request.Id} not found");
        else
        {
            var dto = _mapper.Map<ResourcePositionDto>(resourcePosition);
            return new Response<ResourcePositionDto>(dto);
        }
    }
}