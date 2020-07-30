using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazingPizza.Client.Shared
{
    public class LoginDisplayBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public SignOutSessionStateManager SignOutManager { get; set; }

        protected async Task BeginSignOutAsync()
        {
            await SignOutManager.SetSignOutState();
            NavigationManager.NavigateTo("authentication/logout");
        }

    }
}