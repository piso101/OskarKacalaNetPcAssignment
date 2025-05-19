'use client';

import { useState, useEffect } from 'react';
import API_BASE_URL from '../config';

interface Category {
    id: number;
    name: string;
}

interface Subcategory {
    id: number;
    name: string;
}

interface Contact {
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber?: string;
    categoryId?: number;
    subcategoryId?: number;
    customSubcategory?: string;
    dateOfBirth?: string;
}

interface ContactFormProps {
    contact?: Contact;
    token: string | null;
    onSuccess: (contact: Partial<Contact>) => void;
    onCancel: () => void;
}

export default function ContactForm({ contact, token, onSuccess, onCancel }: ContactFormProps) {
    const [formData, setFormData] = useState<Contact>({
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        categoryId: undefined,
        subcategoryId: undefined,
        customSubcategory: '',
        dateOfBirth: '',
        ...contact,
    });

    const [categories, setCategories] = useState<Category[]>([]);
    const [subcategories, setSubcategories] = useState<Subcategory[]>([]);

    useEffect(() => {
        if (!token) {
            console.warn('Brak tokenu, nie pobieram kategorii');
            return;
        }

        fetch(`${API_BASE_URL}/Categories`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(res => {
                if (!res.ok) throw new Error(`Błąd pobierania kategorii: ${res.status}`);
                return res.json();
            })
            .then(data => {
                setCategories(data);
            })
            .catch(err => {
                console.error('Błąd fetch kategorii:', err);
                setCategories([]);
            });
    }, [token]);

    useEffect(() => {
        if (!token) {
            console.warn('Brak tokenu, nie pobieram podkategorii');
            return;
        }
        if (!formData.categoryId) {
            setSubcategories([]);
            setFormData(prev => ({ ...prev, subcategoryId: undefined }));
            return;
        }

        fetch(`${API_BASE_URL}/Categories/${formData.categoryId}/subcategories`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(res => {
                if (!res.ok) throw new Error(`Błąd pobierania podkategorii: ${res.status}`);
                return res.json();
            })
            .then(data => {
                setSubcategories(data);
            })
            .catch(err => {
                console.error('Błąd fetch podkategorii:', err);
                setSubcategories([]);
            });
    }, [formData.categoryId, token]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: name === 'categoryId' || name === 'subcategoryId' ? (value === '' ? undefined : Number(value)) : value,
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!formData.firstName || !formData.lastName || !formData.email) {
            alert('Proszę uzupełnić imię, nazwisko i email.');
            return;
        }
        if (!formData.categoryId) {
            alert('Proszę wybrać kategorię.');
            return;
        }
        if (!token) {
            alert('Brak tokenu autoryzacyjnego');
            return;
        }

        const cleanData = { ...formData };
        if (!cleanData.categoryId) delete cleanData.categoryId;
        if (!cleanData.subcategoryId) delete cleanData.subcategoryId;
        if (!cleanData.dateOfBirth) delete cleanData.dateOfBirth;
        if (!cleanData.phoneNumber) delete cleanData.phoneNumber;
        if (!cleanData.customSubcategory) delete cleanData.customSubcategory;

        const method = contact?.id ? 'PUT' : 'POST';
        const url = contact?.id
            ? `${API_BASE_URL}/Contacts/${contact.id}`
            : `${API_BASE_URL}/Contacts`;

        try {
            const res = await fetch(url, {
                method,
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(cleanData),
            });

            if (!res.ok) {
                const errorData = await res.json();
                alert(errorData.message || 'Błąd zapisu kontaktu');
                return;
            }

            onSuccess(cleanData);
        } catch {
            alert('Błąd sieci');
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{ marginTop: 20 }}>
            <label>
                Imię:<br />
                <input name="firstName" value={formData.firstName} onChange={handleChange} required />
            </label>
            <br />

            <label>
                Nazwisko:<br />
                <input name="lastName" value={formData.lastName} onChange={handleChange} required />
            </label>
            <br />

            <label>
                Email:<br />
                <input type="email" name="email" value={formData.email} onChange={handleChange} required />
            </label>
            <br />

            <label>
                Telefon:<br />
                <input name="phoneNumber" value={formData.phoneNumber || ''} onChange={handleChange} />
            </label>
            <br />

            <label>
                Data urodzenia:<br />
                <input
                    type="date"
                    name="dateOfBirth"
                    value={formData.dateOfBirth ? formData.dateOfBirth.substring(0, 10) : ''}
                    onChange={handleChange}
                />
            </label>
            <br />

            <label>
                Kategoria:<br />
                <select name="categoryId" value={formData.categoryId ?? ''} onChange={handleChange} required>
                    <option value="">-- Wybierz kategorię --</option>
                    {categories.map(cat => (
                        <option key={cat.id} value={cat.id}>
                            {cat.name}
                        </option>
                    ))}
                </select>
            </label>
            <br />

            <label>
                Podkategoria:<br />
                <select
                    name="subcategoryId"
                    value={formData.subcategoryId ?? ''}
                    onChange={handleChange}
                    disabled={!subcategories.length}
                >
                    <option value="">-- Wybierz podkategorię --</option>
                    {subcategories.map(sub => (
                        <option key={sub.id} value={sub.id}>
                            {sub.name}
                        </option>
                    ))}
                </select>
            </label>
            <br />

            <label>
                Podkategoria niestandardowa:<br />
                <input name="customSubcategory" value={formData.customSubcategory || ''} onChange={handleChange} />
            </label>
            <br />

            <button type="submit" style={{ marginRight: 10 }}>
                Zapisz
            </button>
            <button type="button" onClick={onCancel}>
                Anuluj
            </button>
        </form>
    );
}
