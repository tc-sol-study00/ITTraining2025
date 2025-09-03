using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPEnshu.Data;
using ASPEnshu.Models;
using ASPEnshu.Models.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASPEnshu {
    public class EmployeesController : Controller {
        private readonly ASPEnshuContext _context;
        private readonly EmployeeServices _employeeServices;
        public EmployeesController(ASPEnshuContext context) {
            _context = context;
            _employeeServices = new EmployeeServices(_context);
        }

        // GET: Employees
        public async Task<IActionResult> Index() {
            List<Employee> result = await _employeeServices.GetAllEmployeesAsync();

            return result.Count > 0 ?
                          View(result) :
                          Problem("Entity set 'ASPEnshuContext.Employee'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id) {
            Employee? employee;
            if ((employee = await _employeeServices.GetEmployeeWithExistCheck(id)) != null) {
                return View(employee);
            }
            else {
                return NotFound();
            }
        }

        // GET: Employees/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNo,EmployeeName,CurrentAddress,BirthDay,Age,Department")] Employee employee) {
            if (ModelState.IsValid) {
                await _employeeServices.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id) {
            Employee? employee;
            if ((employee = await _employeeServices.GetEmployeeWithExistCheck(id)) != null) {
                return View(employee);
            }
            else {
                return NotFound();
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeNo,EmployeeName,CurrentAddress,BirthDay,Age,Department")] Employee employee) {
            if (id != employee.EmployeeNo) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                if (await _employeeServices.UpdateEmployeeAsync(employee)) {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id) {
            Employee? employee;
            if (id == null || (employee = await _employeeServices.GetEmployeeWithExistCheck(id)) == null) {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            if (await _employeeServices.RemoveEmployeeAsync(id)) {
                return RedirectToAction(nameof(Index));
            }
            else {
                return Problem("Entity set 'ASPEnshuContext.Employee'  is null.");
            }
        }

    }
}
