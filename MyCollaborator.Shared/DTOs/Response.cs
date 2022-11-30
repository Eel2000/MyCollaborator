namespace MyCollaborator.Shared.DTOs;

public class Response<T> where T : class
{
    public Response(Status status, string message)
    {
        Status = status;
        Message = message;
    }

    public Response(Status status, string message, T data)
    {
        Data = data;
        Status = status;
        Message = message;
    }

    public T Data { get; set; }
    public Status Status { get; set; }
    public string Message { get; set; }
}