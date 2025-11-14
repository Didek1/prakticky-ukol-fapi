import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom"
import { apiPost } from "../utils/api";
import { validateForm } from "../utils/validateForm";
import BookList from "./BookList";

export default function OrderForm()
{
    const [items, setItems] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0);
    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        email: "",
        phone: "",
        address: {
            city: "",
            street: ""
        }
    });
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

    useEffect(() =>
    {
        //Pomoci reduce postupně prochází všechny položky a sčítá jejich totalPrice
        const total = items.reduce((sum, item) => sum + (item.totalPrice || 0), 0);
        setTotalPrice(total);

    }, [items]);

    const handleQuantityChange = (value) =>
    {
        const { bookId, quantity, totalPrice } = value;

        setItems(prevItems =>
        {
            // pokud je quantity 0, smaž položku
            if (quantity === 0)
            {
                return prevItems.filter(item => item.bookId !== bookId)
            }

            //zkontroluje, jestli položka už existuje
            const exist = prevItems.some(item => item.bookId === bookId);

            //pokud položka existuje, aktualizuje ji, jinak přida novou
            return exist
                ? prevItems.map(item => item.bookId === bookId ? { ...item, quantity, totalPrice } : item)
                : [...prevItems, value]
        })

    }

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = { ...form, items };

        const { isValid, errors } = validateForm(formData);

        if (!isValid)
        {
            setErrors(errors);
            return;
        }

        try
        {
            apiPost("/api/order", formData)
                .then((data) => navigate("/summary/" + data.id));

        }
        catch (err)
        {
            setErrors({ submit: "Chyba při odesílání objednávky." });
        }
    }

    return (
        <div className="form-box container mt-2">
            <h1>Objednávkový formulář</h1>

            <form onSubmit={handleSubmit}>

                <div className="row my-5">
                    <div className="col-6 mb-4">
                        <label className="form-label">Jméno</label>
                        <input
                            className="form-control"
                            placeholder="Jan"
                            value={form.firstName}
                            onChange={e => setForm({ ...form, firstName: e.target.value })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.firstName}</span>
                    </div>

                    <div className="col-6 mb-4">
                        <label className="form-label">Přijmení</label>
                        <input
                            className="form-control"
                            placeholder="Novák"
                            value={form.lastName}
                            onChange={e => setForm({ ...form, lastName: e.target.value })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.lastName}</span>
                    </div>

                    <div className="col-6 mb-4">
                        <label className="form-label">Město</label>
                        <input
                            className="form-control"
                            placeholder="Praha 5"
                            value={form.address.city}
                            onChange={e =>  //Adresa v objektu se musi rozbalit aby se mohla aktualizovat
                                setForm({ ...form, address: { ...form.address, city: e.target.value } })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.city}</span>
                    </div>

                    <div className="col-6 mb-4">
                        <label className="form-label">Ulice a č.p.</label>
                        <input
                            className="form-control"
                            placeholder="Na Hřebenkách 47"
                            value={form.street}
                            onChange={e =>
                                setForm({ ...form, address: { ...form.address, street: e.target.value } })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.street}</span>
                    </div>

                    <div className="col-6">
                        <label className="form-label">E-mail</label>
                        <input
                            className="form-control"
                            placeholder="novak@email.cz"
                            value={form.email}
                            onChange={e => setForm({ ...form, email: e.target.value })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.email}</span>
                    </div>

                    <div className="col-6">
                        <label className="form-label">Telefon (volitelné)</label>
                        <input
                            className="form-control"
                            placeholder="123 456 789"
                            value={form.phone}
                            onChange={e => setForm({ ...form, phone: e.target.value })}
                        />
                        <span className="badge text-bg-danger mt-1">{errors.phone}</span>
                    </div>
                </div>

                <BookList onQuantityChange={handleQuantityChange} />
                <span className="badge text-bg-danger mt-1">{errors.items}</span>
                <span className="badge text-bg-danger mt-1">{errors.submit}</span>

                <div className="d-flex justify-content-between">
                    <h2 className="font-bold mt-5">Celková cena: {totalPrice} Kč</h2>
                    <button className="submit btn btn-primary" type="submit">Odeslat</button>
                </div>
            </form >
        </div >
    );
}