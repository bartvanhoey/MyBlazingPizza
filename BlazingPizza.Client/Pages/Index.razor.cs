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
    protected List<PizzaSpecial> specials;
    protected Pizza configuringPizza;
    protected bool showingConfigureDialog;
    protected Order order = new Order();

    protected override async Task OnInitializedAsync()
    {
      specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
    }

    protected void ShowConfigurePizzaDialog(PizzaSpecial special)
    {
      configuringPizza = new Pizza
      {
        Special = special,
        SpecialId = special.Id,
        Size = Pizza.DefaultSize,
        Toppings = new List<PizzaTopping>()
      };
      showingConfigureDialog = true;
    }

    protected void CancelConfigurePizzaDialog()
    {
      configuringPizza = null;
      showingConfigureDialog = false;
    }

    protected void ConfirmConfigurePizzaDialog()
    {
      order.Pizzas.Add(configuringPizza);
      configuringPizza = null;
      showingConfigureDialog = false;
    }

    protected void RemoveConfiguredPizza(Pizza pizza)
    {
      order.Pizzas.Remove(pizza);
    }

    protected async Task PlaceOrder()
    {
      var response = await HttpClient.PostAsJsonAsync("orders", order);
      var newOrderId = await response.Content.ReadFromJsonAsync<int>();
      order = new Order();
      NavigationManager.NavigateTo($"myorders/{newOrderId}");
    }
  }
}