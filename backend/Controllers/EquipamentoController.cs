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
    }
}