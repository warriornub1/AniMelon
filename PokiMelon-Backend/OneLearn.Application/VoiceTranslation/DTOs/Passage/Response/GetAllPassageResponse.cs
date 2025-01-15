namespace OneLearn.Application.VoiceTranslation.DTOs.Passage.Response
{
    public class GetAllPassageResponse
    {
        public int id { get; set; }
        public int langauge_id { get; set; }
        public string title { get; set; }
        public string passage { get; set; }
    }
}
