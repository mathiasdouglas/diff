﻿namespace WaesDiff.API.Services
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

        private readonly IDataRepository _dataRepository;

        private readonly IDiffService _diffService;

        public DiffApiService(IOptions<Settings> options, IDataRepository dataRepository, IDiffService diffService)
        {
            _dataRepository = dataRepository;
            _diffService = diffService;
            _options = options.Value;
        }

        public async Task SaveData(int id, string data, DataType dataType)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentException(_options.Messages.EmptyError, nameof(data));

            byte[] byteConvert;
            try
            {
                byteConvert = Convert.FromBase64String(data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{_options.Messages.ErrorOnConverting} {ex.Message}");
            }

            var dataEntity = new DataEntity
            {
                Id = id,
                Data = data,
                DataBase64 = byteConvert,
                DataType = dataType
            };

            await _dataRepository.Save(dataEntity);
        }

        public async Task<DiffResult> GetDiff(int id)
        {
            var jsonDiff = await _dataRepository.Get(id);
            if (jsonDiff == null)
                return new DiffResult { Message = $"{_options.Messages.NoDiffFound} {id}" };

            if (jsonDiff.Count < 2)
                return new DiffResult { Message = $"{_options.Messages.NoDataForDiff} {id}" };

            return await Task.Run(() => _diffService.GetDiff(jsonDiff));
        }
    }
}
