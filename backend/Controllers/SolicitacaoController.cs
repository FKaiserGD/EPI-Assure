using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;
using backend.context;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SolicitacaoController : ControllerBase
    {
        IMongoCollection<Solicitacao> _solicitacaoCollection;

        public SolicitacaoController(MongoConnection myConnection, IConfiguration myConfig)
        {

            _solicitacaoCollection = myConnection.context.GetCollection<Solicitacao>("Solicitacoes");
        }

        [HttpGet]
        [Route("GetAllSolicitacoes")]
        public IActionResult GetAllSolicitacoes()
        {
            try
            {
                var solicitacoes = _solicitacaoCollection.AsQueryable().ToList();
                return Ok(solicitacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateNewSolicitacao")]
        public IActionResult CreateNewSolicitacao(Solicitacao newSolicitacao)
        {
            try
            {
                _solicitacaoCollection.InsertOne(newSolicitacao);
                return Ok(newSolicitacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}