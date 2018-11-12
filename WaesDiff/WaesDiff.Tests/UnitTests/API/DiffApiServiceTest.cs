namespace WaesDiff.Tests.UnitTests.API
{
    using Microsoft.Extensions.Options;
    using Moq;
    using System;
    using System.Threading.Tasks;

    using WaesDiff.API.Services;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Settings;
    using WaesDiff.Infrastructure.Repository;
    using Xunit;

    /// <remarks>Here is possible to create more test, to verify Get Method or verify if Save data really save the data, but I think is ok for this prototype.</remarks>
    public static class DiffApiServiceTest
    {
        [Fact]
        public static void SaveDataWithEmptyDataReturnException()
        {
            //Arrange
            const int id = 0;
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.SaveData(id, null, EnumDataType.Left)).ConfigureAwait(false);
        }

        [Fact]
        public static void SaveDataWithInvalidBase64DataReturnException()
        {
            //Arrange
            const int id = 0;
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.SaveData(id, "123", EnumDataType.Left)).ConfigureAwait(false);
        }

        [Fact]
        public static void GetDataWithInvalidIdReturnException()
        {
            //Arrange
            const int id = -1;
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.GetDiff(id)).ConfigureAwait(false);
        }

        [Fact]
        public static void GetDataWithValidIdWithOneDataForDiffReturnException()
        {
            //Arrange
            const int id = 200;
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);
            diffApiService.SaveData(id, Util.DataEntitySameSizeA.Data, EnumDataType.Left).ConfigureAwait(false);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => diffApiService.GetDiff(id)).ConfigureAwait(false);
        }

        [Fact]
        public static void GetDataWithValidIdForDiffReturnException()
        {
            //Arrange
            const int id = 201;
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var mockDataRepository = new Mock<IDataRepository>();
            var mockDiffService = new Mock<IDiffService>();

            var diffApiService = new DiffApiService(options, mockDataRepository.Object, mockDiffService.Object);
            diffApiService.SaveData(id, Util.DataEntitySameSizeA.Data, EnumDataType.Left).ConfigureAwait(false);
            diffApiService.SaveData(id, Util.DataEntitySameSizeA.Data, EnumDataType.Right).ConfigureAwait(false);

            //Act & Assert
            Assert.IsType<Task<DiffResult>>(diffApiService.GetDiff(id));
        }
    }
}
