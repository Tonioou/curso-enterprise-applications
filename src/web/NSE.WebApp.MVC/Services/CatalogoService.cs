using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class CatalogoService : Service,ICatalogoService
    {
        private readonly HttpClient _httpClient;
        public CatalogoService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri(options.Value.CatalogoUrl);
        }
        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserealizarObjetoResponse<ProdutoViewModel>(response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos");

            TratarErrosResponse(response);

            return await DeserealizarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }
    }
}
