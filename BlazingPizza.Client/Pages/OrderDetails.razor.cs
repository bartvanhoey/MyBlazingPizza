using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;


namespace BlazingPizza.Client.Pages
{
  public class OrderDetailsBase : ComponentBase, IDisposable
  {
    [Parameter] public int OrderId { get; set; }
    [Inject] HttpClient HttpClient { get; set; }

    protected OrderWithStatus orderWithStatus;
    protected bool invalidOrder;
    protected CancellationTokenSource pollingCancellationToken;

    protected override void OnParametersSet()
    {
      // If we were already polling for a different order, stop doing so
      pollingCancellationToken?.Cancel();

      // Start a new poll loop
      PollForUpdates();
    }

    private async void PollForUpdates()
    {
      pollingCancellationToken = new CancellationTokenSource();
      while (!pollingCancellationToken.IsCancellationRequested)
      {
        try
        {
          invalidOrder = false;
          orderWithStatus = await HttpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{OrderId}");
          StateHasChanged();

          if (orderWithStatus.IsDelivered)
          {
            pollingCancellationToken.Cancel();
          }
          else
          {
            await Task.Delay(4000);
          }
        }
        catch (Exception ex)
        {
          invalidOrder = true;
          pollingCancellationToken.Cancel();
          Console.Error.WriteLine(ex);
          StateHasChanged();
        }
      }
    }

    void IDisposable.Dispose()
    {
      pollingCancellationToken?.Cancel();
    }
  }
}