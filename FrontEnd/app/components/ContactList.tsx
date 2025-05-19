'use client';

import React from 'react';

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

interface Props {
    contacts: Contact[];
    token: string | null;
    onEdit: (contact: Contact) => void;
    onDelete: (id: number) => void;
}

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
