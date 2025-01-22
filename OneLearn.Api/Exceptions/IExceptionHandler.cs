namespace OneLearn.Api.Exceptions
{
    public interface IExceptionHandler
    {
        bool CanHandle(Exception ex);
        Task HandleAsync(HttpContext context, Exception ex);
    }
}
