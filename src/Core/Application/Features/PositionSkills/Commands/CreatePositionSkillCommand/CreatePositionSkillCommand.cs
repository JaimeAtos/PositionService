using Application.Exceptions;
using Application.Extensions.Commands;
using Atos.Core.Abstractions.Publishers;
using Atos.Core.EventsDTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;

public class CreatePositionSkillCommand : IRequest<Wrappers.Response<Guid>>
{
	public Guid SkillId { get; set; }
	public Guid PositionId { get; set; }
	public string? SkillName { get; set; }
	public byte MinToAccept { get; set; }
	public PositionSkillType PositionSkillType { get; set; }
}

public class CreatePositionSkillCommandHandler : IRequestHandler<CreatePositionSkillCommand, Wrappers.Response<Guid>>
{
	private readonly IPositionSkillRepository _positionSkillRepository;
	private readonly IMapper _mapper;
	private readonly IPublisherCommands<PositionSkillCreated> _publisher;

	public CreatePositionSkillCommandHandler(IPositionSkillRepository positionSkillRepository, IMapper mapper,
		IPublisherCommands<PositionSkillCreated> publisher)
	{
		_positionSkillRepository = positionSkillRepository;
		_mapper = mapper;
		_publisher = publisher;
	}

	public Task<Wrappers.Response<Guid>> Handle(CreatePositionSkillCommand request, CancellationToken cancellationToken)
	{
		if (request is null)
			throw new ApiException("Request is empty");

		return ProcessHandle(request, cancellationToken);
	}

	private async Task<Wrappers.Response<Guid>> ProcessHandle(CreatePositionSkillCommand request,
		CancellationToken cancellationToken)
	{
		var newRecord = _mapper.Map<PositionSkill>(request);
		var data = await _positionSkillRepository.CreateAsync(newRecord, cancellationToken);

		await _publisher.PublishEntityMessage(request.ToPositionSkillCreated(data), "positionSkill.created", data,
			cancellationToken);

		return new Wrappers.Response<Guid>(data);
	}
}