using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazingPizza.Shared;

namespace BlazingPizza.Client
{
  public class OrdersClient
  {

    private readonly HttpClient _httpClient;

    public OrdersClient(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderWithStatus>> GetOrdersAsync()
    {
      return await _httpClient.GetFromJsonAsync<IEnumerable<OrderWithStatus>>("orders");
    }

    public async Task<OrderWithStatus> GetOrderAsync(int OrderId)
    {
      return await _httpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{OrderId}");
    }

    public async Task<int> PlaceOrderAsync(Order order)
    {
      var response = await _httpClient.PostAsJsonAsync("orders", order);
      response.EnsureSuccessStatusCode();
      var orderId = await response.Content.ReadFromJsonAsync<int>();
      return orderId;

    }





  }
}