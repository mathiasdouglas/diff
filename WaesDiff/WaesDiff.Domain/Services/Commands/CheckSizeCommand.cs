namespace WaesDiff.Domain.Services.Commands
{
    using Microsoft.Extensions.Options;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Settings;

    public class CheckSizeCommand: IDiffCommand
    {
        public int Order => 2;

        private readonly Settings _options;

        public CheckSizeCommand(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Verify if the data has the same size
        /// </summary>
        /// <param name="dataEntityLeft">Entity of the left data</param>
        /// <param name="dataEntityRight">entity of the right data</param>
        public DiffResult GetDiff(DataEntity dataEntityLeft, DataEntity dataEntityRight)
        {
            if (dataEntityLeft.Data.Length != dataEntityRight.Data.Length)
                return new DiffResult { Message = $"{_options.Messages.NotSameSize} {dataEntityLeft.Id}" };

            return null;
        }
    }
}
