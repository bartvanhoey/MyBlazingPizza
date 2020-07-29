using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Pages
{

  public class MyOrdersBase : ComponentBase
  {
    [Inject] public OrdersClient OrdersClient { get; set; }

    protected IEnumerable<OrderWithStatus> ordersWithStatus;

    protected override async Task OnParametersSetAsync()
    {
      try
      {
          ordersWithStatus = await OrdersClient.GetOrdersAsync();
      }
      catch (AccessTokenNotAvailableException ex)
      {
          ex.Redirect();
      }
    }

  }
}