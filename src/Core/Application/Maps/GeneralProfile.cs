using Application.DTOs;
using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.Positions.Commands.UpdatePositionCommand;
using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using Application.Features.PositionSkills.Commands.UpdatePositionSkillCommand;
using Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;
using Application.Features.ResourcePositions.Commands.UpdateResourcePositionCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Maps;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        #region Commands
        CreateMap<CreatePositionCommand, Position>();
        CreateMap<CreatePositionSkillCommand, PositionSkill>();
        CreateMap<CreateResourcePositionCommand, ResourcePosition>();
        CreateMap<UpdatePositionCommand, Position>();
        CreateMap<UpdatePositionSkillCommand, PositionSkill>();
        CreateMap<UpdateResourcePositionCommand, ResourcePosition>();
        #endregion

        #region DTOs
        CreateMap<Position, PositionDto>();
        CreateMap<PositionSkill, PositionSkillDto>();
        CreateMap<ResourcePosition, ResourcePositionDto>();
        #endregion
    }
}
