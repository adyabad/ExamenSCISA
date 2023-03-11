using ExamenSCISA.Models;
using ExamenSCISA.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenSCISA.Controllers
{
    public class CitaController : Controller
    {        
        private readonly ICitaRepository _repository;

        public CitaController(ICitaRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var citas = await _repository.GetCitas();
            return View(citas);
        }

        public async Task<IActionResult> ModalCita()
        {
            return PartialView("_ModalCita");
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCita(Cita cita)
        {           		
            if(await _repository.ValidarFechaCita(cita.Fecha) > 0)
            {
                return Json("Fecha no válida");
            }
            await _repository.CrearCita(cita);
            return Json("OK");
        }

    }
}
