using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JobsWatcher.Api.Contracts.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace JobsWatcher.Api.IntegrationTests.Controllers
{
    [Collection("CustomWebApplication")]
    public class AreasControllerHttpTests :  IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationTestFixture _fixture;

        public AreasControllerHttpTests(CustomWebApplicationTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _fixture.OutputHelper = output;
            _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        public void Dispose()
        {
            _fixture.OutputHelper = null;
        }

        [Fact]
        public async Task ShouldReturnAreasForSubscription()
        {
            var subscriptionId = 1;

            var response = await _client.GetAsync($"/api/areas?subscriptionId={subscriptionId}");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<List<AreaDto>>(content);
            pagedResponse.Should().NotBeNull();
            pagedResponse.Should().HaveCount(c => c > 0);
        }
    }
}