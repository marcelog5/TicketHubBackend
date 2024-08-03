﻿using Domain.Abstracts;
using Domain.Partners;

namespace Application.UseCases.PartnerUseCases.CreatePartner
{
    public sealed class CreatePartnerUseCase : ICreatePartnerUseCase
    {
        private IPartnerRepository _partnerRepository;

        public CreatePartnerUseCase
        (
            IPartnerRepository partnerRepository
        )
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<Result<CreatePartnerOutput>> Execute(
            CreatePartnerInput input, 
            CancellationToken cancellationToken = default)
        {
            bool partnerExist = await _partnerRepository.AlreadyExist(
                input.Email, input.Cnpj, cancellationToken);

            if (partnerExist)
            {
                return Result.Failure<CreatePartnerOutput>(PartnerErrors.AlreadyExist);
            }

            var partner = new Partner(
                Guid.NewGuid(),
                input.Name,
                input.Email,
                input.Cnpj);

            await _partnerRepository.Add(partner);

            return new CreatePartnerOutput(
                partner.Id,
                partner.Cnpj,
                partner.Email,
                partner.Name);
        }
    }
}
