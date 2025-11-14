import { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { apiGet } from "../utils/api";
import { getRates } from "../utils/getRates";
import "../css/Summary.css";

export default function Summary()
{
    const { id } = useParams();
    const { rates, convertFromCZK, formatCurrency } = getRates();
    const [order, setOrder] = useState([]);
    const [currency, setCurrency] = useState("CZK");
    const [totalPriceCZK, setTotalPriceCZK] = useState(0);
    const [displayedTotal, setDisplayedTotal] = useState(0);
    const [loading, setLoading] = useState(true);

    const calculateDph = (order) =>
    {
        if (!order)
        {
            return 0;
        }
        //Pomoci reduce postupně prochází všechny položky a sčítá jejich totalPrice
        const total = order.items.reduce((sum, item) => sum + (item.totalPrice || 0), 0);
        return total * 1.21;
    }

    useEffect(() =>
    {
        if (id)
        {
            apiGet("/api/order/" + id)
                .then((data) =>
                {
                    setOrder(data);
                    const total = calculateDph(data);
                    setTotalPriceCZK(total);
                })
                .catch(err => console.error(err))
                .finally(() => setLoading(false));
        }

    }, [id]);

    //Převod celkové ceny z CZK do vybrané měny při změně měny nebo celkové ceny
    useEffect(() =>
    {
        const converted = convertFromCZK(totalPriceCZK, currency);
        setDisplayedTotal(converted ?? 0);

    }, [totalPriceCZK, currency, rates]);

    return (
        <div>
            {loading ? <div>Načitam data...</div> : (
                <div className="summary border border-2 rounded-4 mx-auto">
                    <h2 className="mb-3">Objednávka byla přijata</h2>

                    <h4>Shrnuti objednavky č.{id}</h4>
                    <ul className="list-group list-group-flush">
                        <li className="list-group-item">Jméno: {order.firstName}</li>
                        <li className="list-group-item">Přijmení: {order.lastName}</li>
                        <li className="list-group-item">Email: {order.email}</li>
                        {order.phone && <li className="list-group-item">Telefon: {order.phone}</li>}
                        <li className="list-group-item">Město: {order.address.city}</li>
                        <li className="list-group-item">Ulice: {order.address.street}</li>
                    </ul>

                    <h5 className="my-2">Položky</h5>
                    <ul className="list-group list-group-flush">
                        {order.items.map(item => (
                            <li key={item.book.id} className="list-group-item">
                                {item.book.title} - {item.quantity}x {item.totalPrice} Kč
                            </li>
                        ))}
                    </ul>

                    <h5 className="my-4">
                        Celková cena včetně DPH {formatCurrency(displayedTotal, currency)}
                    </h5>

                    <div className="d-flex justify-content-between">
                        <Link to={"/order"} className="btn btn-primary">Nová objednavka</Link>

                        <div className="select-wrapper">
                            <select
                                value={currency}
                                onChange={e => setCurrency(e.target.value)}
                                className="form-control"
                            >
                                {Object.keys(rates).map(code => <option key={code} value={code}>{code}</option>)}
                            </select>
                        </div>
                    </div>

                </div>
            )
            }
        </div >
    );
}