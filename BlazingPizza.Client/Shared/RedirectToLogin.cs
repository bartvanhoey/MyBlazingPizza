using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Shared
{
  public class RedirectToLogin : ComponentBase
  {
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
      NavigationManager.NavigateTo($"authentication/login?returnUrl={NavigationManager.Uri}");
    }

  }
}