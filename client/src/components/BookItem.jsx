import React, { useState, useEffect } from "react";

export default function BookItem({ book, onQuantityChange })
{
    const [quantity, setQuantity] = useState(0)

    const handleIncrease = () =>
    {
        setQuantity(prev => prev + 1);
    };

    const handleDecrease = () =>
    {
        if (quantity > 0)
        {
            setQuantity(prev => prev - 1);
        }
    };

    useEffect(() =>
    {
        onQuantityChange({ bookId: book.id, quantity: quantity, totalPrice: (book.price * quantity) });

    }, [quantity]);

    return (
        <div>
            <div className="books-box container">
                <div className="row">
                    <div className="col-10">
                        {book.title} - {book.author} {book.price} Kč
                    </div>

                    <div className="col-2">
                        <span className="m-1 align-middle">{quantity}</span>
                        <button className="btn" onClick={handleIncrease} type="button">+</button>
                        <button className="btn" onClick={handleDecrease} type="button">-</button>
                    </div>
                </div>
            </div>
        </div >
    );
};