using System.Net;

namespace OneLearn.Application.Response
{
    public class APIResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
