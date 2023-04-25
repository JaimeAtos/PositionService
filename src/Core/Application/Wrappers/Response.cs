namespace Application.Wrappers;

public class Response<T>
{
	public bool Succeeded { get; set; }
	public string? Message { get; set; }
	public List<string>? Errors { get; set; }
	public T Data { get; set; }

	public Response()
	{
	}

	public Response(bool succeeded, T data)
	{
		Succeeded = succeeded;
		Data = data;
		Message = null;
		Errors = new List<string>();
	}

	public Response(T data, string message = "")
	{
		Succeeded = true;
		Message = message;
		Data = data;
		Errors = new List<string>();
	}

	public Response(string message, T data)
	{
		Succeeded = false;
		Message = message;
		Data = data;
		Errors = new List<string>();
	}
}
