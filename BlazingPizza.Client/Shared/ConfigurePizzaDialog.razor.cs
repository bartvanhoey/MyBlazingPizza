using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Shared
{
  public class ConfigurePizzaDialogBase : ComponentBase
  {
    protected List<Topping> toppings;
    [Inject] HttpClient HttpClient { get; set; }
    [Parameter] public Pizza Pizza { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnConfirm { get; set; }

    protected override async Task OnInitializedAsync()
    {
      toppings = await HttpClient.GetFromJsonAsync<List<Topping>>("toppings");
    }

    protected void ToppingSelected(ChangeEventArgs e)
    {
      if (int.TryParse((string)e.Value, out var index) && index >= 0)
      {
        AddTopping(toppings[index]);
      }
    }

    protected void AddTopping(Topping topping)
    {
      if (Pizza.Toppings.Find(pt => pt.Topping == topping) == null)
      {
        Pizza.Toppings.Add(new PizzaTopping { Topping = topping });
      }
    }

    protected void RemoveTopping(Topping topping)
    {
      Pizza.Toppings.RemoveAll(pt => pt.Topping == topping);
    }

  }
}