namespace WaesDiff.Tests.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using WaesDiff.API;
    using WaesDiff.Domain.Enum;
    using Xunit;

    /// <remarks>Its possible to do this tests Mocking the database, but I've just using database on the integration tests because this is a prototype. </remarks>
    public class DiffControllerTest
    {
        private readonly HttpClient _client;

        public DiffControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Fact]
        public async Task CallPostWithEmptyDataReturnBadRequest()
        {
            //Act
            var result = await _client.PostAsync(PostUrl(99, EnumDataType.Left), ConvertStringContent(string.Empty));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [Fact]
        public async Task CallPostWithNonBase64DataReturnBadRequest()
        {
            //Act
            var result = await _client.PostAsync(PostUrl(99, EnumDataType.Left), ConvertStringContent("123"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [Fact]
        public async Task CallPostWithValidBase64DataReturnOk()
        {
            //Act
            var result = await _client.PostAsync(PostUrl(99, EnumDataType.Left), ConvertStringContent(Util.DataEntitySameSizeA.Data));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        }

        [Fact]
        public async Task CallGetWithoutDataForDiffReturnNotFound()
        {
            //Act
            var result = await _client.GetAsync(GetUrl(100));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        }

        [Fact]
        public async Task CallGetWithOneDataForDiffReturnNotFound()
        {
            //Arrange
            const int id = 100;
            await _client.PostAsync(PostUrl(id, EnumDataType.Left), ConvertStringContent(Util.DataEntitySameSizeA.Data));

            //Act
            var result = await _client.GetAsync(GetUrl(id));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task CallGetWithCorrectDataForDiffReturnOk()
        {
            //Arrange
            const int id = 101;
            await _client.PostAsync(PostUrl(id, EnumDataType.Left), ConvertStringContent(Util.DataEntitySameSizeA.Data));
            await _client.PostAsync(PostUrl(id, EnumDataType.Right), ConvertStringContent(Util.DataEntitySameSizeB.Data));

            //Act
            var result = await _client.GetAsync(GetUrl(id));

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        #region private methods

        private StringContent ConvertStringContent(string value)
        {
            var jsonString = JsonConvert.SerializeObject(value);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        private static string PostUrl(int id, EnumDataType enumDataType)
        {
            return $"/v1/diff/{id}/{enumDataType}";
        }

        private static string GetUrl(int id)
        {
            return $"/v1/diff/{id}";
        }

        #endregion
    }
}
