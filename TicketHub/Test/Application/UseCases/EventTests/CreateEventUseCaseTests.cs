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
            Guid partnerId = Guid.NewGuid();

            CreateEventInput createInput = new CreateEventInput(
                    DateTime.Now,
                    new Name("Event Name"),
                    100,
                    partnerId);

            Partner partner = new Partner(
                partnerId,
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cnpj("12345678000195"));

            var partnerRepoMock = new Mock<IPartnerRepository>();
            partnerRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), new CancellationToken()))
                .ReturnsAsync(partner);

            var eventRepoMock = new Mock<IEventRepository>();
            eventRepoMock.Setup(x => x.Add(It.IsAny<Event>(), new CancellationToken()));

            CreateEventUseCase useCase = new CreateEventUseCase(
                partnerRepoMock.Object, 
                eventRepoMock.Object);

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
                    DateTime.Now,
                    new Name("Event Name"),
                    100,
                    Guid.NewGuid());


            var partnerRepoMock = new Mock<IPartnerRepository>();
            partnerRepoMock
                .Setup(x => x.GetById(It.IsAny<Guid>(), new CancellationToken()))
                .ReturnsAsync(partner);

            var eventRepoMock = new Mock<IEventRepository>();

            CreateEventUseCase useCase = new CreateEventUseCase(
                partnerRepoMock.Object,
                eventRepoMock.Object);

            // Act
            Result<CreateEventOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, PartnerErrors.NotFound);
        }
    }
}
