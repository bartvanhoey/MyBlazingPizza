using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazingPizza.Client.Shared
{
    public class AddressEditorBase : ComponentBase
    {
        [Parameter] public Address Address { get; set; }
    }
}