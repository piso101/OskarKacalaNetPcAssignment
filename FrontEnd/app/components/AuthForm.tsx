'use client';

import { useState, useEffect } from 'react';
import API_BASE_URL from '../config';
import { useRouter } from 'next/navigation';

/// <summary>
/// Typ definiujący tryb działania formularza: logowanie lub rejestracja.
/// </summary>
type Mode = 'login' | 'register';

/// <summary>
/// Komponent formularza logowania/rejestracji.
/// Obsługuje walidację formularza, komunikację z API oraz zapis tokena JWT.
/// Automatycznie przekierowuje zalogowanego użytkownika na stronę główną.
/// </summary>
export default function AuthForm() {
    const router = useRouter();

    // Tryb formularza: logowanie lub rejestracja
    const [mode, setMode] = useState<Mode>('login');

    // Pola formularza
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    // Stan błędu oraz ładowania
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    /// Sprawdzenie obecności tokena i przekierowanie, jeśli użytkownik jest już zalogowany
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            router.replace('/');
        }
    }, [router]);

    /// Prosta walidacja pól formularza
    const validate = (): boolean => {
        setError(null);
        if (!email) return setError('Email jest wymagany'), false;
        if (!/\S+@\S+\.\S+/.test(email)) return setError('Email ma niepoprawny format'), false;
        if (!password) return setError('Hasło jest wymagane'), false;
        if (password.length < 6) return setError('Hasło musi mieć co najmniej 6 znaków'), false;
        if (mode === 'register' && password !== confirmPassword)
            return setError('Hasła nie są takie same'), false;
        return true;
    };

    /// Obsługa wysłania formularza (logowanie lub rejestracja)
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!validate()) return;

        setLoading(true);
        setError(null);

        try {
            const endpoint = mode === 'login' ? 'Auth/login' : 'Auth/register';
            const body =
                mode === 'login'
                    ? { email, password }
                    : { email, password, confirmPassword };

            const res = await fetch(`${API_BASE_URL}/${endpoint}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body),
            });

            // Obsługa błędów HTTP
            if (!res.ok) {
                if (res.status === 401) setError('Nieprawidłowy email lub hasło');
                else if (res.status === 400) {
                    const data = await res.json();
                    setError(data.message || 'Błąd walidacji danych');
                } else setError('Błąd serwera');
                setLoading(false);
                return;
            }

            // Po zalogowaniu – zapisz token i przeładuj aplikację
            if (mode === 'login') {
                const data = await res.json();
                localStorage.setItem('token', data.token);

                // Powiadom inne zakładki o zmianie tokena (np. automatyczny refresh)
                window.dispatchEvent(new StorageEvent('storage', {
                    key: 'token',
                    newValue: data.token,
                }));

                router.push('/');
            } else {
                // Po udanej rejestracji – przełącz tryb na logowanie
                alert('Rejestracja zakończona sukcesem! Możesz się teraz zalogować.');
                setMode('login');
            }
        } catch (err: any) {
            setError(err.message || 'Błąd sieci');
        } finally {
            setLoading(false);
        }
    };

    return (
        <main style={{ padding: 20 }}>
            <h1>{mode === 'login' ? 'Zaloguj się' : 'Zarejestruj się'}</h1>

            <form onSubmit={handleSubmit}>
                {/* Email */}
                <label>
                    Email:<br />
                    <input
                        type="email"
                        value={email}
                        onChange={e => setEmail(e.target.value)}
                        required
                    />
                </label>
                <br />

                {/* Hasło */}
                <label>
                    Hasło:<br />
                    <input
                        type="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                </label>
                <br />

                {/* Potwierdzenie hasła – tylko przy rejestracji */}
                {mode === 'register' && (
                    <>
                        <label>
                            Powtórz hasło:<br />
                            <input
                                type="password"
                                value={confirmPassword}
                                onChange={e => setConfirmPassword(e.target.value)}
                                required
                            />
                        </label>
                        <br />
                    </>
                )}

                <br />

                {/* Przycisk submit */}
                <button type="submit" disabled={loading}>
                    {loading
                        ? 'Proszę czekać...'
                        : mode === 'login'
                            ? 'Zaloguj się'
                            : 'Zarejestruj się'}
                </button>
            </form>

            {/* Błąd (jeśli wystąpił) */}
            {error && <p style={{ color: 'red' }}>{error}</p>}

            <br />

            {/* Przełącznik trybu formularza */}
            <button onClick={() => setMode(mode === 'login' ? 'register' : 'login')}>
                {mode === 'login'
                    ? 'Nie masz konta? Zarejestruj się'
                    : 'Masz konto? Zaloguj się'}
            </button>
        </main>
    );
}