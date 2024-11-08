namespace FFilms.Application.Shared.Response
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
