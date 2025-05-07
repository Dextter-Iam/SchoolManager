using Microsoft.AspNetCore.Mvc;
using MVCCamiloMentoria.Integracao.Interfaces;
using MVCCamiloMentoria.Integracao.Response;

namespace MVCCamiloMentoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CEPController : Controller
    {
        private readonly IViaCepIntegracao _viaCepIntegracao;


        public CEPController(IViaCepIntegracao viaCepIntegracao)
        {
            _viaCepIntegracao = viaCepIntegracao;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<ViaCepResponse>> ListarDadosDoEndereco(string cep)
        {
           var responseData = await _viaCepIntegracao.ObterDadosViaCep(cep);
            if (responseData == null)
            {
                return BadRequest("CEP NÃO ENTRADO!");
            }
            return Ok(responseData);
        }

    }
}
