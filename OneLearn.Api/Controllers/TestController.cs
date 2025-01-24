using Azure.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using OneLearn.Application.Tests.DTOs.Request;
using OneLearn.Application.Tests.Services;

namespace OneLearn.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateMultipleTest request)
        {
            await _testService.UpdateTestAsync(request);
            return Ok();
        }

        /*
         [
          {
            "path": "/studentName",
            "op": "replace",
            "value": "Anil New"
          }
        ]
        */
        [HttpPatch("{id:int}")]

        public async Task<IActionResult> UpdateTestPatch(int id, [FromBody] JsonPatchDocument<UpdateTestDto> patchDocument)
        {
            await _testService.UpdatePatchTestAsync(id, patchDocument);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestMultiple([FromBody] List<UpdateMultipleTest> requests)
        {
            await _testService.UpdateTestMultipleAsync(requests);
            return Ok();
        }

        /*
         [
            {
                "id": 3,
                "patchDocument": [
                    { "op": "replace", "path": "/a", "value": "Updated Name 1" },
                    { "op": "replace", "path": "/b", "value": "30" }
                ]
            },
            {
                "id": 4,
                "patchDocument": [
                    { "op": "replace", "path": "/c", "value": "Updated Name 2" },
                    { "op": "replace", "path": "/d", "value": "40" }
                ]
            }
        ]
         */

        [HttpPatch]
        public async Task<IActionResult> UpdateMultiplePatch([FromBody] List<UpdateMultipleTestPatch> updates)
        {
            await _testService.BulkUpdatePatchAsync(updates);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            var result = await _testService.GetTest();
            return Ok(result);
        }
    }
}
