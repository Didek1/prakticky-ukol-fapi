import { useState, useEffect } from "react";

export const getRates = () =>
{
    const [rates, setRates] = useState({});

    // Načítá kurzy z externího API ČNB a upravuje je do použitelného formátu,
    //klíč je měnový kód, hodnota je kurz vůči CZK
    useEffect(() =>
    {
        fetch("/api/cnb-rates")
            .then(res => res.text())
            .then(text =>
            {
                const rows = text.trim().split("\n").slice(2);
                const data = {};

                for (const row of rows)
                {
                    const [country, currency, amount, code, rate] = row.split("|");
                    data[code] = parseFloat(rate.replace(",", ".")) / parseFloat(amount);
                }

                data["CZK"] = 1;
                setRates(data);
            })
            .catch(err => console.error("Chyba při načítání kurzů:", err));
    }, []);

    const convertFromCZK = (amountCZK, toCurrency) =>
    {
        if (!rates[toCurrency])
        {
            return null
        };

        return amountCZK / rates[toCurrency];
    };

    // Formátuje číslo jako měnovou hodnotu podle zadaného měnového kódu "cs-CZ"
    const formatCurrency = (value, currencyCode) =>
    {
        return new Intl.NumberFormat("cs-CZ", {
            style: "currency",
            currency: currencyCode,
            minimumFractionDigits: 2,
        }).format(value);
    };

    return { rates, convertFromCZK, formatCurrency };
};