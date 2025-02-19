using L01_2022MS651_2022ZR650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022MS651_2022ZR650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public usuariosController(blogContext blogContexto)
        {
            _blogContexto = blogContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Usuarios> listadoUsuarios = (from e in _blogContexto.usuarios
                                        select e).ToList();
            if (listadoUsuarios.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult CrearUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                _blogContexto.usuarios.Add(usuario);
                _blogContexto.SaveChanges();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] Usuarios usuarioModificar)
        {
            Usuarios? usuarioActual = (from e in _blogContexto.usuarios
                                  where e.usuarioId == id
                                  select e).FirstOrDefault();



            if (usuarioActual == null)
            {
                return NotFound();
            }

            usuarioActual.rolId = usuarioModificar.rolId;
            usuarioActual.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioActual.clave = usuarioModificar.clave;
            usuarioActual.nombre = usuarioModificar.nombre;
            usuarioActual.apellido = usuarioModificar.apellido;


            _blogContexto.Entry(usuarioActual).State = EntityState.Modified;
            _blogContexto.SaveChanges();


            return Ok(usuarioModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            Usuarios? usuario = (from e in _blogContexto.usuarios
                            where e.usuarioId == id
                            select e).FirstOrDefault();



            if (usuario == null)
            {
                return NotFound();
            }



            _blogContexto.usuarios.Attach(usuario);
            _blogContexto.usuarios.Remove(usuario);
            _blogContexto.SaveChanges();



            return Ok(usuario);
        }

        [HttpGet]
        [Route("GetByNombreApellido/{nombre},{apellido}")]
        public IActionResult GetByNombreApellido(string nombre, string apellido)
        {
            var autor = (from uu in _blogContexto.usuarios
                         where uu.nombre.Equals(nombre) && uu.apellido.Equals(apellido)
                         select uu).ToList();

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpGet]
        [Route("GetByRol/{rol}")]
        public IActionResult GetByRol(string rol)
        {
            var autor = (from uu in _blogContexto.usuarios
                         join rr in _blogContexto.roles
                              on uu.rolId equals rr.rolId
                         where rr.rol == rol
                         select uu).ToList();

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpGet]
        [Route("GetTopN/{N}")]
        public IActionResult GetByRol(int N)
        {
            var usuario = (from u in _blogContexto.usuarios
                         join cc in _blogContexto.comentarios
                              on u.usuarioId equals cc.usuarioId
                         group u  by  cc.usuarioId into grupo
                         orderby grupo.Count() descending
                         select new
                         {
                             Usuario = grupo.Key,
                             Cantidad_Comentario = grupo.Count()
                         }).Take(N).ToList();

            

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
    }
}
