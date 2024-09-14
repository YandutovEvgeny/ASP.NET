using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using PromoCodeFactory.WebHost.Models.Extensions;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;

        public EmployeesController(
            IRepository<Employee> employeeRepository,
            IRepository<Role> roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEmployeeAsync(EmployeeRequest employeeRequest)
        {
            var employee = await _employeeRepository.CreateAsync();

            var roles = (await _roleRepository.GetAllAsync()).ToList();

            employee.FillEmployee(employeeRequest, roles);

            await _employeeRepository.AddAsync(employee);

            return employee.Id;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<bool>> DeleteEmployeeAsync(Guid id)
        {
            if(!(await _employeeRepository.GetAllAsync()).Any())
                return NoContent();

            var isContains = await _employeeRepository.DeleteByIdAsync(id);

            if (!isContains)
                return NotFound($"Employee with id = {id} not found!");

            return isContains;
        }

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeAsync(Guid id, EmployeeRequest employeeRequest)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
                return NotFound($"Employee with id = {id} not found!");

            var roles = (await _roleRepository.GetAllAsync()).ToList();

            employee.FillEmployee(employeeRequest, roles);

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);

            var employeeModel = new EmployeeResponse()
            {
                Id = updatedEmployee.Id,
                Email = updatedEmployee.Email,
                Roles = updatedEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = updatedEmployee.FullName,
                AppliedPromocodesCount = updatedEmployee.AppliedPromocodesCount
            };

            return employeeModel;
        }
    }
}