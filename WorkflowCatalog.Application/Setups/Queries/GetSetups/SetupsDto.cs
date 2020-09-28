using System;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;
using WorkflowCatalog.Domain.Enums;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetups
{
    public class SetupsDto : IMapFrom<Setup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Setup, SetupsDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => (int)s.Status));
        }
    }
}
