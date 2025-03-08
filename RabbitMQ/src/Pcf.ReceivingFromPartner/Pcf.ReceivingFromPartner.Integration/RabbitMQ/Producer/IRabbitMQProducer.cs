using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMQ.Producer
{
    public interface IRabbitMQProducer<T>
    {
        Task PublishAsync(T message);
    }
}