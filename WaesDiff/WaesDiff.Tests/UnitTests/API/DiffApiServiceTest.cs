namespace WaesDiff.Tests.UnitTests.API
{
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using WaesDiff.API.Services;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Settings;
    using WaesDiff.Infrastructure.Repository;
    using Xunit;

    /// <remarks>Here is possible to create more test, to verify Get Method or verify if Save data really save the data, but I think is ok for this prototype.</remarks>
    public class DiffApiServiceTest
    {
        [Fact]
        public static void SaveDataWithEmptyDataReturnException()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.SaveData(0, null, EnumDataType.Left)).ConfigureAwait(false);
        }

        [Fact]
        public static void SaveDataWithInvalidBase64DataReturnException()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.SaveData(0, "123", EnumDataType.Left)).ConfigureAwait(false);
        }
    }
}
