namespace WaesDiff.Domain.Services
{
    using System.Collections.Generic;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;

    public interface IDiffService
    {
        /// <summary>
        /// Method responsible for get the difference between data (left/right), if exist
        /// </summary>
        /// <param name="dataEntities">List with left and right data</param>
        DiffResult GetDiff(List<DataEntity> dataEntities);
    }
}
