namespace Application.Parameters;

public class RequestParameter
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }

	public RequestParameter()
	{
		PageNumber = 0;
		PageSize = 10;
	}

	public RequestParameter(int pageNumber, int pageSize)
	{
		PageNumber = pageNumber < 1 ? 0 : pageNumber;
		PageSize = pageSize < 10 ? 10 : pageSize;
	}
}
