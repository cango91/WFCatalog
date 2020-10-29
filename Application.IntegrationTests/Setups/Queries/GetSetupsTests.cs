using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WorkflowCatalog.Application.Setups.Queries.GetSetups;
using FluentAssertions;
using WorkflowCatalog.Domain.Entities;
using System.Linq;

namespace Application.IntegrationTests.Setups.Queries
{
    using static Testing;
    public class GetSetupsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnSetupStatuses()
        {
            var query = new GetSetupsQuery();

            var result = await SendAsync(query);

            result.SetupStatus.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnASetupsListWithOneSetup()
        {
            await AddAsync(new Setup
            {
                Name = "Turkey",
                ShortName = "TR"
            });

            var query = new GetSetupsQuery();

            var result = await SendAsync(query);

            result.Setups.Should().HaveCount(1);
            result.Setups.First().ShortName.Should().Equals("TR");


        }
    }
}
