using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.CreatePositionCommand;

public class CreatePositionCommand : IRequest<Response<Guid>>
{
    public string Description { get; set; }
    public Guid ClientId { get; set; }
    public string ClientDescription { get; set; }
    public string PositionLevel { get; set; }
}

public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Response<Guid>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public CreatePositionCommandHandler(IPositionRepository questionRepository, IMapper mapper)
    {
        _positionRepository = questionRepository;
        _mapper = mapper;
    }

    public Task<Response<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException();

        return ProcessHandle(request, cancellationToken);
    }

    private async Task<Response<Guid>> ProcessHandle(CreatePositionCommand request, CancellationToken cancellationToken = default)
    {
        var newRecord = _mapper.Map<Position>(request);
        var data = await _positionRepository.CreateAsync(newRecord, cancellationToken);

        return new Response<Guid>(data);
    }
}