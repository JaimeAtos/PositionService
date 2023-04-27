using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;

public class CreateResourcePositionCommand : IRequest<Response<Guid>>
{
    public Guid PositionId { get; set; }
    public Guid ResourceId { get; set; }
    public byte PercentMatchPosition { get; set; }
    public bool IsDefault { get; set; }
    public string? ResourceName { get; set; }
    public string? RomaId { get; set; }
}

public class CreateResourcePositionCommandHandler : IRequestHandler<CreateResourcePositionCommand, Response<Guid>>
{
    private readonly IResourcePositionRepository _resourcePositionRepository;
    private readonly IMapper _mapper;

    public CreateResourcePositionCommandHandler(IResourcePositionRepository repository, IMapper mapper)
    {
        _resourcePositionRepository = repository;
        _mapper = mapper;
    }

    public Task<Response<Guid>> Handle(CreateResourcePositionCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ApiException("Request is empty");

        return ProcessHandle(request, cancellationToken);
    }

    private async Task<Response<Guid>> ProcessHandle(CreateResourcePositionCommand request, CancellationToken cancellationToken)
    {
        var newRecord = _mapper.Map<ResourcePosition>(request);
        var data = await _resourcePositionRepository.CreateAsync(newRecord, cancellationToken);
        return new Response<Guid>(data);
    }
}
