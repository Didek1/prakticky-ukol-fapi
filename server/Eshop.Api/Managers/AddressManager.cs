using AutoMapper;
using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Api.Managers
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;

        public AddressManager(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }

        public AddressDto AddAddress(AddressDto addressDto)
        {
            Address address = mapper.Map<Address>(addressDto);
            Address addedAddress = addressRepository.Insert(address);

            return mapper.Map<AddressDto>(addedAddress);
        }
    }
}
