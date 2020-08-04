using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazingPizza.ComponentsLibrary.Map
{
  public class MapBase : ComponentBase
  {
    protected string elementId = $"map-{Guid.NewGuid().ToString("D")}";

    [Parameter] public double Zoom { get; set; }
    [Parameter] public List<Marker> Markers { get; set; }
    [Inject] public IJSRuntime JSRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      await JSRuntime.InvokeVoidAsync("deliveryMap.showOrUpdate", elementId, Markers);
    }



  }
}