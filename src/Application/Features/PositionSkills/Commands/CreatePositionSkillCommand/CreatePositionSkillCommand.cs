using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;

public class CreatePositionSkillCommand : IRequest<Response<Guid>>
{
        public Guid SkillId { get; set; }
        public Guid PositionId { get; set; }
        public string SkillName { get; set; }
        public byte? MinToAccept { get; set; }
        public byte PositionSkillType { get; set; }

}

public class CreatePositionSkillCommandHandler : IRequestHandler<CreatePositionSkillCommand, Response<Guid>>
{
    private readonly IPositionSkillRepository _positionSkillRepository;
    private readonly IMapper _mapper;

    public CreatePositionSkillCommandHandler(IPositionSkillRepository repository, IMapper mapper)
    {
        _positionSkillRepository = repository;
        _mapper = mapper;
    }

    public Task<Response<Guid>> Handle(CreatePositionSkillCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException();
        
        return ProcessHandle(request, cancellationToken);
    }

    private async Task<Response<Guid>> ProcessHandle(CreatePositionSkillCommand request, CancellationToken cancellationToken)
    {
        var newRecord = _mapper.Map<PositionSkill>(request);
        var data = await _positionSkillRepository.CreateAsync(newRecord, cancellationToken);

        return new Response<Guid>(data);
    }
}
