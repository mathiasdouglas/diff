namespace WaesDiff.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using WaesDiff.API.Services;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;

    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private IDiffApiService _diffApiService;

        public DiffController(IDiffApiService diffApiService)
        {
            _diffApiService = diffApiService;
        }

        // GET api/values/5
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
        /// <param name="jsonLeft">The value for the json left</param>
        [HttpPost("{id}/left")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task PostLeft(int id, [FromBody] string jsonLeft)
        {
            await _diffApiService.SaveJson(id, jsonLeft, DiffType.Left);
        }

        /// <summary>
        /// Post the json right for diff
        /// </summary>
        /// <param name="id">id for create the link between json (left, right)</param>
        /// <param name="jsonRight">The value for the json right</param>
        [HttpPost("{id}/right")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task PostRight(int id, [FromBody] string jsonRight)
        {
            await _diffApiService.SaveJson(id, jsonRight, DiffType.Right);
        }
    }
}
