namespace WaesDiff.Domain.Services
{
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Services.Commands;
    using WaesDiff.Domain.Settings;

    public class DiffService : IDiffService
    {
        private readonly IEnumerable<IDiffCommand> _diffCommands;

        private readonly Settings _options;

        private DataEntity DataEntityLeft { get; set; }

        private DataEntity DataEntityRight { get; set; }

        public DiffService(IOptions<Settings> options, IEnumerable<IDiffCommand> diffCommands)
        {
            _diffCommands = diffCommands;
            _options = options.Value;
        }

        /// <summary>
        /// Execute all commands responsible for doing the logic and diff between the data(left / right)
        /// </summary>
        /// <param name="dataEntities">List with the data (left/right)</param>
        public DiffResult GetDiff(List<DataEntity> dataEntities)
        {
            DataEntityLeft = dataEntities.FirstOrDefault(q => q.EnumDataType == EnumDataType.Left);

            DataEntityRight = dataEntities.FirstOrDefault(q => q.EnumDataType == EnumDataType.Right);

            DiffResult diffResult = null;
            foreach (var command in _diffCommands.OrderBy(q => q.Order))
            {
                diffResult = command.GetDiff(DataEntityLeft, DataEntityRight);
                if (diffResult != null)
                    break;
            }

            return diffResult ?? new DiffResult { Message = $"{_options.Messages.Inconclusive} {DataEntityLeft?.Id}" };
        }
    }
}
