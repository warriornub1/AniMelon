using AniMelon.Application.Video.DTOs.Request;
using AniMelon.Application.Video.DTOs.Response;

namespace AniMelon.Application.Video.Services
{
    public interface IVideoService
    {
        Task<VideoConvertResponse> ConvertVideo(FileUploadDto upload);
    }
}
