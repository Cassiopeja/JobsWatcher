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
    public class SkillsControllerHttpTests :  IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationTestFixture _fixture;

        public SkillsControllerHttpTests(CustomWebApplicationTestFixture fixture, ITestOutputHelper output)
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
        public async Task ShouldReturnSkills()
        {
            var response = await _client.GetAsync("/api/skills");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var skills = JsonConvert.DeserializeObject<List<VacancySkillDto>>(content);
            skills.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task ShouldReturnFiveSkills()
        {
            var response = await _client.GetAsync("/api/skills?limit=5");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var skills = JsonConvert.DeserializeObject<List<VacancySkillDto>>(content);
            skills.Should().HaveCount(5);
        }

        [Fact]
        public async Task ShouldReturnFiveSkillsForSubscription()
        {
            var response = await _client.GetAsync("/api/skills?subscriptionid=1&limit=5");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var skills = JsonConvert.DeserializeObject<List<VacancySkillDto>>(content);
            skills.Should().HaveCount(5);
        }
    }
}