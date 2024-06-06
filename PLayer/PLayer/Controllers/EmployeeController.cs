using AutoMapper;
using BLLayer.Interfaces;
using DALayer.Entities;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLayer.Helper;
using PLayer.Models;

namespace PLayer.Controllers
{
	//[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitofWork unitofWork, ILogger<DepartmentController> logger, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue = "")

        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeVM> employeesvm;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitofWork.EmployeeRepository.GetAllAsync();
                employeesvm = _mapper.Map<IEnumerable<EmployeeVM>>(employees);

            }

            else
            {
                employees =await _unitofWork.EmployeeRepository.Search(SearchValue);
                employeesvm = _mapper.Map<IEnumerable<EmployeeVM>>(employees);
            }


            return View(employeesvm);
        }



        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitofWork.DepartmentRepository.GetAllAsync();
            ViewBag.Message = "Hello From View Bag";
         
            return View(new EmployeeVM());
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM employeevm)
        {
            //ModelState["Department"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {

                var employee = _mapper.Map<EmployeeVM, Employee>(employeevm);

                employee.ImageURL = DocumentSetting.UploadFile(employeevm.Image, "image");

                await _unitofWork.EmployeeRepository.AddAsync(employee);
                await _unitofWork.completeAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments =await _unitofWork.DepartmentRepository.GetAllAsync();
            return View(employeevm);

        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var employee = await _unitofWork.EmployeeRepository.GetByIdAsync(id);
                var employeevm = _mapper.Map<EmployeeVM>(employee);

                if (employeevm == null)
                    return NotFound();
                return View(employeevm);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }
        public async Task<IActionResult> Edit(int id)
        { 
            if (id == null)
                    return BadRequest();

            ViewBag.Departments =await _unitofWork.DepartmentRepository.GetAllAsync();
            ViewBag.Message = "Hello From View Bag";

            var employee = await _unitofWork.EmployeeRepository.GetByIdAsync(id);
            var employeevm = _mapper.Map<EmployeeVM>(employee);
            try
            {
                if (employeevm == null)
                    return NotFound();

                return View(employeevm);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVM employeevm)
        {
            // ModelState["Employee"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeevm.Image is not null)
                        employeevm.ImageURL = DocumentSetting.UploadFile(employeevm.Image, "image");
                    var employee = _mapper.Map<EmployeeVM, Employee>(employeevm);
                    _unitofWork.EmployeeRepository.Update(employee);
                   await _unitofWork.completeAsync();
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
            ViewBag.Departments = await _unitofWork.DepartmentRepository.GetAllAsync();
            return View(employeevm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var employee = await _unitofWork.EmployeeRepository.GetByIdAsync(id);
                var employeevm = _mapper.Map<EmployeeVM>(employee);
                if (employeevm == null)
                    return NotFound();
                return View(employeevm);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM employeevm)
        {
            try
            {
                var employee = _mapper.Map<EmployeeVM, Employee>(employeevm);
                _unitofWork.EmployeeRepository.Delete(employee);

                if (await _unitofWork.completeAsync() > 0 && employeevm.ImageURL is not null)

                    DocumentSetting.DeleteFile(employeevm.ImageURL, "image");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

           
        }


    }
}
