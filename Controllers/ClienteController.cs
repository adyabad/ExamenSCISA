using ExamenSCISA.Models;
using ExamenSCISA.Repositories;
using ExamenSCISA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamenSCISA.Controllers
{
    public class ClienteController : Controller
    {        
        private readonly IClienteRepository _repository;
        private readonly IVehiculoRepository _repositoryVehiculo;

        public ClienteController(IClienteRepository repository, IVehiculoRepository repositoryVehiculo)
        {
            _repository = repository;
            _repositoryVehiculo = repositoryVehiculo;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Cliente> clientes = await _repository.GetClientes();
            return View(clientes);
        }
        
        public async Task<IActionResult> CrearCliente()
        {            
            return View();
        }

        public async Task<IActionResult> GuardarCliente(Cliente cliente)
        {
            if(cliente.ClienteId == 0)
                await _repository.CrearCliente(cliente);
            else
                await _repository.EditarCliente(cliente);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditarCliente(int clienteId)
        {
            var cliente = _repository.GetClienteById(clienteId).Result;            
            return View(cliente);
        }

        public async Task<IActionResult> DetalleCliente(int clienteId)
        {
            var detalleCliente = new DetalleCliente();
            detalleCliente.Cliente = await _repository.GetClienteById(clienteId);
            detalleCliente.Vehiculos = await _repositoryVehiculo.GetVehiculosByIdCliente(clienteId);
            return View(detalleCliente);
        }

        [HttpPost]
        public ActionResult EliminarCliente(int clienteId)
        {
            _repository.EliminarCliente(clienteId);            
             return Json("OK");                
        }

        public async Task<IActionResult> ModalVehiculo()
        {            
            return PartialView("_ModalVehiculo");
        }

        [HttpPost]
        public ActionResult GuardarVehiculo(Vehiculo vehiculo)
        {
            _repositoryVehiculo.CrearVehiculo(vehiculo);            
            return Json("OK");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarVehiculo(int vehiculoId)
        {
            await _repositoryVehiculo.EliminarVehiculo(vehiculoId);
            return Json("OK");
        }

    }
}
