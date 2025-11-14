
export const validateForm = (data) =>
{
    const errors = {};

    if (!data.firstName?.trim())
    {
        errors.firstName = "Jméno je povinné";
    }

    if (!data.lastName?.trim())
    {
        errors.lastName = "Příjmení je povinné";
    }

    if (!data.address.city?.trim())
    {
        errors.city = "Město je povinné";
    }

    if (!data.address.street?.trim())
    {
        errors.street = "Ulice je povinná";
    }

    if (!data.email?.trim())
    {
        errors.email = "Email je povinný";
    }

    else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.email))
    {
        errors.email = "Neplatný email";
    }

    if (data.phone?.trim())
    {
        if (!/^\+?\d{9,15}$/.test(data.phone))
        {
            errors.phone = "Neplatné telefonní číslo";
        }
    }

    if (data.items.length === 0)
    {
        errors.items = "Musí vybrat alespoň jednu položku";
    }

    return {
        isValid: Object.keys(errors).length === 0,
        errors,
    };
}