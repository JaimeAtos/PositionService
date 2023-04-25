using System.Net;
using System.Text.Json;
using Application.Wrappers;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Controllers.Middlewares;

public class ErrorHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public ErrorHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			var response = context.Response;
			response.ContentType = "application/json";

			var responseModel = new Response<string>
			{
				Succeeded = false,
				Message = error.Message,
				Errors = new List<string>()
			};

			switch (error)
			{
				case ArgumentNullException ae:
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					responseModel.Errors.Add(ae.Message);
					break;
				case NpgsqlException:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					responseModel.Errors.Add("Failed to execute sql in database");
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					responseModel.Errors.Add("Internal Server error");
					break;
			}

			var result = JsonSerializer.Serialize(responseModel);
			await response.WriteAsync(result);
		}
	}
}
