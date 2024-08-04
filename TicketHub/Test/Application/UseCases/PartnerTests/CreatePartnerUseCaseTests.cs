using Application.UseCases.PartnerUseCases.CreatePartner;
using Domain.Abstracts;
using Domain.Partners;
using Domain.Shared;
using Moq;

namespace Test.Application.UseCases.PartnerTests
{
    public class CreatePartnerUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustCreatePartner()
        {
            // Arrange
            CreatePartnerInput createInput = new CreatePartnerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cnpj("12345678000195"));

            var PartnerRepoMock = new Mock<IPartnerRepository>();
            PartnerRepoMock
                .Setup(x => x.AlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cnpj("12345678000195"),
                    new CancellationToken()))
                .ReturnsAsync(false);
            PartnerRepoMock.Setup(x => x.Add(It.IsAny<Partner>(), new CancellationToken()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(x => x.SaveChangesAsync(new CancellationToken()))
                .ReturnsAsync(1);

            CreatePartnerUseCase useCase = new CreatePartnerUseCase(
                PartnerRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            Result<CreatePartnerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, output.Value.Id);
            Assert.Equal(new Name("John Doe"), output.Value.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Value.Email);
            Assert.Equal(new Cnpj("12345678000195"), output.Value.Cnpj);
        }

        [Fact]
        public async Task Execute_WhenPartnerAlreadyExist_MustReturnError()
        {
            // Arrange
            CreatePartnerInput createInput = new CreatePartnerInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cnpj("12345678000195"));

            var PartnerRepoMock = new Mock<IPartnerRepository>();
            PartnerRepoMock
                .Setup(x => x.AlreadyExist(
                    new Email("john.doe@gmail.com"),
                    new Cnpj("12345678000195"),
                    It.IsAny<CancellationToken>())) // Use It.IsAny<CancellationToken>() para permitir qualquer token de cancelamento
                .ReturnsAsync(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            CreatePartnerUseCase useCase = new CreatePartnerUseCase(
                PartnerRepoMock.Object, 
                unitOfWorkMock.Object);

            // Act
            Result<CreatePartnerOutput> output = await useCase.Execute(createInput, new CancellationToken());

            // Assert
            Assert.Equal(output.Error, PartnerErrors.AlreadyExist);
        }
    }
}
