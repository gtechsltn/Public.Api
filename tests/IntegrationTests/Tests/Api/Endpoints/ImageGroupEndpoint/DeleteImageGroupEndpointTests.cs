﻿using ApiClient.Extensions;
using Core.Testing.Builders;
using Core.Testing.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace IntegrationTests.Tests.Api.Endpoints.ImageGroupEndpoint
{
    [Collection(nameof(ApiCollection))]
    public class DeleteImageGroupEndpointTests : Test
    {
        [InlineData(nameof(ApiClient.ControllerApi), 1)]
        [InlineData(nameof(ApiClient.ControllerApi), 2)]
        [InlineData(nameof(ApiClient.MinimalApi), 1)]
        [InlineData(nameof(ApiClient.MinimalApi), 2)]
        [Theory]
        public async Task GivenImageGroup_WhenDelete_IsDeleted(string apiType, int version)
        {
            //Given
            const string imagePath = @"Images\didi.jpeg";
            var imageGroup = await ApiClient.GetApiEndpoints(apiType).SaveImageGroup(imagePath).To<ImageGroup>();
            var imageGroup2 = await ApiClient.GetApiEndpoints(apiType).GetImageGroup(imageGroup.Id).To<ImageGroup>();
            imageGroup.Should().BeEquivalentTo(imageGroup2);

            //When
            var deleteResponse = await ApiClient.GetApiEndpoints(apiType).DeleteImageGroup(imageGroup.Id, version);

            //Then
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var getResponse = await ApiClient.GetApiEndpoints(apiType).GetImageGroup(imageGroup.Id);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [InlineData(nameof(ApiClient.ControllerApi))]
        [InlineData(nameof(ApiClient.MinimalApi))]
        [Theory]
        public async Task WhenDeleteNonexistentImageGroup_ExpectedProblemDetails(string apiType)
        {
            //When
            var response = await ApiClient.GetApiEndpoints(apiType).DeleteImageGroup(id: 600);

            //Then
            var expected = new ProblemDetailsBuilder()
                .WithNotFoundException(apiType, "ImageGroup", 600)
                .Build();

            var problemDetails = await response.To<ProblemDetails>();
            problemDetails.Should().BeEquivalentTo(expected);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
