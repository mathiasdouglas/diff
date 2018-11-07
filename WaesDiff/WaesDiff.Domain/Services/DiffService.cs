namespace WaesDiff.Domain.Services
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Settings;

    public class DiffService : IDiffService
    {
        private readonly Settings _options;

        private string JsonLeft { get; set; }

        private string JsonRight { get; set; }

        public DiffService(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        public async Task<DiffResult> GetDiff(List<JsonEntity> jsonEntities)
        {
            JsonLeft = jsonEntities.FirstOrDefault(q => q.JsonType == DiffType.Left)?.Json;

            JsonRight = jsonEntities.FirstOrDefault(q => q.JsonType == DiffType.Right)?.Json;

            if (CheckEqual())
                return new DiffResult { Message = $"{_options.General.JsonEqual} {jsonEntities[0].Id}" };

            if (CheckSize())
                return new DiffResult { Message = $"{_options.General.NotSameSize} {jsonEntities[0].Id}" };

            var diffResult = CheckDiff();

            return diffResult ?? new DiffResult { Message = $"{_options.General.Inconclusive} {jsonEntities[0].Id}" };
        }

        private bool CheckEqual()
        {
            return string.Equals(JsonLeft, JsonRight);
        }

        private bool CheckSize()
        {
            return JsonLeft.Length != JsonRight.Length;
        }

        private DiffResult CheckDiff()
        {
            byte[] leftConvert, rightConvert;
            try
            {
                leftConvert = Convert.FromBase64String(JsonLeft);
            }
            catch (Exception ex)
            {
                return new DiffResult { Message = $"{_options.General.ErrorDuringDiff} Error: {ex.Message}" };
            }

            try
            {
                rightConvert = Convert.FromBase64String(JsonRight);
            }
            catch (Exception ex)
            {
                return new DiffResult { Message = $"{_options.General.ErrorDuringDiff} Error: {ex.Message}" };
            }

            var diffResult = new DiffResult { Message = _options.General.JsonDifference };

            DiffDetail detail = null;

            for (var index = 0; index < leftConvert.Length; index++)
            {
                if (leftConvert[index] != rightConvert[index])
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
