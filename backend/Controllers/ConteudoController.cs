using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;
using backend.context;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ConteudoController : ControllerBase
    {
        IMongoCollection<Conteudo> _conteudoCollection;

        public ConteudoController(MongoConnection myConnection, IConfiguration myConfig)
        {
            _conteudoCollection = myConnection.context.GetCollection<Conteudo>("Conteudos");
        }

        [HttpGet]
        [Route("GetAllConteudos")]
        public IActionResult GetAllConteudos()
        {
            try
            {
                var conteudos = _conteudoCollection.Find(_ => true).ToList();
                return Ok(conteudos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CriarConteudo")]
        public IActionResult CriarConteudo(Conteudo novoConteudo)
        {
            try
            {
                _conteudoCollection.InsertOne(novoConteudo);
                return Ok(novoConteudo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("AtualizarConteudo/{id}")]
        public IActionResult AtualizarConteudo(string id, Conteudo conteudoAtualizado)
        {
            try
            {
                conteudoAtualizado.Id = id;

                var filter = Builders<Conteudo>.Filter.Eq("_id", id);
                var result = _conteudoCollection.ReplaceOne(filter, conteudoAtualizado);

                if (result.ModifiedCount == 1)
                    return Ok(conteudoAtualizado);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("ExcluirConteudo/{id}")]
        public IActionResult ExcluirConteudo(string id)
        {
            try
            {
                var filter = Builders<Conteudo>.Filter.Eq("_id", id);
                var result = _conteudoCollection.DeleteOne(filter);

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
