using Microsoft.AspNetCore.Components;

namespace BlazingPizza.BlazingComponents
{
    public class TemplatedDialogBase : ComponentBase
    {
            [Parameter] public RenderFragment ChildContent { get; set; }
            [Parameter] public bool Show { get; set; }
    }
}