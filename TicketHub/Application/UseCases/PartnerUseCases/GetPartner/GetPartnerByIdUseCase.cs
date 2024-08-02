using Application.Abstracts;
using Domain.Abstracts;
using Domain.Partners;

namespace Application.UseCases.PartnerUseCases.GetPartner
{
    public class GetPartnerByIdUseCase : UseCase<GetPartnerByIdInput, Result<GetPartnerByIdOutput>>
    {
        private readonly IPartnerRepository _partnerRepository;

        public GetPartnerByIdUseCase
        (
            IPartnerRepository partnerRepository
        )
        {
            _partnerRepository = partnerRepository;
        }

        public async override Task<Result<GetPartnerByIdOutput>> Execute(
            GetPartnerByIdInput input, 
            CancellationToken cancellationToken = default)
        {
            Partner partner = await _partnerRepository.GetById(
                input.Id,
                cancellationToken);

            if (partner == null)
            {
                return Result.Failure<GetPartnerByIdOutput>(PartnerErrors.NotFound);
            }

            return new GetPartnerByIdOutput(
                partner.Name,
                partner.Email,
                partner.Cnpj
            );
        }
    }
}
