namespace WaesDiff.Tests.UnitTests.Domain
{
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;
    using Xunit;

    public static class CheckDiffCommandTest
    {
        [Fact]
        public static void VerifyDataIsEqualReturnEmptyList()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckDiffCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntitySameSizeA);

            //Assert
            Assert.Empty(result.Detail);
        }

        [Fact]
        public static void VerifyDataIsNotEqualReturnDetail()
        {
            //Arrange
            var settings = new Settings { Messages = new MessageSettings() };
            IOptions<Settings> options = Options.Create(settings);
            var command = new CheckDiffCommand(options);

            //Act
            var result = command.GetDiff(Util.DataEntitySameSizeA, Util.DataEntitySameSizeB);

            //Assert
            Assert.NotNull(result.Detail);

            var listDetail = new List<DiffDetail>
                                 {
                                     new DiffDetail
                                         {
                                             Offset = 1,
                                             Length = 1
                                         },
                                     new DiffDetail
                                         {
                                             Offset = 61,
                                             Length = 6
                                         },
                                     new DiffDetail
                                         {
                                             Offset = 73,
                                             Length = 1
                                         },
                                     new DiffDetail
                                         {
                                             Offset = 75,
                                             Length = 1
                                         }
                                 };

            for (int i = 0; i < listDetail.Count; i++)
            {
                Assert.Equal(listDetail[i].Offset, result.Detail[i].Offset);
                Assert.Equal(listDetail[i].Length, result.Detail[i].Length);
            }
        }
    }
}
