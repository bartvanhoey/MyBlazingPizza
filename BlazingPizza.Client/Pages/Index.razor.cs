using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
  public class IndexBase : ComponentBase
  {
    [Inject] HttpClient HttpClient { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] public OrderState OrderState { get; set; }
    protected List<PizzaSpecial> specials;

    protected override async Task OnInitializedAsync()
    {
      specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
    }

    protected async Task PlaceOrder()
    {
      var response = await HttpClient.PostAsJsonAsync("orders", OrderState.Order);
      var newOrderId = await response.Content.ReadFromJsonAsync<int>();
      OrderState.ResetOrder();
      NavigationManager.NavigateTo($"myorders/{newOrderId}");
    }
  }
}