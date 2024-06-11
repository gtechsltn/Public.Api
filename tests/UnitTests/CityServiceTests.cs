﻿using Xunit;
using Application.Services;
using FluentAssertions;

namespace UnitTests
{
    public class CityServiceTests
    {
        [InlineData("LAS PALMAS DE G. CANARIA", "LAS PALMAS DE GRAN CANARIA", true)]
        [InlineData("POSADA DE VALDEON", "PRADA DE VALDEON", true)]
        [InlineData("LESŸROQUETES", "LES ROQUETES", true)]
        [InlineData("PE#ISCOLA", "PEÑISCOLA", true)]
        [InlineData("STA EULALIA DEL RIO NEGRO", "SANTA EULALIA DEL RIO NEGRO", true)]
        [InlineData("BILBAO", "BILBO", true)]
        [InlineData("BARACALDO", "BARAKALDO", true)]
        [InlineData("TERRASSA", "TRERASSA", true)]
        [InlineData("TERRASSA", "TRRASSA", true)]
        [InlineData("TERRASSA", "TARRASA", true)]
        [InlineData("TERRASSA", "TARRASSA", true)]
        [InlineData("MANRESA", "MANRESA", true)] //Exact case
        [Theory]
        public void IsSameCityTests(string city, string candidate, bool expected)
        {
            //When
            var result = CityService.IsSameCity(city, candidate);

            //Then
            result.Should().Be(expected);
        }
    }
}