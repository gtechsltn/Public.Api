﻿using FluentAssertions;
using FunctionalTests.Others;
using Xunit;

namespace FunctionalTests
{
    public class GetImageGroupTest : Test
    {
        [Fact]
        public async Task GivenImage_WhenSaveAndGetImageGroup_IsGot()
        {
            //Given
            const string imagePath = @"Images\didi.jpeg";

            //When
            var imageGroup = await apiClient.SaveImageGroup(imagePath);

            //Then
            var imageGroup2 = await apiClient.GetImageGroup(imageGroup.Id);
            imageGroup.Should().BeEquivalentTo(imageGroup2);
        }
    }
}
