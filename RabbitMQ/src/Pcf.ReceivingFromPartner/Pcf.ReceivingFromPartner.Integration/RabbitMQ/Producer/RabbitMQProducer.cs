using MassTransit;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMQ.Producer
{
    public class RabbitMQProducer<T> : IRabbitMQProducer<T>
    {
        private readonly IBus _bus;

        public RabbitMQProducer(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync(T message)
        {
            await _bus.Publish(message);
        }
    }
}