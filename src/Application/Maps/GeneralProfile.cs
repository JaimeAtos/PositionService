using Application.DTOs;
using Application.Features.Positions.Commands.CreatePositionCommand;
using Application.Features.Positions.Queries.GetPositionById;
using Application.Features.PositionSkills.Commands.CreatePositionSkillCommand;
using Application.Features.PositionSkills.Queries.GetPositionSkillById;
using Application.Features.ResourcePositions.Commands.CreateResourcePositionCommand;
using Application.Features.ResourcePositions.Queries.GetResourcePositionById;
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
        #endregion

        #region DTOs
        CreateMap<Position, PositionDto>();
        CreateMap<PositionSkill, PositionSkillDto>();
        CreateMap<ResourcePosition, ResourcePositionDto>();
        #endregion
    }
}
