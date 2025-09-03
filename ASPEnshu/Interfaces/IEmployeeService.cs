using ASPEnshu.Models.DTOs;

namespace ASPEnshu.Models.Services {
    public interface IEmployeeService {

        /*
         * 取り出し系
         */

        /// <summary>
        /// Employee全行取り出し
        /// </summary>
        /// <returns>Employee全行</returns>
        public Task<List<Employee>> GetAllEmployeesAsync();
        /// <summary>
        /// 主キーに該当するEmployeeの存在チェックを行い、
        /// 存在していればEmployeeを返却、なければnullを返却
        /// </summary>
        /// <param name="id"></param>
        /// <returns>存在していればEmployee(一行）を返却、なければnullを返却</returns>
        public Task<Employee?> GetEmployeeWithExistCheck(string id);

        /*
         * 更新系
         */

        /// <summary>
        /// Employeeデータ追加
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Task AddEmployeeAsync(Employee employee);

        /// <summary>
        /// Employeeデータ更新
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Task<bool> UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Employeeデータ削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveEmployeeAsync(string id);

    }
}
