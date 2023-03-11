using ExamenSCISA.Models;
using ExamenSCISA.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenSCISA.Controllers
{
    public class UsuarioController : Controller
    {        
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Usuario> usuarios = await _repository.GetUsuarios();
            return View(usuarios);
        }
        
        public async Task<IActionResult> CrearUsuario()
        {
            IEnumerable<TipoUsuario> tiposUsuario = await _repository.GetTiposUsuario();
            ViewBag.TiposUsuario = tiposUsuario
                          .Select(a => new SelectListItem()
                          {
                              Value = a.TipoUsuarioId.ToString(),
                              Text = a.Tipo
                          })
                          .ToList();
            return View();
        }

        public async Task<IActionResult> GuardarUsuario(Usuario usuario)
        {
            if(usuario.UsuarioId == 0)			
                await _repository.CrearUsuario(usuario);                                          
            else
                await _repository.EditarUsuario(usuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditarUsuario(int usuarioId)
        {
            var usuario = _repository.GetUsuarioById(usuarioId).Result;
            IEnumerable<TipoUsuario> tiposUsuario = await _repository.GetTiposUsuario();
            ViewBag.TiposUsuario = tiposUsuario
                          .Select(a => new SelectListItem()
                          {
                              Value = a.TipoUsuarioId.ToString(),
                              Text = a.Tipo
                          })
                          .ToList();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int usuarioId)
        {
            _repository.EliminarUsuario(usuarioId);            
             return Json("OK");                
        }
    }
}
