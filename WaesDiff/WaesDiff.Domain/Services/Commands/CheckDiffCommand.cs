namespace WaesDiff.Domain.Services.Commands
{
    using Microsoft.Extensions.Options;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Settings;

    public class CheckDiffCommand : IDiffCommand
    {
        public int Order => 3;

        private readonly Settings _options;

        public CheckDiffCommand(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Get the diff between the data (left/right) and return the position and length of the differences
        /// </summary>
        /// <param name="dataEntityLeft">Entity of the left data</param>
        /// <param name="dataEntityRight">entity of the right data</param>
        public DiffResult GetDiff(DataEntity dataEntityLeft, DataEntity dataEntityRight)
        {
            var diffResult = new DiffResult { Message = _options.Messages.DataDifference };

            DiffDetail detail = null;

            for (long index = 0; index < dataEntityLeft.DataBase64.Length; index++)
            {
                if (dataEntityLeft.DataBase64[index] != dataEntityRight.DataBase64[index])
                {
                    if (detail == null)
                    {
                        detail = new DiffDetail { Offset = index + 1, Length = 1 };
                    }
                    else
                    {
                        detail.Length++;
                    }
                }
                else if (detail != null)
                {
                    diffResult.Detail.Add(detail);
                    detail = null;
                }
            }

            if (detail != null)
            {
                diffResult.Detail.Add(detail);
            }

            return diffResult;
        }
    }
}
