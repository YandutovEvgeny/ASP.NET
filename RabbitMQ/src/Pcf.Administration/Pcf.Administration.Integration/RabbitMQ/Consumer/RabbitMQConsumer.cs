using MassTransit;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain.Administration;
using Pcf.Administration.Integration.Models;
using System.Threading.Tasks;

namespace Pcf.Administration.Integration.RabbitMQ.Consumer
{
    public class RabbitMQConsumer : IConsumer<Message>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public RabbitMQConsumer(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            var id = context.Message.Id;
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return;

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}