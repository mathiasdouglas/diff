namespace WaesDiff.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WaesDiff.Domain.Entities;

    public interface IJsonRepository
    {
        Task Save(JsonEntity jsonEntity);

        Task<List<JsonEntity>> Get(int id);
    }
}
