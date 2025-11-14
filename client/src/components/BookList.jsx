import React, { useState, useEffect } from "react";
import { apiGet } from "../utils/api";
import BookItem from "./BookItem";

export default function BookList({ onQuantityChange })
{
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() =>
    {
        apiGet("/api/books")
            .then((data) => setBooks(data))
            .then(() => setLoading(false));
    }, []);

    const handleQuantityChange = (value) =>
    {
        onQuantityChange(value);
    }

    return (
        <>
            {loading ? <div>Načítám produkty…</div> : (
                <div>
                    <h3>Knihy k objednání</h3>
                    {books.map((book) => (
                        <BookItem
                            key={book.id}
                            book={book}
                            onQuantityChange={handleQuantityChange}
                        />
                    ))}
                </div>
            )}
        </>
    );
};