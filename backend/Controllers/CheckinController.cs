using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;
using backend.context;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CheckinController : ControllerBase
    {
        IMongoCollection<Checkin> _checkinCollection;

        public CheckinController(MongoConnection myConnection, IConfiguration myConfig)
        {

            _checkinCollection = myConnection.context.GetCollection<Checkin>("Checkin");
        }

        [HttpGet]
        [Route("GetAllCheckin")]
        public IActionResult GetAllCheckin()
        {
            try
            {
                var checkins = _checkinCollection.AsQueryable().ToList();
                return Ok(checkins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("CreateNewCheckin")]
        public IActionResult CreateNewCheckin(Checkin newCheckin)
        {
            try
            {
                _checkinCollection.InsertOne(newCheckin);
                return Ok(newCheckin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}