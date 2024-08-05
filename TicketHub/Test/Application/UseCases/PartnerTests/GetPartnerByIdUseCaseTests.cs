using Application.UseCases.PartnerUseCases.GetPartner;
using Domain.Abstracts;
using Domain.Partners;
using Domain.Shared;
using Moq;

namespace Test.Application.UseCases.PartnerTests
{
    public class GetPartnerByIdUseCaseTests
    {
        [Fact]
        public async void Execute_WhenCalled_MustGetPartner()
        {
            // Arrange
            Partner Partner = Partner.Create(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                new Cnpj("19.764.730/0001-97"));

            GetPartnerByIdInput createInput = new GetPartnerByIdInput(
                Partner.Id);

            var PartnerRepoMock = new Mock<IPartnerRepository>();
            PartnerRepoMock
                .Setup(x => x.GetById(
                    Partner.Id,
                    new CancellationToken()))
                .ReturnsAsync(Partner);

            GetPartnerByIdUseCase useCase = new GetPartnerByIdUseCase(PartnerRepoMock.Object);

            // Act
            Result<GetPartnerByIdOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(new Name("John Doe"), output.Value.Name);
            Assert.Equal(new Email("john.doe@gmail.com"), output.Value.Email);
            Assert.Equal(new Cnpj("19.764.730/0001-97"), output.Value.Cnpj);
        }

        [Fact]
        public async Task Execute_WhenPartnerNotFound_MustReturnResultError()
        {
            // Arrange
            Partner Partner = null;
            Guid invalidId = Guid.NewGuid();

            GetPartnerByIdInput createInput = new GetPartnerByIdInput(
                invalidId);

            var PartnerRepoMock = new Mock<IPartnerRepository>();
            PartnerRepoMock
                .Setup(x => x.GetById(
                    invalidId,
                    new CancellationToken()))
                .ReturnsAsync(Partner);

            GetPartnerByIdUseCase useCase = new GetPartnerByIdUseCase(PartnerRepoMock.Object);

            // Act
            Result<GetPartnerByIdOutput> output = await useCase.Execute(
                createInput,
                new CancellationToken());

            // Assert
            Assert.Equal(output.Error, PartnerErrors.NotFound);

        }
    }
}
