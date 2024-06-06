using BLLayer.Interfaces;
using BLLayer.Repositiories;
using DALayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PLayer.Controllers
{
	//[Authorize]
	public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitofWork _unitofWork;
      
        public DepartmentController(
            ILogger<DepartmentController> logger,
         
           IUnitofWork unitofWork )
        {
            _logger = logger;
            _unitofWork = unitofWork;
       
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var department = await _unitofWork.DepartmentRepository.GetAllAsync();
            
             return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
        
            return View(new Department());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            { 
                await _unitofWork.DepartmentRepository.AddAsync(department);
                await _unitofWork.completeAsync();

               
                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var department = await _unitofWork.DepartmentRepository.GetByIdAsync(id);
                if (department == null)
                    return NotFound();
                return View(department);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var department =await _unitofWork.DepartmentRepository.GetByIdAsync(id);
                if (department == null)
                    return NotFound();
                return View(department);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.DepartmentRepository.Update(department);
                await  _unitofWork.completeAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id )
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var department = await _unitofWork.DepartmentRepository.GetByIdAsync(id);
                if (department == null)
                    return NotFound();
                return View(department);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.DepartmentRepository.Delete(department);
                await  _unitofWork.completeAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

    }
}
