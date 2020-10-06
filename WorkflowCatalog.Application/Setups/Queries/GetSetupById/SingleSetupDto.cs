using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetupById
{
    public class SingleSetupDto : IMapFrom<Setup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Status { get; set; }
        public List<int> Workflows { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Setup, SingleSetupDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => (int)s.Status))
                .ForMember(d => d.Workflows, opt => opt.MapFrom(x => x.Workflows.Select(y => y.Id).ToList()));
            
        }
    }
}
