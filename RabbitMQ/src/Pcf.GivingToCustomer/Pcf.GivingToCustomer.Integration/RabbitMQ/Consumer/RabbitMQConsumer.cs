using MassTransit;
using System.Threading.Tasks;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using System.Linq;
using Pcf.GivingToCustomer.WebHost.Mappers;
using Pcf.GivingToCustomer.WebHost.Models;

namespace Pcf.GivingToCustomer.Integration.RabbitMQ.Consumer
{
    public class RabbitMQConsumer : IConsumer<GivePromoCodeRequest>
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public RabbitMQConsumer(IRepository<PromoCode> promoCodesRepository,
            IRepository<Preference> preferencesRepository, IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }

        public async Task Consume(ConsumeContext<GivePromoCodeRequest> context)
        {
            var request = context.Message;
             //Получаем предпочтение по имени
            var preference = await _preferencesRepository.GetByIdAsync(request.PreferenceId);

            if (preference == null)
            {
                return;
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            PromoCode promoCode = PromoCodeMapper.MapFromModel(request, preference, customers);

            await _promoCodesRepository.AddAsync(promoCode);
        }
    }
}