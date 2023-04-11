using Microsoft.AspNetCore.Mvc;

namespace Controllers.v1.Position;

[ApiVersion("1.0")]
public class ReadPositionController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> GetPositionById()
	{
		return Ok(null);
	}
}