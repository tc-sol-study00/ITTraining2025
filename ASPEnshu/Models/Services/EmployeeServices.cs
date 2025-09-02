using ASPEnshu.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPEnshu.Models.Services {
    public class EmployeeServices {

        private readonly ASPEnshuContext _context;
        public EmployeeServices(ASPEnshuContext context) {
            _context = context;
        }
        public void AddEmployee(Employee employee) {
            _context.Add(employee);
            _context.SaveChanges();
        }
    }
}
