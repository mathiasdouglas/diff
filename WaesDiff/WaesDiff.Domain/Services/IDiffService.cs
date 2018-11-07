namespace WaesDiff.Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;

    public interface IDiffService
    {
        Task<DiffResult> GetDiff(List<JsonEntity> jsonEntities);
    }
}
