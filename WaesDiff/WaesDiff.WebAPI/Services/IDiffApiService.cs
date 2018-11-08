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
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        Task SaveData(int id, string data, DataType dataType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DiffResult> GetDiff(int id);
    }
}
