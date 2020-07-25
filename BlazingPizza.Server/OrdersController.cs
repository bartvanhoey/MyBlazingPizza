using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Server
{
  [Route("orders")]
  [ApiController]
  // [Authorize]
  public class OrdersController : Controller
  {
    private readonly PizzaStoreContext _db;

    public OrdersController(PizzaStoreContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
    {
      var orders = await _db.Orders
          // .Where(o => o.UserId == GetUserId())
          .Include(o => o.DeliveryLocation)
          .Include(o => o.Pizzas).ThenInclude(p => p.Special)
          .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
          .OrderByDescending(o => o.CreatedTime)
          .ToListAsync();
      return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
    }

       [HttpPost]
        public async Task<ActionResult<int>> PlaceOrder(Order order)
        {
            order.CreatedTime = DateTime.Now;
            order.DeliveryLocation = new LatLong(51.5001, -0.1239);
            // order.UserId = GetUserId();

            // Enforce existence of Pizza.SpecialId and Topping.ToppingId
            // in the database - prevent the submitter from making up
            // new specials and toppings
            foreach (var pizza in order.Pizzas)
            {
                pizza.SpecialId = pizza.Special.Id;
                pizza.Special = null;

                foreach (var topping in pizza.Toppings)
                {
                    topping.ToppingId = topping.Topping.Id;
                    topping.Topping = null;
                }
            }

            _db.Orders.Attach(order);
            await _db.SaveChangesAsync();

            // In the background, send push notifications if possible
            // var subscription = await _db.NotificationSubscriptions.Where(e => e.UserId == GetUserId()).SingleOrDefaultAsync();
            // if (subscription != null)
            // {
            //     _ = TrackAndSendNotificationsAsync(order, subscription);
            // }

            return order.OrderId;
        }

  }
}