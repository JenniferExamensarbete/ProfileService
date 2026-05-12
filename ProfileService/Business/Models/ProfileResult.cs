namespace ProfileService.Business.Models;

public class ProfileResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
}

public class ProfileResult<T> : ProfileResult
{
    public T? Result { get; set; }
}