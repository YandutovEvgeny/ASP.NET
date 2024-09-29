using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public CustomersController(
            IRepository<Customer> customerRepository,
            IRepository<Preference> preferenceRepository)
        {
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Получить данные всех клиентов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<CustomerShortResponse>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            var result = customers.Select(x => new CustomerShortResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            });

            return Ok(result);
        }

        /// <summary>
        /// Получить клиента по id
        /// </summary>
        /// <param name="id">id клиента</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            var result = new CustomerResponse(customer);

            return Ok(result);
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="request">Данные клиента</param>
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var preferences = await _preferenceRepository.GetListByIdsAsync(request.PreferenceIds);

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            customer.Preferences = preferences.Select(x => new CustomerPreference
            {
                Customer = customer,
                Preference = x
            }).ToList();

            await _customerRepository.AddAsync(customer);

            return Ok();

        }

        /// <summary>
        /// Изменить данные клиента
        /// </summary>
        /// <param name="id">id клиента</param>
        /// <param name="request">Обновленные данные клиента</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            var preferences = await _preferenceRepository.GetListByIdsAsync(request.PreferenceIds);

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.Preferences.Clear();
            customer.Preferences = preferences.Select(x => new CustomerPreference
            {
                Customer = customer,
                Preference = x
            }).ToList();

            await _customerRepository.UpdateAsync(customer);

            return NoContent();
        }

        /// <summary>
        /// Удалить клиента по id
        /// </summary>
        /// <param name="id">id клиента</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return NotFound();

            await _customerRepository.DeleteAsync(customer);

            return NoContent();
        }
    }
}