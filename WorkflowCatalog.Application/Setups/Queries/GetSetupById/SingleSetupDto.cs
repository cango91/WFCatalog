using AutoMapper;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Application.Common.Models;
using WorkflowCatalog.Application.UCActors.Queries;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Setups.Queries.GetSetupById
{
    public class SingleSetupDto : Auditable, IMapFrom<Setup>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public List<ActorDto> Actors { get; set; }
        public List<Guid> WorkflowIds { get; set; }

        public void Configure(Profile profile)
        {
            profile.CreateMap<Setup, SingleSetupDto>()
                .ForMember(x => x.WorkflowIds, opt => opt.MapFrom(x => x.Workflows.Select(x=> x.Id)));
        }

        /*  public void Configure(Profile profile)
          {
              profile.CreateMap<Setup, SingleSetupDto>()
                  .ForMember(x => x.WorkflowCount, opt => opt.MapFrom(x => x.Workflows.Count));
          }*/




    }
}
