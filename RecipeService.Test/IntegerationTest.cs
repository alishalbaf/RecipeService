using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RecipeService.DB;
using RecipeService.DTO;
using RecipeService.DTO.Requests;
using RecipeService.Mappings;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Xunit.Extensions;

namespace RecipeService.Test
{
    // [TestCaseOrderer("TestOrderExamples.TestCaseOrdering.AlphabeticalOrderer", "TestOrderExamples")]
    [TestCaseOrderer("RecipeService.Test.PriorityOrderer", "RecipeService.Test")]
    public class GroupTests : IClassFixture<MemoryWebApplicationFactory<Program>>
    {
        private readonly MemoryWebApplicationFactory<Program> _factory;
        private readonly HttpClient client;
        public readonly IMapper mapper;
        private readonly RecipeDbContext dbContext;
        
        public GroupTests(MemoryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDtoProfile());
                cfg.CreateMap<RecipeDto, RecipeCreateDto>();
            });

            mapper = mapConfig.CreateMapper();
            
           
        }

        protected async Task seedData(HttpClient client)
        {
            //var response = await client.GetAsync($"/Recipe");
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    var existing=await response.Content.ReadFromJsonAsync< List<RecipeDto>>();
            //    //Task.WaitAll(existing.Select(r => client.DeleteAsync($"/Recipe/{r.Id}")).ToArray());
            //    foreach (var r in existing)
            //        await client.DeleteAsync($"/Recipe/{r.Id}");

            //}

                HttpResponseMessage? res;
                int repeatLeft;
                var listReqs = mapper.Map<List<RecipeCreateDto>>(GlobalData.recipes);
                //Task.WaitAll(listReqs.Select( r=> client.PostAsJsonAsync("/Recipe",r)).ToArray());
                foreach (var req in listReqs)
                {
                    repeatLeft = 5;
                    do
                    {
                        res = await client.PostAsJsonAsync("/Recipe", req);
                    } while (!res.IsSuccessStatusCode && repeatLeft-- > 0);
                }
            
        }
        [Fact, TestPriority(0)]
        public async void Should1GetAll()
            {
                // Arrange
                //using var client = _factory.CreateClient();
                await seedData(client);

                // Act
                var response = await client.GetFromJsonAsync<List<RecipeDto>>($"/Recipe");

                Assert.Equal(GlobalData.recipes.Count, response.Count);
                for (int i = 0; i < GlobalData.recipes.Count; i++)
                {
                    Assert.Equivalent(GlobalData.recipes[i], response[i]);
                }

            
        }

        [Fact, TestPriority(0)]
      
        public async void Should2GetOne()
        {
            // Arrange
            //using var client = _factory.CreateClient();
            await seedData(client);

            // Act
            var response = await client.GetFromJsonAsync<RecipeDto>($"/Recipe/1");

            Assert.Equivalent(GlobalData.recipes[0],response);
        }
        [Fact, TestPriority(2)]
 
        public async void Should3Put()
        {
            // Arrange
            //using var client = _factory.CreateClient();
            await seedData(client);
            RecipeUpdateDto updateRequest=new RecipeUpdateDto() { Name="Changed!"};
            // Act
            var response = await client.PutAsJsonAsync<RecipeUpdateDto>($"/Recipe/1", updateRequest);
            
            var qresponse = await client.GetFromJsonAsync<RecipeDto>($"/Recipe/1");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var r0 = GlobalData.recipes[0] with { };
            r0.Name = updateRequest.Name;

            Assert.Equivalent(r0, qresponse);
        }
        [Fact, TestPriority(6)]
        public async void Should4Delete()
        {
            // Arrange
            //using var client = _factory.CreateClient();
            await seedData(client);
            
            // Act
            var response = await client.DeleteAsync($"/Recipe/2");

            var qresponse = await client.GetAsync($"/Recipe/2");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, qresponse.StatusCode);

        }
    }
}