using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{
    public class AuthenticationBase : ComponentBase
    {
        [Parameter] public string Action { get; set; }

        
    }
}