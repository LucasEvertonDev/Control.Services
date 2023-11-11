using Authentication.Application.Domain.Structure.Models;
using Notification.Notifications.Helpers;

namespace Authentication.WebApi.Controllers.Base;

public class BaseController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Result(Result result)
    {
        if (result.HasFailures())
        {
            return BadRequest(new ResponseError<Dictionary<object, object[]>>
            {
                Type = "Business rules",
                HttpCode = 400,
                Success = false,
                Errors = ResultServiceHelpers.GetFailures(result)
            });
        }
        else
        {
            if (result.GetContent<dynamic>() == null)
            {
                return Ok(new ResponseDto
                {
                    Success = true
                });
            }
            else
            {
                return Ok(new ResponseDto<dynamic>()
                {
                    Content = result.GetContent<dynamic>()
                });
            }
        }
    }
}
