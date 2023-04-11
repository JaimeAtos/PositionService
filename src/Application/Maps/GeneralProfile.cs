using Application.DTOs;
using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Maps;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        #region
        CreateMap<CreatePositionCommand, Position>();
        CreateMap<CreatePositionSkillCommand, PositionSkill>();
        CreateMap<CreateResourcePositionCommand, ResourcePosition>();
        #endregion
    }
}
