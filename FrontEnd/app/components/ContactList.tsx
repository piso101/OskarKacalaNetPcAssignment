'use client';

import React from 'react';

/// <summary>
/// Model pojedynczego kontaktu wyświetlanego na liście.
/// Dane pochodzą z backendu i są już zmapowane (np. kategoria jako string).
/// </summary>
interface Contact {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber?: string;
    category: string;
    subcategory?: string;
    dateOfBirth?: string;
}

/// <summary>
/// Właściwości komponentu ContactList:
/// - lista kontaktów,
/// - token JWT (warunkowo pokazuje przyciski edycji/usuwania),
/// - funkcje do edycji i usuwania.
/// </summary>
interface Props {
    contacts: Contact[];
    token: string | null;
    onEdit: (contact: Contact) => void;
    onDelete: (id: number) => void;
}

/// <summary>
/// Komponent listy kontaktów.
/// Wyświetla każdy kontakt w postaci listy z możliwością edycji/usuwania (jeśli użytkownik jest zalogowany).
/// </summary>
export default function ContactList({ contacts, token, onEdit, onDelete }: Props) {
    return (
        <ul>
            {contacts.map(c => (
                <li key={c.id} style={{ marginBottom: 10 }}>
                    <strong>
                        {c.firstName} {c.lastName}
                    </strong>
                    <br />
                    Email: {c.email}
                    <br />
                    Telefon: {c.phoneNumber || 'brak'}
                    <br />
                    Kategoria: {c.category} {c.subcategory ? `(${c.subcategory})` : ''}
                    <br />

                    {/* Przycisk edycji i usuwania tylko gdy obecny token (użytkownik zalogowany) */}
                    {token && (
                        <>
                            <button onClick={() => onEdit(c)}>Edytuj</button>{' '}
                            <button onClick={() => onDelete(c.id)}>Usuń</button>
                        </>
                    )}
                </li>
            ))}
        </ul>
    );
}