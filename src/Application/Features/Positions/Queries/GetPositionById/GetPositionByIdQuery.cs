using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Queries.GetPositionById;

public class GetPositionByIdQuery : IRequest<Response<PositionDto>>
{
    public Guid Id { get; set; }
}
public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Response<PositionDto>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public GetPositionByIdQueryHandler(IMapper mapper, IPositionRepository positionRepository)
    {
        _mapper=mapper;
        _positionRepository=positionRepository;
    }

    public Task<Response<PositionDto>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        
        if (request == null)
            throw new ArgumentNullException();
        return ProcessHandle(request, cancellationToken);
    }

    public async Task<Response<PositionDto>> ProcessHandle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.GetEntityByIdAsync(request.Id, cancellationToken);
        if (position == null)
            throw new KeyNotFoundException($"Record with id {request.Id} not found");
        else
        {
            var dto = _mapper.Map<PositionDto>(position);
            return new Response<PositionDto>(dto);
        }
    }
}