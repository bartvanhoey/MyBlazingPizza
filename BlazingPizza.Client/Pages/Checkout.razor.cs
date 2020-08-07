using System.Net.Http;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace BlazingPizza.Client.Pages
{
  public class CheckoutBase : ComponentBase
  {
    [Inject] public OrderState OrderState { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public OrdersClient OrdersClient { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    protected bool IsSubmitting;

    protected async Task PlaceOrderAsync()
    {
      IsSubmitting = true;
      try
      {
        var OrderId = await OrdersClient.PlaceOrderAsync(OrderState.Order);
        OrderState.ResetOrder();
        NavigationManager.NavigateTo($"myorders/{OrderId}", true);
      }
      catch (AccessTokenNotAvailableException ex)
      {
        ex.Redirect();
      }
    }

    protected override void OnInitialized()
    {
      _ = RequestNotificationSubscriptionAsync();
    }

    private async Task RequestNotificationSubscriptionAsync()
    {

      var subscription = await JSRuntime.InvokeAsync<NotificationSubscription>("blazorPushNotifications.requestSubscription");
      if (subscription != null)
      {
          try
          {
              await OrdersClient.SubscribeToNotifications(subscription);
          }
          catch (AccessTokenNotAvailableException ex)
          {
              ex.Redirect();
          }
      }
    }
  }
}