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

        /*
         * 取り出し系
         */

        /// <summary>
        /// Employee全行取り出し
        /// </summary>
        /// <returns>Employee全行</returns>
        public async Task<List<Employee>> GetAllEmployeesAsync() =>
            await _context.Employee.ToListAsync();


        /// <summary>
        /// 主キーに該当するEmployeeの存在チェックを行い、
        /// 存在していればEmployeeを返却、なければnullを返却
        /// </summary>
        /// <param name="id"></param>
        /// <returns>存在していればEmployee(一行）を返却、なければnullを返却</returns>
        public async Task<Employee?> GetEmployeeWithExistCheck(string id) {
            if (string.IsNullOrEmpty(id) || _context.Employee == null) return null;

            return await GetEmployeeByIdAsync(id);
        }
        /// <summary>
        /// 主キー(id)指定された場合のEmployee取り出し（１行）
        /// </summary>
        /// <param name="id">主キー</param>
        /// <returns>Employee（１行）</returns>
        private async Task<Employee?> GetEmployeeByIdAsync(string id) =>
            await _context.Employee.FirstOrDefaultAsync(m => m.EmployeeNo == id);

        /*
         * 更新系
         */
        /// <summary>
        /// Employeeデータ追加
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee employee) {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Employeeデータ更新
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEmployeeAsync(Employee employee) {
            if (employee == null) throw new ArgumentNullException(nameof(employee));

            try {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException) {
                // 対象のEmployeeがまだ存在するか確認
                bool exists = _context.Employee != null &&
                              await _context.Employee.AnyAsync(e => e.EmployeeNo == employee.EmployeeNo);

                if (!exists) {
                    throw;
                }
                return false;
            }
        }


        /// <summary>
        /// Employeeデータ削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveEmployeeAsync(string id) {
            if (string.IsNullOrWhiteSpace(id) || _context.Employee == null)
                return false;

            var employee = await GetEmployeeByIdAsync(id);
            if (employee == null)
                return false;

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
