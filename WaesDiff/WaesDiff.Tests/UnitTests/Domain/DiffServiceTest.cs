namespace WaesDiff.Tests.UnitTests.Domain
{
    using System.Collections.Generic;

    using Microsoft.Extensions.Options;

    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;

    using Xunit;

    public class DiffServiceTest
    {
        [Fact]
        public void ExecuteServiceWithoutCommandReturnInconclusiveMessage()
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
