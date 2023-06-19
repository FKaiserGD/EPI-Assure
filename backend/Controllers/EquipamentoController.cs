using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;
using backend.context;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EquipamentoController : ControllerBase
    {
        IMongoCollection<Equipamento> _equipamentosCollection;

        public EquipamentoController(MongoConnection myConnection, IConfiguration myConfig)
        {
            //var CollectionString = myConfig["MongoDatabases:EPIAssure:Collections:Equipamentos"];

            _equipamentosCollection = myConnection.context.GetCollection<Equipamento>("Equipamentos");
        }

        [HttpGet]
        [Route("GetAllEquipamentos")]
        public IActionResult GetAllEquipamentos()
        {
            try
            {
                var equips = _equipamentosCollection.AsQueryable().ToList();
                return Ok(equips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateNewEquipamento")]
        public IActionResult CreateNewEquipamento(Equipamento newEquipamento)
        {
            try
            {
                _equipamentosCollection.InsertOne(newEquipamento);
                return Ok(newEquipamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEquipamento/{id}")]
        public IActionResult UpdateEquipamento(string id, Equipamento updatedEquipamento)
        {
            try
            {
                var filter = Builders<Equipamento>.Filter.Eq("_id", id);
                var result = _equipamentosCollection.ReplaceOne(filter, updatedEquipamento);

                if (result.ModifiedCount == 1)
                    return Ok(updatedEquipamento);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEquipamento/{id}")]
        public IActionResult DeleteEquipamento(string id)
        {
            try
            {
                var filter = Builders<Equipamento>.Filter.Eq("_id", id);
                var result = _equipamentosCollection.DeleteOne(filter);

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