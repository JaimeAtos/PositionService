using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;

public class CreateResourcePositionCommand : IRequest<Wrappers.Response<Guid>>
{
	public Guid PositionId { get; set; }
	public Guid ResourceId { get; set; }
	public byte PercentMatchPosition { get; set; }
	public bool IsDefault { get; set; }
	public string? ResourceName { get; set; }
	public string? RomaId { get; set; }
}

public class
	CreateResourcePositionCommandHandler : IRequestHandler<CreateResourcePositionCommand, Wrappers.Response<Guid>>
{
	private readonly IResourcePositionRepository _resourcePositionRepository;
	private readonly IMapper _mapper;

	public CreateResourcePositionCommandHandler(IResourcePositionRepository resourcePositionRepository, IMapper mapper)
	{
		_resourcePositionRepository = resourcePositionRepository;
		_mapper = mapper;
	}

	public Task<Wrappers.Response<Guid>> Handle(CreateResourcePositionCommand request,
		CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<Guid>> ProcessHandle(CreateResourcePositionCommand request,
		CancellationToken cancellationToken)
	{
		var newRecord = _mapper.Map<ResourcePosition>(request);
		var data = await _resourcePositionRepository.CreateAsync(newRecord, cancellationToken);
		return new Wrappers.Response<Guid>(data);
	}
}
