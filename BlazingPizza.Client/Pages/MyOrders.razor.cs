using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Pages
{

  public class MyOrdersBase : ComponentBase
  {

    [Inject] HttpClient HttpClient { get; set; }

    protected IEnumerable<OrderWithStatus> ordersWithStatus;

    protected override async Task OnParametersSetAsync()
    {
      ordersWithStatus = await HttpClient.GetFromJsonAsync<List<OrderWithStatus>>("orders");
    }

  }
}