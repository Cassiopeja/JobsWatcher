using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JobsWatcher.Api.Contracts.Dto;
using JobsWatcher.Api.Contracts.Responses;
using JobsWatcher.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace JobsWatcher.Api.IntegrationTests.Controllers
{
    [Collection("CustomWebApplication")]
    public class SubscriptionsControllerHttpTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationTestFixture _fixture;

        public SubscriptionsControllerHttpTests(CustomWebApplicationTestFixture fixture, ITestOutputHelper output)
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
        public async Task ShouldReturnSubscriptions()
        {
            var response = await _client.GetAsync("/api/subscriptions");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var subscriptions = JsonConvert.DeserializeObject<List<SubscriptionDto>>(content);
            subscriptions.Should().HaveCount(5);
        }

        [Fact]
        public async Task ShouldReturnSubscriptionById()
        {
            var response = await _client.GetAsync("/api/subscriptions/1");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var subscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            subscription.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldCreateNewSubscription()
        {
            var newSubscription = Utilities.GetRandomSubscriptionCreateDto();
            var httpContent = HttpUtilities.GetHttpContent(newSubscription);

            var response = await _client.PostAsync("/api/subscriptions", httpContent);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var storageSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            storageSubscription.Should().NotBeNull();
            storageSubscription.Name.Should().BeEquivalentTo(newSubscription.Name);
            storageSubscription.Id.Should().BeGreaterThan(0);
        }


        [Fact]
        public async Task ShouldUpdateSubscription()
        {
            var updatedSubscription = new SubscriptionUpdateDto
                {Name = "UpdatedFromTest", SourceTypeId = 1, Parameters = "{}", CreatedDate = DateTimeOffset.Now};
            var httpContent = HttpUtilities.GetHttpContent(updatedSubscription);

            var response = await _client.PutAsync("/api/subscriptions/1", httpContent);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var storageSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);
            storageSubscription.Should().NotBeNull();
            storageSubscription.Name.Should().BeEquivalentTo(updatedSubscription.Name);
        }

        [Fact]
        public async Task ShouldDeleteSubscription()
        {
            var newSubscription = new SubscriptionCreateDto
                {Name = "UpdatedFromTest", SourceTypeId = 1, Parameters = "{}"};
            var httpContent = HttpUtilities.GetHttpContent(newSubscription);
            var response = await _client.PostAsync("/api/subscriptions", httpContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var storageSubscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);

            response = await _client.DeleteAsync($"/api/subscriptions/{storageSubscription.Id}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ShouldReturnSubscriptionVacancies()
        {
            var subscriptionId = 1;

            var response = await _client.GetAsync($"/api/subscriptions/{subscriptionId}/vacancies");
            
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<PagedResponse<SubscriptionVacancyDto>>(content);
            pagedResponse.Should().NotBeNull();
            pagedResponse.Data.Should().NotBeNull();
            pagedResponse.Data.Should().HaveCount(c => c > 0);
        }

        
        
    }
}