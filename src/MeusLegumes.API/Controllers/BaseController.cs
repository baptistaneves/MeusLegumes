namespace MeusLegumes.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private readonly INotifier _notifier;

    public BaseController(INotifier notifier)
    {
        _notifier = notifier;
    }


    [NonAction]
    protected ActionResult Response(object result = null)
    {
        if (OperationIsValid())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            data = GetMessageErrors()
        });
    }

    [NonAction]
    protected bool OperationIsValid()
    {
        return !_notifier.HasNotifications();
    }

    [NonAction]
    protected void Notify(string message)
    {
        _notifier.Handle(new Notification(message));
    }

    [NonAction]
    private IEnumerable<string> GetMessageErrors()
    {
        return _notifier.GetNotifications().Select(m => m.Message).ToList();
    }
}
