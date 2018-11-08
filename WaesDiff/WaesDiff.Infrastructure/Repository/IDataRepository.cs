namespace WaesDiff.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Domain.Entities;

    public interface IDataRepository
    {
        Task Save(DataEntity dataEntity);

        Task<List<DataEntity>> Get(int id);
    }
}
