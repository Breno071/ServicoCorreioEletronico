using Microsoft.JSInterop;

namespace ClientApp.Components.Pages
{
    public partial class Catalog
    {
        //private IJSObjectReference module;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/Catalog.razor.js");
            }
        }
    }
}
