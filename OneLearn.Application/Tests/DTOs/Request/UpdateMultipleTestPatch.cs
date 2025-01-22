using Microsoft.AspNetCore.JsonPatch;

namespace OneLearn.Application.Tests.DTOs.Request
{
    public class UpdateMultipleTestPatch
    {
        public int Id { get; set; }
        public JsonPatchDocument<UpdateTestDto> PatchDocument { get; set; }

    }
}
