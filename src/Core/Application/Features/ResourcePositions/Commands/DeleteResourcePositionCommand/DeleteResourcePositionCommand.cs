using Application.Exceptions;
using Application.Wrappers;
using Domain.Repositories;
using MediatR;

namespace Application.Features.ResourcePositions.Commands.DeleteResourcePositionCommand;
public class DeleteResourcePositionCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}

public class DeleteResourcePositionCommandHandler : IRequestHandler<DeleteResourcePositionCommand, Response<bool>>
{
    private readonly IResourcePositionRepository _resourcePositionRepository;

    public DeleteResourcePositionCommandHandler (IResourcePositionRepository resourcePositionRepository)
    {
        _resourcePositionRepository = resourcePositionRepository;
    }

    public Task<Response<bool>> Handle(DeleteResourcePositionCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ApiException();

        return ProcessHandle(request, cancellationToken);
    }

    private async Task<Response<bool>> ProcessHandle(DeleteResourcePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _resourcePositionRepository.GetEntityByIdAsync(request.Id, cancellationToken);

        if(position is null)
            throw new ApiException($" Entity with {request.Id} not found");

        var state = await _resourcePositionRepository.DeleteAsync(position.Id, cancellationToken);
        return new Response<bool>(state);
    }

}