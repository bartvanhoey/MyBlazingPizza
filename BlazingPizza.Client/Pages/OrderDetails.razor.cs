using System;
using System.Threading;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;


namespace BlazingPizza.Client.Pages
{
  public class OrderDetailsBase : ComponentBase, IDisposable
  {
    [Parameter] public int OrderId { get; set; }

    [Inject] public OrdersClient OrdersClient { get; set; }

    protected OrderWithStatus orderWithStatus;
    protected bool invalidOrder;
    protected CancellationTokenSource pollingCancellationToken;

    protected async override Task OnParametersSetAsync()
    {
      // If we were already polling for a different order, stop doing so
      pollingCancellationToken?.Cancel();

      // Start a new poll loop
      await PollForUpdates();
    }

    private async Task PollForUpdates()
    {
      pollingCancellationToken = new CancellationTokenSource();
      while (!pollingCancellationToken.IsCancellationRequested)
      {
        try
        {
          invalidOrder = false;
          orderWithStatus = await OrdersClient.GetOrderAsync(OrderId);
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