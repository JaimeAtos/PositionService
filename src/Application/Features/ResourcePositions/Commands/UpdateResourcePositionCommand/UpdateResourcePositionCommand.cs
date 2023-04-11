using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;

public class UpdateResourcePositionCommand : IRequest<Response<Guid>>
{
    public Guid ResourceId { get; set; }
    public Guid PositionId { get; set; }
    public byte PercentMathPosition { get; set; }
    public bool IsDefault { get; set; }
    public string ResourceName { get; set; }
}

public class UpdateResourcePositionCommandHandler : IRequestHandler<UpdateResourcePositionCommand, Response<Guid>>
{
    private readonly IResourcePositionRepository _resourcePositionRepository;
    private readonly IMapper _mapper;

    public UpdateResourcePositionCommandHandler(IResourcePositionRepository resourcePositionRepository, IMapper mapper)
    {
        _resourcePositionRepository = resourcePositionRepository;
        _mapper = mapper;
    }

    public Task<Response<Guid>> Handle(UpdateResourcePositionCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private async Task<Response<Guid>> ProcessHandler(UpdateResourcePositionCommand request, CancellationToken cancellationToken = default)
    {
        var newRecord = _mapper.Map<ResourcePosition>(request);
        var data = await _resourcePositionRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);

        return new Response<Guid>(data);
    }
}