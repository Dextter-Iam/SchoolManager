using MVCCamiloMentoria.Integracao.Response;

namespace MVCCamiloMentoria.Integracao.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}
