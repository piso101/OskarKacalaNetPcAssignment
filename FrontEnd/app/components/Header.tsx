'use client';

import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

/// <summary>
/// Komponent nagłówka (Header) aplikacji.
/// Wyświetla linki nawigacyjne oraz przycisk wylogowania, jeśli użytkownik jest zalogowany.
/// Śledzi zmiany tokena JWT w localStorage w czasie rzeczywistym.
/// </summary>
export default function Header() {
    const [isLoggedIn, setIsLoggedIn] = useState(false); // Stan logowania
    const router = useRouter();                          // Router do przekierowań

    /// Sprawdzenie obecności tokena w localStorage
    const checkLogin = () => {
        const token = localStorage.getItem('token');
        setIsLoggedIn(!!token);
    };

    /// Uruchamia się po załadowaniu komponentu – oraz nasłuchuje zmian tokena z innych zakładek
    useEffect(() => {
        checkLogin();

        // Zmiana w localStorage (np. login/logout w innej karcie)
        const handleStorageChange = (event: StorageEvent) => {
            if (event.key === 'token') {
                checkLogin();
            }
        };

        window.addEventListener('storage', handleStorageChange);

        return () => {
            window.removeEventListener('storage', handleStorageChange);
        };
    }, []);

    /// Wylogowanie użytkownika – usunięcie tokena i przekierowanie
    const handleLogout = () => {
        localStorage.removeItem('token');
        setIsLoggedIn(false);
        router.push('/login');
    };

    return (
        <header style={{ borderBottom: '1px solid #ccc', paddingBottom: 10, marginBottom: 20 }}>
            <nav style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    {/* Link do strony głównej */}
                    <Link href="/">Strona główna</Link>
                    {' | '}
                    {/* Link do logowania – tylko dla niezalogowanych */}
                    {!isLoggedIn && <Link href="/login">Zaloguj się</Link>}
                </div>

                {/* Przycisk wylogowania – tylko dla zalogowanych */}
                {isLoggedIn && (
                    <button onClick={handleLogout} style={{ marginLeft: 10 }}>
                        Wyloguj się
                    </button>
                )}
            </nav>
        </header>
    );
}