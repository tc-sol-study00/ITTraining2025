using ASPEnshu.Data;
using ASPEnshu.Models;
using ASPEnshu.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace ASPEnshu.Models.Services {
    public class EmployeeServices {

        private readonly ASPEnshuContext _context;
        public EmployeeServices(ASPEnshuContext context) {
            _context = context;
        }

        /// <summary>
        /// Employee全行取り出し
        /// </summary>
        /// <returns>Employee全行</returns>
        public async Task<List<Employee>> GetAllEmployeesAsync() =>
            await _context.Employee.ToListAsync();

        /// <summary>
        /// Employeeテーブルのデータ存在チェック
        /// </summary>
        /// <returns>存在していればtrueしていなければfalse</returns>
        private async Task<bool> EmployeeExistCheckAsync() =>
            _context.Employee == null ? false : true;

        /// <summary>
        /// 主キー(id)指定された場合のEmployee取り出し（１行）
        /// </summary>
        /// <param name="id">主キー</param>
        /// <returns>Employee（１行）</returns>
        public async Task<Employee?> GetEmployeeByIdAsync(string id) =>
            await _context.Employee.FirstOrDefaultAsync(m => m.EmployeeNo == id);

        /// <summary>
        /// 主キーに該当するEmployeeの存在チェック
        /// </summary>
        /// <param name="id">主キー</param>
        /// <returns>存在していればtrue・なければfalse</returns>
        private async Task<bool> EmployeeExistsAsync(string id) =>
            await (_context.Employee?.AnyAsync(e => e.EmployeeNo == id) ?? Task.FromResult(false));

        /// <summary>
        /// 主キーに該当するEmployeeの存在チェックを行い、
        /// 存在していればEmployeeを返却、なければnullを返却
        /// </summary>
        /// <param name="id"></param>
        /// <returns>存在していればEmployee(一行）を返却、なければnullを返却</returns>
        public async Task<Employee?> GetEmployeeWithExistCheck(string id) {
            if (id == null || !await EmployeeExistCheckAsync()) {
                return null;
            }

            Employee employee = await GetEmployeeByIdAsync(id);
            if (employee == null) {
                return null;
            }

            return employee;

        }
        /// <summary>
        /// Employeeデータ追加
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee employee) {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Employeeデータ更新
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmployeeAsync(Employee employee) {

            try {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!await EmployeeExistsAsync(employee.EmployeeNo)) {
                    return false;
                }
                else {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// Employeeデータ削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveEmployeeAsync(string id) {

            if (!await EmployeeExistCheckAsync()) {
                return false;
            }
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null) {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
