using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Pages
{
  public class CheckoutBase : ComponentBase
  {
    [Inject] public OrderState OrderState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public OrdersClient OrdersClient { get; set; }
    protected bool IsSubmitting;


    protected async Task PlaceOrderAsync()
    {
      IsSubmitting = true;
      try
      {
        var newOrderId = await OrdersClient.PlaceOrderAsync(OrderState.Order);
        OrderState.ResetOrder();
        NavigationManager.NavigateTo($"myorders/{newOrderId}");
      }
      catch (AccessTokenNotAvailableException ex)
      {
        ex.Redirect();
      }


    }











  }
}