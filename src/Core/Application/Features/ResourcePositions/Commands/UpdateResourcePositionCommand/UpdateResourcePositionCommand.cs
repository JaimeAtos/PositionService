using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;

public class UpdateResourcePositionCommand : IRequest<Wrappers.Response<bool>>
{
	public Guid Id { get; set; }
	public Guid ResourceId { get; set; }
	public Guid PositionId { get; set; }
	public byte PercentMatchPosition { get; set; }
	public bool IsDefault { get; set; }
	public string? ResourceName { get; set; }
	public string? RomaId { get; set; }
}

public class UpdateResourcePositionCommandHandler : IRequestHandler<UpdateResourcePositionCommand, Wrappers.Response<bool>>
{
	private readonly IResourcePositionRepository _resourcePositionRepository;
	private readonly IMapper _mapper;


	public UpdateResourcePositionCommandHandler(IResourcePositionRepository resourcePositionRepository, IMapper mapper)
	{
		_resourcePositionRepository = resourcePositionRepository;
		_mapper = mapper;
	}

	public Task<Wrappers.Response<bool>> Handle(UpdateResourcePositionCommand request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");
		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<bool>> ProcessHandle(UpdateResourcePositionCommand request,
		CancellationToken cancellationToken = default)
	{
		var newRecord = _mapper.Map<ResourcePosition>(request);
		var data = await _resourcePositionRepository.UpdateAsync(newRecord, newRecord.Id, cancellationToken);

		return new Wrappers.Response<bool>(data);
	}
}
