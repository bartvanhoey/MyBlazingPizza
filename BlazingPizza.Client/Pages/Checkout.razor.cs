using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
  public class CheckoutBase : ComponentBase
  {
    [Inject] public OrderState OrderState { get; set; }
    [Inject] public HttpClient HttpClient { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected async Task PlaceOrderAsync()
    {
        var response = await HttpClient.PostAsJsonAsync("orders", OrderState.Order);
        var newOrderId = await response.Content.ReadFromJsonAsync<int>();
        OrderState.ResetOrder();
        NavigationManager.NavigateTo($"myorders/{newOrderId}");
    }











  }
}