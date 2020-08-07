using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.Server
{

  [Route("[controller]")]
  [ApiController]
  [Authorize]
  public class NotificationsController : Controller
  {
    private readonly PizzaStoreContext _db;

    public NotificationsController(PizzaStoreContext db)
    {
      _db = db;
    }

    [HttpPut("subscribe")]
    public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
    {
        var userId = GetUserId();

        var oldSubscriptions = _db.NotificationSubscriptions.Where(e => e.UserId == userId);
        _db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

        subscription.UserId = userId;
        _db.NotificationSubscriptions.Attach(subscription);

        await _db.SaveChangesAsync();
        return subscription;
    }

    private string GetUserId() => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


  }
}