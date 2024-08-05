using Application.UseCases.EventUseCases.CreateEvent;
using Domain.Abstracts;
using Domain.Events;
using Domain.Partners;
using Domain.Shared;
using Moq;

namespace Test.Application.UseCases.EventTests
{
    public class CreateEventUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustCreateEvent()
        {
            // Arrange
            Partner partner = Partner.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cnpj("19.764.730/0001-97"));
            Guid partnerId = partner.Id;

            CreateEventInput createInput = new CreateEventInput(
                    DateTime.Today.AddDays(1),
                    new Name("Event Name"),
                    100,
                    partnerId);

            var partnerRepoMock = new Mock<IPartnerRepository>();
            partnerRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), new CancellationToken()))
                .ReturnsAsync(partner);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(x => x.Add(It.IsAny<Event>(), new CancellationToken()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(1);

            CreateEventUseCase useCase = new CreateEventUseCase(
                partnerRepoMock.Object, 
                eventRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            Result<CreateEventOutput> output = await useCase.Execute(
                createInput, 
                new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Value.Id);
            Assert.Equal(createInput.DateTime, output.Value.Date);
            Assert.Equal(createInput.Name, output.Value.Name);
            Assert.Equal(createInput.TotalSpots, output.Value.TotalSpots);
            Assert.Equal(createInput.PartnerId, output.Value.PartnerId);
        }

        [Fact]
        public async void Execute_WhenPartnerNotFound_MustReturnError()
        {
            // Arrange
            Partner partner = null;

            CreateEventInput createInput = new CreateEventInput(
                    DateTime.Today.AddDays(1),
                    new Name("Event Name"),
                    100,
                    Guid.NewGuid());


            var partnerRepoMock = new Mock<IPartnerRepository>();
            partnerRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), new CancellationToken()))
                .ReturnsAsync(partner);

            var eventRepoMock = new Mock<IEventRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            CreateEventUseCase useCase = new CreateEventUseCase(
                partnerRepoMock.Object,
                eventRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            Result<CreateEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, PartnerErrors.NotFound);
        }
    }
}
