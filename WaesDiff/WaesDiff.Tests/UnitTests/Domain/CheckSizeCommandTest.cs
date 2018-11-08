namespace WaesDiff.Tests.UnitTests.Domain
{
    using Microsoft.Extensions.Options;

    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;

    using Xunit;

    public class CheckSizeCommandTest
    {
        [Fact]
        public static void VerifyDataWhenHaveSameSizeReturnNull()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckSizeCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntitySameSizeA);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public static void VerifyDataWhenDoNotHaveSameSizeReturnNotNull()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckSizeCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntityDifferentSize);

            //Assert
            Assert.NotNull(result);
        }
    }
}
