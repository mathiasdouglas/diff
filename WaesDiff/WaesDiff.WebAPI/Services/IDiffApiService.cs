namespace WaesDiff.API.Services
{
    using System.Threading.Tasks;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;

    /// <summary>
    /// Service for the Application part of the project, responsible for save the data (left/right), and get the diff
    /// </summary>
    public interface IDiffApiService
    {
        /// <summary>
        /// Save the data on the repository
        /// </summary>
        /// <param name="id">id of the data</param>
        /// <param name="data">value of the data</param>
        /// <param name="enumDataType">type of the data (left/right)</param>
        Task SaveData(int id, string data, EnumDataType enumDataType);

        /// <summary>
        /// Get the diff between the data
        /// </summary>
        /// <param name="id">id of the data for the diff</param>
        Task<DiffResult> GetDiff(int id);
    }
}
