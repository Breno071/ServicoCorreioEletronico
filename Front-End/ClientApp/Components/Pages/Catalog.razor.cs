using ClientApp.Models;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientApp.Components.Pages
{
    public partial class Catalog
    {
        private Request Request = new();
        private bool Loading = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/Catalog.razor.js");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5145/api/Adopt/list-pets?specie=dog&age=young&sex=m");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Request = JsonSerializer.Deserialize<Request>(await response.Content.ReadAsStringAsync()) ?? throw new Exception("Falha ao deserializar conteudo do json");

            Request.pets = Request.pets.OrderByDescending(p => p.pet_id).ToList();
            Loading = false;
        }

        //Metodo para verificar se a imagem recebida pode ser carregada
        private bool GetImage(string url)
        {
            using var cliente = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = cliente.Send(request);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                return false;
            }
            return response.IsSuccessStatusCode;
        }
    }
}
