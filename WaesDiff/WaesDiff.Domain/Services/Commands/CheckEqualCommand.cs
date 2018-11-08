namespace WaesDiff.Domain.Services.Commands
{
    using Microsoft.Extensions.Options;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Settings;

    public class CheckEqualCommand : IDiffCommand
    {
        public int Order => 1;

        private readonly Settings _options;

        public CheckEqualCommand(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Verify if the data are equal
        /// </summary>
        /// <param name="dataEntityLeft">Entity of the left data</param>
        /// <param name="dataEntityRight">entity of the right data</param>
        public DiffResult GetDiff(DataEntity dataEntityLeft, DataEntity dataEntityRight)
        {
            if (string.Equals(dataEntityLeft.Data, dataEntityRight.Data))
                return new DiffResult { Message = $"{_options.Messages.DataEqual} {dataEntityLeft.Id}" };

            return null;
        }
    }
}
