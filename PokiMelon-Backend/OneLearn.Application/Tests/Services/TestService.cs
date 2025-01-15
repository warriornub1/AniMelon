using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OneLearn.Application.Tests.DTOs.Request;
using OneLearn.Application.Tests.Interface;
using OneLearn.Domain.Test;
using static System.Net.Mime.MediaTypeNames;

namespace OneLearn.Application.Tests.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public TestService(ITestRepository testRepository, IMapper mapper, IDistributedCache cache)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task UpdateTestAsync(UpdateMultipleTest request)
        {
            var test = await _testRepository.GetByIdAsync(x => x.id == request.id);

            test.a = request.a;
            test.b = request.b;
            test.c = request.c;
            test.d = request.d;
            test.e = request.e;
            test.f = request.f;
            test.g = request.g;
            test.h = request.h;
            test.i = request.i;
            test.j = request.j;
            test.k = request.k;
            test.l = request.l;
            test.m = request.m;
            test.n = request.n;
            test.last_modified_by = "asd";
            test.last_modified_on = DateTime.Now;
            await _testRepository.UpdateAsAsync(test);
        }
        public async Task UpdatePatchTestAsync(int id, JsonPatchDocument<UpdateTestDto> patch)
        {
            var test = await _testRepository.GetByIdAsync(x => x.id == id);

            if (test == null)
            {
                throw new KeyNotFoundException($"Test with ID {id} not found.");
            }

            var testDto = new UpdateTestDto
            {
                a = test.a,
                b = test.b,
                c = test.c,
                d = test.d,
                e = test.e,
                f = test.f,
                g = test.g,
                h = test.h,
                i = test.i,
                j = test.j,
                k = test.k,
                l = test.l,
                m = test.m,
                n = test.n
            };

            patch.ApplyTo(testDto);

            test.a = testDto.a;
            test.b = testDto.b;
            test.c = testDto.c;
            test.d = testDto.d;
            test.e = testDto.e;
            test.f = testDto.f;
            test.g = testDto.g;
            test.h = testDto.h;
            test.i = testDto.i;
            test.j = testDto.j;
            test.k = testDto.k;
            test.l = testDto.l;
            test.m = testDto.m;
            test.n = testDto.n;
            test.last_modified_by = "asd";
            test.last_modified_on = DateTime.Now;
            await _testRepository.UpdateAsAsync(test);

        }
        public async Task UpdateTestMultipleAsync(List<UpdateMultipleTest> requests)
        {
            //List<Test> testToUpdate = new List<Test>();

            var testInDB = await _testRepository.FindMultiple(x => requests.Select(y => y.id).Contains(x.id));

            foreach(var request in requests)
            {
                var test = testInDB.Where(x => request.id ==  x.id).FirstOrDefault();

                test.a = request.a;
                test.b = request.b;
                test.c = request.c;
                test.d = request.d;
                test.e = request.e;
                test.f = request.f;
                test.g = request.g;
                test.h = request.h;
                test.i = request.i;
                test.j = request.j;
                test.k = request.k;
                test.l = request.l;
                test.m = request.m;
                test.n = request.n;
                test.last_modified_by = "asd";
                test.last_modified_on = DateTime.Now;
            }

            await _testRepository.UpdateRangeAsAsync(testInDB);
        }
        public async Task BulkUpdatePatchAsync(List<UpdateMultipleTestPatch> updates)
        {
            // Extract all IDs to find entities in one query
            var ids = updates.Select(u => u.Id).ToList();

            // Fetch all entities in a single query
            var entities = await _testRepository.FindMultiple(entity => ids.Contains(entity.id));

            if (entities == null || !entities.Any())
                throw new KeyNotFoundException("No matching entities found for the provided IDs.");

            var entityDict = entities.ToDictionary(e => e.id);

            // Prepare for bulk update
            var updatedEntities = new List<Test>();

            foreach (var update in updates)
            {
                if (entityDict.TryGetValue(update.Id, out var entity))
                {

                    var dto = new UpdateTestDto
                    {
                        a = entity.a,
                        b = entity.b,
                        c = entity.c,
                        d = entity.d,
                        e = entity.e,
                        f = entity.f,
                        g = entity.g,
                        h = entity.h,
                        i = entity.i,
                        j = entity.j,
                        k = entity.k,
                        l = entity.l,
                        m = entity.m,
                        n = entity.n
                    };

                    // Apply the patch to DTO
                    update.PatchDocument.ApplyTo(dto);

                    entity.a = dto.a;
                    entity.b = dto.b;
                    entity.c = dto.c;
                    entity.d = dto.d;
                    entity.e = dto.e;
                    entity.f = dto.f;
                    entity.g = dto.g;
                    entity.h = dto.h;
                    entity.i = dto.i;
                    entity.j = dto.j;
                    entity.k = dto.k;
                    entity.i = dto.i;
                    entity.j = dto.j;
                    entity.k = dto.k;
                    entity.l = dto.l;
                    entity.m = dto.m;
                    entity.n = dto.n;
                    entity.last_modified_by = "asd";
                    entity.last_modified_on = DateTime.Now;
                    updatedEntities.Add(entity);
                }
                else
                {
                    throw new KeyNotFoundException($"Entity with ID {update.Id} not found.");
                }
            }

            // Perform a bulk update
            await _testRepository.UpdateRangeAsAsync(updatedEntities);
        }

        public async Task<IEnumerable<Test>> GetTest()
        {
            string key = "test";
            string? cachedMember = await _cache.GetStringAsync(key);
            IEnumerable<Test> tests;
            if (string.IsNullOrEmpty(cachedMember))
            {
                tests = await _testRepository.GetAllAsync();
                if (tests is null)
                    return tests;
                else
                {
                    await _cache.SetStringAsync(key, JsonConvert.SerializeObject(tests));
                    return tests;
                }

            }
            tests = JsonConvert.DeserializeObject<IEnumerable<Test>>(cachedMember);
            return tests;
        }
    }
}
