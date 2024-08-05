using Domain.Abstracts;
using Domain.Events;
using Domain.Partners;

namespace Application.UseCases.EventUseCases.CreateEvent
{
    public class CreateEventUseCase : ICreateEventUseCase
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventUseCase(
            IPartnerRepository partnerRepository,
            IEventRepository eventRepository,
            IUnitOfWork unitOfWork)
        {
            _partnerRepository = partnerRepository;
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateEventOutput>> Execute(
            CreateEventInput input,
            CancellationToken cancellationToken = default)
        {
            Partner partner = await _partnerRepository.GetById(input.PartnerId);

            if (partner is null)
            {
                return Result.Failure<CreateEventOutput>(PartnerErrors.NotFound);
            }

            Event @event = Event.Create(
                input.DateTime,
                input.Name,
                input.TotalSpots,
                partner.Id);

            await _eventRepository.Add(@event);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateEventOutput(
                @event.Id,
                @event.Date,
                @event.Name,
                @event.TotalSpots,
                @event.PartnerId);
        }
    }
}
