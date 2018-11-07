namespace WaesDiff.API.Services
{
    using System.Threading.Tasks;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;

    public interface IDiffApiService
    {
        Task SaveJson(int diffId, string json, DiffType diffType);

        Task<DiffResult> GetDiff(int id);
    }
}
