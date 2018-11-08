namespace WaesDiff.Domain.Services.Commands
{
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;

    public interface IDiffCommand
    {
        /// <summary>
        /// Order for execution
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Get the result of diff between data left / right
        /// </summary>
        /// <param name="dataEntityLeft">data left for diff</param>
        /// <param name="dataEntityRight">data right for diff</param>
        DiffResult GetDiff(DataEntity dataEntityLeft, DataEntity dataEntityRight);
    }
}
