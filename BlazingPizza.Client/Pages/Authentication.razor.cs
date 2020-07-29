using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Pages
{
  public class AuthenticationBase : ComponentBase
  {
    [Parameter] public string Action { get; set; }
    [Inject] public OrderState OrderState { get; set; }
    public PizzaAuthenticationState RemoteAuthenticationState { get; set; } = new PizzaAuthenticationState();

    protected override void OnInitialized()
    {
      if (RemoteAuthenticationActions.IsAction(RemoteAuthenticationActions.LogIn, Action))
      {
        RemoteAuthenticationState.Order = OrderState.Order;
      }
    }

    protected void RestorePizza(PizzaAuthenticationState pizzaState)
    {
        if (pizzaState.Order != null)
        {
            OrderState.ReplaceOrder(pizzaState.Order);
        }
    }
  }
}