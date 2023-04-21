using System.Globalization;
using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class ApiException : Exception
{
	protected ApiException(SerializationInfo info, StreamingContext streamingContext) : base(info, streamingContext)
	{
		
	}
	public ApiException() 
	{
	}

	public ApiException(string message) : base(message)
	{
	}

	public ApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
	{
	}
}