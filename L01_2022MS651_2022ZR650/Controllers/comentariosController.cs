using L01_2022MS651_2022ZR650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022MS651_2022ZR650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _blogContext;

        [HttpGet]
        [Route("ObtenerComentarios")]
        public IActionResult ObtenerComentarios()
        {
            List<Comentarios> listadoComentarios = (from a in _blogContext.comentarios select a).ToList();
            if (listadoComentarios.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoComentarios);
        }

        [HttpGet]
        [Route("ObtenerComentariosByUser/{id}")]
        public IActionResult ObtenerComentariosByUser(int id)
        {
            List<Comentarios> listadoComents = (from a in _blogContext.comentarios
                                                          where a.usuarioId == id
                                                          select a).ToList();
            if (listadoComents.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoComents);
        }

        [HttpPost]
        [Route("AgregarComentarios")]
        public IActionResult guardarComentarios([FromBody] Comentarios comentario)
        {
            try
            {
                _blogContext.comentarios.Add(comentario);
                _blogContext.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizarComentario/{id}")]
        public IActionResult ActualizarCoementario(int id, [FromBody] Comentarios comentarioModificar)
        {
            Comentarios? comentarioActual = (from a in _blogContext.comentarios
                                                  where a.cometarioId == id
                                                  select a).FirstOrDefault();

            if (comentarioActual == null)
            {
                return NotFound();
            }
            comentarioActual.publicacionId = comentarioModificar.publicacionId;
            comentarioActual.usuarioId = comentarioModificar.usuarioId;
            comentarioActual.comentario = comentarioModificar.comentario;


            _blogContext.Entry(comentarioActual).State = EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(comentarioModificar);
        }

        [HttpDelete]
        [Route("eliminarComentario/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            Comentarios? comentario = (from a in _blogContext.comentarios
                                            where a.cometarioId == id
                                            select a).FirstOrDefault();

            if (comentario == null)
            {
                return NotFound();
            }

            _blogContext.comentarios.Attach(comentario);
            _blogContext.comentarios.Remove(comentario);
            _blogContext.SaveChanges();

            return Ok(comentario);
        }
    }
}
