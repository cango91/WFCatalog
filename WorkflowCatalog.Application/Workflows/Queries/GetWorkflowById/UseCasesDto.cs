using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class UseCasesDto : IMapFrom<UseCase>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Actors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UseCase, UseCasesDto>()
                .ForMember(d => d.Actors, opt => opt.MapFrom(s => s.Actors.Select(s => s.Name).ToList()));
        }
    }
}
