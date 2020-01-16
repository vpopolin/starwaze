using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StarWaze.Gateway;
using Xunit;

namespace StarWaze.Services.Tests
{
    public class StarshipServiceTests
    {
        private readonly Mock<ISWApiGateway> gatewayMock;
        private readonly IStarshipService target;

        public StarshipServiceTests()
        {
            this.gatewayMock = new Mock<ISWApiGateway>();
            this.target = new StarshipService(gatewayMock.Object);
        }

        [Theory]
        [InlineData("Millenium Falcon", "75", "2 months", "9")]
        [InlineData("Millenium Halfcon", "75", "1 month", "18")]
        [InlineData("Naboo StarFighter", "75", "120 days", "4")]
        [InlineData("A-Wing", "120", "1 week", "49")]
        [InlineData("Behemot", "75", "1 day", "555")]
        [InlineData("Rocinante", "idk", "1 year", "Unknown")]
        [InlineData("Science vessel", "98", "unknown", "Unknown")]
        public async Task GetRequiredStops_KnownConsumables_ReturnsAuthonomy(string name, string mglt, string consumables, string stops)
        {
            // Arrange
            var distance = 1000000m;
            var starships = new List<StarshipDTO>()
            {
                new StarshipDTO
                {
                     Consumables = consumables,
                     MGLT = mglt,
                     Name = name
                }
            };

            this.gatewayMock.Setup(g => g.GetStarships())
                .ReturnsAsync(starships);

            // Act
            var result = await this.target.GetRequiredStops(distance);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(name, result[0].Key);
            Assert.Equal(stops, result[0].Value);
        }

        [Fact]
        public async Task GetRequiredStops_UnknownTimeMeasure_ThrowsException()
        {
            // Arrange
            var starships = new List<StarshipDTO>()
            {
                new StarshipDTO
                {
                     Consumables = "1 aeon",
                     MGLT = "1",
                     Name = "Nabucodonosor"
                }
            };

            this.gatewayMock.Setup(g => g.GetStarships())
                .ReturnsAsync(starships);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => target.GetRequiredStops(1));
        }
    }
}
