﻿using System;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class UCActorDto : IMapFrom<UseCaseActor>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCaseActor, UCActorDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(d => d.Id));
               
        }
    }
}
