namespace WaesDiff.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WaesDiff.API.Services;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;

    /// <summary>
    /// Controller responsible for endpoints of the api
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly IDiffApiService _diffApiService;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiffController(IDiffApiService diffApiService)
        {
            _diffApiService = diffApiService;
        }

        /// <summary>
        /// Get a result between the two data posted (left / right)
        /// </summary>
        /// <param name="id">id of the data posted</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DiffResult), StatusCodes.Status201Created)]
        public async Task<DiffResult> Get(int id)
        {
            return await _diffApiService.GetDiff(id);
        }

        /// <summary>
        /// Post the json left for diff
        /// </summary>
        /// <param name="id">id for create the link between json (left, right)</param>
        /// <param name="dataLeft">The value for the json left</param>
        [HttpPost("{id}/left")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task PostLeft(int id, [FromBody] string dataLeft)
        {
            await _diffApiService.SaveData(id, dataLeft, EnumDataType.Left);
        }

        /// <summary>
        /// Post the json right for diff
        /// </summary>
        /// <param name="id">id for create the link between json (left, right)</param>
        /// <param name="dataRight">The value for the json right</param>
        [HttpPost("{id}/right")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task PostRight(int id, [FromBody] string dataRight)
        {
            await _diffApiService.SaveData(id, dataRight, EnumDataType.Right);
        }
    }
}
