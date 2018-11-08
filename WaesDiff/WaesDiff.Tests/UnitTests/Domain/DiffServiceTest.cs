namespace WaesDiff.Tests.UnitTests.Domain
{
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;
    using Xunit;

    public static class DiffServiceTest
    {
        [Fact]
        public static void ExecuteServiceWithoutCommandReturnInconclusiveMessage()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings { Inconclusive = "Inconclusive" } };
            IOptions<Settings> options = Options.Create(settings);
            var service = new DiffService(options, new List<IDiffCommand>());

            //Act
            var result = service.GetDiff(new List<DataEntity>());

            //Assert
            Assert.Equal("Inconclusive", result.Message.Trim());
        }
    }
}
