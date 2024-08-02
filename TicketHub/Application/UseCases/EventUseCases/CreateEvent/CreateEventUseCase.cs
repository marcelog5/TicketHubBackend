using Application.Abstracts;
using Domain.Abstracts;
using Domain.Events;
using Domain.Partners;

namespace Application.UseCases.EventUseCases.CreateEvent
{
    public class CreateEventUseCase : UseCase<CreateEventInput, Result<CreateEventOutput>>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IEventRepository _eventRepository;

        public CreateEventUseCase(
            IPartnerRepository partnerRepository,
            IEventRepository eventRepository)
        {
            _partnerRepository = partnerRepository;
            _eventRepository = eventRepository;
        }

        public async override Task<Result<CreateEventOutput>> Execute(
            CreateEventInput input,
            CancellationToken cancellationToken = default)
        {
            Partner partner = await _partnerRepository.GetById(input.PartnerId);

            if (partner is null)
            {
                return Result.Failure<CreateEventOutput>(PartnerErrors.NotFound);
            }

            Event @event = new Event(
                Guid.NewGuid(),
                input.DateTime,
                input.Name,
                input.TotalSpots,
                partner.Id);

            await _eventRepository.Add(@event);

            return new CreateEventOutput(
                @event.Id,
                @event.Date,
                @event.Name,
                @event.PartnerId);
        }
    }
}
