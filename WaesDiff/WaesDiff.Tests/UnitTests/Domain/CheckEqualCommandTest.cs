namespace WaesDiff.Tests.UnitTests.Domain
{
    using Microsoft.Extensions.Options;
    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;
    using Xunit;

    public static class CheckEqualCommandTest
    {

        [Fact]
        public static void VerifyDataWhenIsEqualReturnNotNull()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckEqualCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntitySameSizeA);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public static void VerifyDataWhenIsNotEqualReturnNull()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckEqualCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntityDifferentSize);

            //Assert
            Assert.Null(result);
        }
    }
}
