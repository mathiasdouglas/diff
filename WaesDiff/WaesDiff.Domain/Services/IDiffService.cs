namespace WaesDiff.Domain.Services
{
    using System.Collections.Generic;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;

    public interface IDiffService
    {
        DiffResult GetDiff(List<DataEntity> dataEntities);
    }
}
