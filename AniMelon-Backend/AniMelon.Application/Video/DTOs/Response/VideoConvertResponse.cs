using AniMelon.Application.Video.DTOs.Request;

namespace AniMelon.Application.Video.DTOs.Response
{
    public class VideoConvertResponse
    {
        public string hlsUrl { get; set; }
        public List<SubtitleChoice> subtitles { get; set; }
    }
}
