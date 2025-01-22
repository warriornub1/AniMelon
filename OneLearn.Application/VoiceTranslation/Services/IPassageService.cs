using OneLearn.Application.VoiceTranslation.DTOs.Passage.Request;
using OneLearn.Application.VoiceTranslation.DTOs.Passage.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLearn.Application.VoiceTranslation.Services
{
    public interface IPassageService
    {
        Task<IEnumerable<GetAllPassageResponse>> GetAllPassageAsync();

        Task CreatePassageAsync(string userId, CreatePassageRequest request);
        Task<GetPassageResponse> GetPassageByIdAsync(int id);
    }
}
