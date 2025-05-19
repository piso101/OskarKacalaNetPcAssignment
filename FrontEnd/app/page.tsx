'use client';

import { useEffect, useState } from 'react';
import API_BASE_URL from './config';
import ContactList from './components/ContactList';
import ContactForm from './components/ContactForm';

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

export default function Home() {
  const [contacts, setContacts] = useState<Contact[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [editingContact, setEditingContact] = useState<Contact | null>(null);
  const [adding, setAdding] = useState(false);

  const [token, setToken] = useState<string>('');

  useEffect(() => {
    const storedToken = localStorage.getItem('token') || '';
    setToken(storedToken);
  }, []);

  useEffect(() => {
    fetchContacts();
  }, [token]);

  async function fetchContacts() {
    setLoading(true);
    setError(null);
    console.log('Wysyłam zapytanie do backendu, token:', token);

    try {
      const headers: Record<string, string> = {};
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }

      const res = await fetch(`${API_BASE_URL}/Contacts`, { headers });
      console.log('Odpowiedź z backendu:', res.status);

      if (res.status === 401) {
        logout();
        return;
      }

      if (!res.ok) {
        throw new Error(`Błąd serwera: ${res.status}`);
      }

      const data = await res.json();
      console.log('Pobrane kontakty:', data);
      setContacts(data);
    } catch (err: any) {
      setError(err.message || 'Błąd sieci');
      setContacts([]);
    } finally {
      setLoading(false);
    }
  }

  function logout() {
    localStorage.removeItem('token');
    setToken('');
    setLoading(false);
    alert('Wylogowano. Zaloguj się ponownie.');
  }

  const handleEdit = (contact: Contact) => {
    setEditingContact(contact);
    setAdding(false);
  };

  const handleDelete = async (id: number) => {
    if (!token) return alert('Brak autoryzacji');
    if (!confirm('Na pewno chcesz usunąć ten kontakt?')) return;

    try {
      const headers: Record<string, string> = {};
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }

      const res = await fetch(`${API_BASE_URL}/Contacts/${id}`, {
        method: 'DELETE',
        headers,
      });

      if (res.status === 401) {
        logout();
        return;
      }

      if (!res.ok) throw new Error('Błąd przy usuwaniu');

      await fetchContacts();
    } catch (err: any) {
      alert(err.message || 'Błąd usuwania');
    }
  };

  const handleAddClick = () => {
    setAdding(true);
    setEditingContact(null);
  };

  const handleFormSuccess = () => {
    setAdding(false);
    setEditingContact(null);
    fetchContacts();
  };

  const handleFormCancel = () => {
    setAdding(false);
    setEditingContact(null);
  };

  if (loading) return <p>Ładowanie kontaktów...</p>;
  if (error) return <p style={{ color: 'red' }}>Błąd: {error}</p>;

  return (
    <main>
      <h1>Lista kontaktów</h1>

      {!token && (
        <p>
          <a href="/login">Zaloguj się, aby edytować kontakty</a>
        </p>
      )}

      <ContactList contacts={contacts} token={token} onEdit={handleEdit} onDelete={handleDelete} />

      {token && !adding && !editingContact && (
        <button onClick={handleAddClick} style={{ marginTop: 20 }}>
          Dodaj kontakt
        </button>
      )}

      {(adding || editingContact) && token && (
        <ContactForm
          contact={editingContact || undefined}
          token={token}
          onSuccess={handleFormSuccess}
          onCancel={handleFormCancel}
        />
      )}
    </main>
  );
}
