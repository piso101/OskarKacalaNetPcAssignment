'use client';

import { useEffect, useState } from 'react';
import API_BASE_URL from './config';
import ContactList from './components/ContactList';
import ContactForm from './components/ContactForm';

/// <summary>
/// Interfejs reprezentujący strukturę kontaktu przychodzącego z backendu.
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
/// Komponent strony głównej aplikacji kontaktowej NetPC.
/// Pobiera kontakty z API, umożliwia dodawanie, edycję i usuwanie.
/// Wymaga JWT tokenu do operacji zabezpieczonych.
/// </summary>
export default function Home() {
  const [contacts, setContacts] = useState<Contact[]>([]);         // Lista kontaktów
  const [loading, setLoading] = useState(true);                    // Czy trwa ładowanie danych
  const [error, setError] = useState<string | null>(null);         // Komunikat błędu 

  const [editingContact, setEditingContact] = useState<Contact | null>(null); // Kontakt w trybie edycji
  const [adding, setAdding] = useState(false);                                // Czy aktywne jest dodawanie nowego kontaktu

  const [token, setToken] = useState<string>('');                 // JWT token użytkownika

  /// Pobierz token z localStorage po załadowaniu komponentu 
  useEffect(() => {
    const storedToken = localStorage.getItem('token') || '';
    setToken(storedToken);
  }, []);

  /// Gdy token się zmienia — załaduj kontakty
  useEffect(() => {
    fetchContacts();
  }, [token]);

  /// Pobiera listę kontaktów z backendu
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

  /// Usuwa token i informuje użytkownika o wylogowaniu
  function logout() {
    localStorage.removeItem('token');
    setToken('');
    setLoading(false);
    alert('Wylogowano. Zaloguj się ponownie.');
  }

  /// Obsługuje kliknięcie edycji kontaktu
  const handleEdit = (contact: Contact) => {
    setEditingContact(contact);
    setAdding(false);
  };

  /// Usuwa kontakt z backendu
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

      await fetchContacts(); // Odśwież kontakty
    } catch (err: any) {
      alert(err.message || 'Błąd usuwania');
    }
  };

  /// Przejście do formularza dodawania nowego kontaktu
  const handleAddClick = () => {
    setAdding(true);
    setEditingContact(null);
  };

  /// Po sukcesie formularza (dodanie/edycja) – odśwież listę kontaktów
  const handleFormSuccess = () => {
    setAdding(false);
    setEditingContact(null);
    fetchContacts();
  };

  /// Anulowanie formularza – powrót do listy
  const handleFormCancel = () => {
    setAdding(false);
    setEditingContact(null);
  };

  // Widoki dla stanów ładowania lub błędu
  if (loading) return <p>Ładowanie kontaktów...</p>;
  if (error) return <p style={{ color: 'red' }}>Błąd: {error}</p>;

  return (
    <main>
      <h1>Lista kontaktów</h1>

      {/* Komunikat dla niezalogowanych */}
      {!token && (
        <p>
          <a href="/login">Zaloguj się, aby edytować kontakty</a>
        </p>
      )}

      {/* Lista kontaktów */}
      <ContactList contacts={contacts} token={token} onEdit={handleEdit} onDelete={handleDelete} />

      {/* Przycisk dodawania – tylko jeśli nie edytujemy ani nie dodajemy */}
      {token && !adding && !editingContact && (
        <button onClick={handleAddClick} style={{ marginTop: 20 }}>
          Dodaj kontakt
        </button>
      )}

      {/* Formularz edycji lub dodawania */}
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