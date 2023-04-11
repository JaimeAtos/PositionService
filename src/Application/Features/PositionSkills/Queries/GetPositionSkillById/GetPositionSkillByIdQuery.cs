using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Queries.GetPositionSkillById;

public class GetPositionSkillByIdQuery : IRequest<Response<PositionSkillDto>>
{
    public Guid Id { get; set; }
}
public class GetPositionSkillByIdQueryHandler : IRequestHandler<GetPositionSkillByIdQuery, Response<PositionSkillDto>>
{
    private readonly IPositionSkillRepository _positionSkillRepository;
    private readonly IMapper _mapper;

    public GetPositionSkillByIdQueryHandler(IMapper mapper, IPositionSkillRepository positionSkillRepository)
    {
        _mapper=mapper;
        _positionSkillRepository=positionSkillRepository;
    }

    public Task<Response<PositionSkillDto>> Handle(GetPositionSkillByIdQuery request, CancellationToken cancellationToken)
    {
        
        if (request == null)
            throw new ArgumentNullException();
        return ProcessHandle(request, cancellationToken);
    }

    public async Task<Response<PositionSkillDto>> ProcessHandle(GetPositionSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var positionSkill = await _positionSkillRepository.GetEntityByIdAsync(request.Id, cancellationToken);
        if (positionSkill == null)
            throw new KeyNotFoundException($"Record with id {request.Id} not found");
        else
        {
            var dto = _mapper.Map<PositionSkillDto>(positionSkill);
            return new Response<PositionSkillDto>(dto);
        }
    }
}