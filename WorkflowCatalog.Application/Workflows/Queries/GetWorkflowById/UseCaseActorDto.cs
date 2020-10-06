using System;
using WorkflowCatalog.Application.Common.Mappings;
using WorkflowCatalog.Domain.Entities;

namespace WorkflowCatalog.Application.Workflows.Queries.GetWorkflowById
{
    public class UseCaseActorDto : IMapFrom<UseCaseActor>
    {
        public int Id { get; set; }
        public int UseCaseId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Workflow, SingleWorkflowDto>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => (int)s.Type))
                .ForMember(d => d.UseCases, opt => opt.MapFrom(x => x.UseCases.Select(y => y.Id).ToList()))
                .ForMember(d => d.Diagrams, opt => opt.MapFrom(x => x.Diagrams.Select(y => y.Id).ToList()))
                .ForMember(d => d.PrimaryDiagramId, opt => opt.MapFrom(x => x.Primary.Id));


            //.ForMember(d => d.Workflows, opt => opt.MapFrom(q=> q.Workflows.ToList()));
        }
    }
}
