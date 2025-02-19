using L01_2022MS651_2022ZR650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022MS651_2022ZR650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly blogContext _blogContext;

        [HttpGet]
        [Route("ObtenerCalificaciones")]
        public IActionResult Get()
        {
            List<Calificaciones> listadoCalificaciones = (from a in _blogContext.calificaciones select a).ToList();
            if (listadoCalificaciones.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoCalificaciones);
        }

        [HttpGet]
        [Route("ObtenerCalificacionesByPublicacion/{id}")]
        public IActionResult ObtenerCalificacionesByPublicacion(int id)
        {
            List<Calificaciones> listadoCalificaciones = (from a in _blogContext.calificaciones 
                                                          where a.publicacionId == id 
                                                          select a).ToList();
            if (listadoCalificaciones.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoCalificaciones);
        }

        [HttpPost]
        [Route("AgregarCalificaciones")]
        public IActionResult guardarCalificacion([FromBody] Calificaciones calificacion)
        {
            try
            {
                _blogContext.calificaciones.Add(calificacion);
                _blogContext.SaveChanges();
                return Ok(calificacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("actualizarCalificacion/{id}")]
        public IActionResult ActualizarCalificacion(int id, [FromBody] Calificaciones calificacionModificar)
        {
            Calificaciones? calificacionActual = (from a in _blogContext.calificaciones
                                                  where a.calificacionId == id
                                                  select a).FirstOrDefault();

            if (calificacionActual == null)
            {
                return NotFound();
            }
            calificacionActual.publicacionId = calificacionModificar.publicacionId;
            calificacionActual.usuarioId = calificacionModificar.usuarioId;
            calificacionActual.calificacion = calificacionModificar.calificacion;


            _blogContext.Entry(calificacionActual).State = EntityState.Modified;
            _blogContext.SaveChanges();

            return Ok(calificacionModificar);
        }

        [HttpDelete]
        [Route("eliminarCalificacion/{id}")]
        public IActionResult EliminarCalificacion(int id)
        {
            Calificaciones? calificacion = (from a in _blogContext.calificaciones
                            where a.calificacionId == id
                            select a).FirstOrDefault();

            if (calificacion == null)
            {
                return NotFound();
            }

            _blogContext.calificaciones.Attach(calificacion);
            _blogContext.calificaciones.Remove(calificacion);
            _blogContext.SaveChanges();

            return Ok(calificacion);
        }

    }
}
