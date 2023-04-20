using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Positions.Commands.UpdatePositionCommand;

public class UpdatePositionCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? PositionDescription { get; set; }
    public string? PositionLevel { get; set; }
}

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<bool>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IMapper _mapper;

    public UpdatePositionCommandHandler(IPositionRepository repository, IMapper mapper)
    {
        _positionRepository = repository;
        _mapper = mapper;
    }

    public Task<Response<bool>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ApiException("Request is empty");

        return ProcessHandle(request, cancellationToken);
    }

    private async Task<Response<bool>> ProcessHandle(UpdatePositionCommand request, CancellationToken cancellationToken = default)
    {
        var newRecord = _mapper.Map<Position>(request);
        var data = await _positionRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);

        return new Response<bool>(data);
    }
}
