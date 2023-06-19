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
        [HttpPut]
        [Route("UpdateSolicitacao/{id}")]
        public IActionResult UpdateSolicitacao(string id, Solicitacao updatedSolicitacao)
        {
            try
            {
                var filter = Builders<Solicitacao>.Filter.Eq("_id", id);
                var result = _solicitacaoCollection.ReplaceOne(filter, updatedSolicitacao);

                if (result.ModifiedCount == 1)
                    return Ok(updatedSolicitacao);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteSolicitacao/{id}")]
        public IActionResult DeleteSolicitacao(string id)
        {
            try
            {
                var filter = Builders<Solicitacao>.Filter.Eq("_id", id);
                var result = _solicitacaoCollection.DeleteOne(filter);

                if (result.DeletedCount == 1)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}