using Eshop.Api.Models;

namespace Eshop.Api.Interfaces
{
    public interface IAddressManager
    {
        AddressDto AddAddress(AddressDto address);
    }
}
