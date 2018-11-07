namespace WaesDiff.API.Services
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;
    using WaesDiff.Domain.Entities;
    using WaesDiff.Domain.Enum;
    using WaesDiff.Domain.Models;
    using WaesDiff.Domain.Services;
    using WaesDiff.Domain.Settings;
    using WaesDiff.Infrastructure.Repository;

    public class DiffApiService : IDiffApiService
    {
        private readonly Settings _options;

        private readonly IJsonRepository _jsonRepository;

        private readonly IDiffService _diffService;

        public DiffApiService(IOptions<Settings> options, IJsonRepository jsonRepository, IDiffService diffService)
        {
            _jsonRepository = jsonRepository;
            _diffService = diffService;
            _options = options.Value;
        }

        public async Task SaveJson(int diffId, string json, DiffType diffType)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException(_options.General.EmptyError, nameof(json));

            byte[] byteConvert;
            try
            {
                byteConvert = Convert.FromBase64String(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{_options.General.ErrorOnConverting} {ex.Message}");
            }

            var jsonEntity = new JsonEntity
            {
                Id = diffId,
                Json = byteConvert,
                JsonType = diffType
            };

            await _jsonRepository.Save(jsonEntity);
        }

        public async Task<DiffResult> GetDiff(int id)
        {
            var jsonDiff = await _jsonRepository.Get(id);
            if (jsonDiff == null)
                return new DiffResult { Message = $"{_options.General.NoDiffFound} {id}" };

            if (jsonDiff.Count < 2)
                return new DiffResult { Message = $"{_options.General.NoJsonForDiff} {id}" };

            return await Task.Run(() => _diffService.GetDiff(jsonDiff));
        }
    }
}
