namespace WaesDiff.API.Services
{
    using System.Threading.Tasks;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IDiffApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diffId"></param>
        /// <param name="json"></param>
        /// <param name="diffType"></param>
        /// <returns></returns>
        Task SaveJson(int diffId, string json, DiffType diffType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DiffResult> GetDiff(int id);
    }
}
