using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MVCCamiloMentoria.Integracao.Response;
using Refit;

namespace MVCCamiloMentoria.Integracao.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json/")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
        
    } 
}
