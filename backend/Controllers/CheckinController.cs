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

        [HttpPut]
        [Route("UpdateCheckin/{id}")]
        public IActionResult UpdateCheckin(string id, Checkin updatedCheckin)
        {
            try
            {
                var filter = Builders<Checkin>.Filter.Eq("_id", id);
                var result = _checkinCollection.ReplaceOne(filter, updatedCheckin);

                if (result.ModifiedCount == 1)
                    return Ok(updatedCheckin);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCheckin/{id}")]
        public IActionResult DeleteCheckin(string id)
        {
            try
            {
                var filter = Builders<Checkin>.Filter.Eq("_id", id);
                var result = _checkinCollection.DeleteOne(filter);

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