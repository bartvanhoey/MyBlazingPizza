using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Shared
{
  public class LoginDisplayBase : ComponentBase
  {

    [Inject] public NavigationManager Navigation { get; set; }
    [Inject] public SignOutSessionStateManager SignOutManager { get; set; }

    protected async Task BeginSignOut()
    {
      await SignOutManager.SetSignOutState();
      Navigation.NavigateTo("authentication/logout");
    }
  }
}