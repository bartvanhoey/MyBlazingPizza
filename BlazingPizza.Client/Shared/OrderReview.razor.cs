using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Shared
{
    public class OrderReviewBase : ComponentBase
    {
        [Parameter] public Order Order { get; set; }
    }
}