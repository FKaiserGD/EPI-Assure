using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using backend.models;
using backend.context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioController(MongoConnection myConnection, IConfiguration myConfig)
        {
            _usuarioCollection = myConnection.context.GetCollection<Usuario>("Usuarios");
        }

        [HttpGet]
        [Route("GetAllUsuarios")]
        public IActionResult GetAllUsuarios()
        {
            try
            {
                var usuarios = _usuarioCollection.Find(_ => true).ToList();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsuarioById/{id}")]
        public IActionResult GetUsuarioById(string id)
        {
            try
            {
                var usuario = _usuarioCollection.Find(u => u.Id == id).FirstOrDefault();

                if (usuario != null)
                    return Ok(usuario);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateUsuario")]
        public IActionResult CreateUsuario(Usuario novoUsuario)
        {
            try
            {
                _usuarioCollection.InsertOne(novoUsuario);
                return Ok(novoUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUsuario/{id}")]
        public IActionResult UpdateUsuario(string id, Usuario usuarioAtualizado)
        {
            try
            {
                usuarioAtualizado.Id = id;

                var filter = Builders<Usuario>.Filter.Eq("_id", id);
                var result = _usuarioCollection.ReplaceOne(filter, usuarioAtualizado);

                if (result.ModifiedCount == 1)
                    return Ok(usuarioAtualizado);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUsuario/{id}")]
        public IActionResult DeleteUsuario(string id)
        {
            try
            {
                var filter = Builders<Usuario>.Filter.Eq("_id", id);
                var result = _usuarioCollection.DeleteOne(filter);

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
