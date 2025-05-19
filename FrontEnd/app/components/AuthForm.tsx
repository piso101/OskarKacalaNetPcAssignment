'use client';

import { useState, useEffect } from 'react';
import API_BASE_URL from '../config';
import { useRouter } from 'next/navigation';

type Mode = 'login' | 'register';

export default function AuthForm() {
    const router = useRouter();
    const [mode, setMode] = useState<Mode>('login');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    // Jeśli użytkownik ma token, przekieruj na stronę główną
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            router.replace('/');
        }
    }, [router]);

    const validate = (): boolean => {
        setError(null);
        if (!email) {
            setError('Email jest wymagany');
            return false;
        }
        if (!/\S+@\S+\.\S+/.test(email)) {
            setError('Email ma niepoprawny format');
            return false;
        }
        if (!password) {
            setError('Hasło jest wymagane');
            return false;
        }
        if (password.length < 6) {
            setError('Hasło musi mieć co najmniej 6 znaków');
            return false;
        }
        if (mode === 'register' && password !== confirmPassword) {
            setError('Hasła nie są takie same');
            return false;
        }
        return true;
    };

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

            if (!res.ok) {
                if (res.status === 401) setError('Nieprawidłowy email lub hasło');
                else if (res.status === 400) {
                    const data = await res.json();
                    setError(data.message || 'Błąd walidacji danych');
                } else setError('Błąd serwera');
                setLoading(false);
                return;
            }

            if (mode === 'login') {
                const data = await res.json();
                localStorage.setItem('token', data.token);

                window.dispatchEvent(new StorageEvent('storage', {
                    key: 'token',
                    newValue: data.token,
                }));
                router.push('/');
            } else {
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
                <button type="submit" disabled={loading}>
                    {loading
                        ? 'Proszę czekać...'
                        : mode === 'login'
                            ? 'Zaloguj się'
                            : 'Zarejestruj się'}
                </button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <br />
            <button onClick={() => setMode(mode === 'login' ? 'register' : 'login')}>
                {mode === 'login' ? 'Nie masz konta? Zarejestruj się' : 'Masz konto? Zaloguj się'}
            </button>
        </main>
    );
}
