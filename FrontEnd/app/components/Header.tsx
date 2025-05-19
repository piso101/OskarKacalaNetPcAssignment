'use client';

import Link from 'next/link';
import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

export default function Header() {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const router = useRouter();

    const checkLogin = () => {
        const token = localStorage.getItem('token');
        setIsLoggedIn(!!token);
    };

    useEffect(() => {
        checkLogin();

        // Nasłuchiwanie na zmiany w localStorage (np. z innych kart lub po logowaniu)
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

    const handleLogout = () => {
        localStorage.removeItem('token');
        setIsLoggedIn(false);
        router.push('/login');
    };

    return (
        <header style={{ borderBottom: '1px solid #ccc', paddingBottom: 10, marginBottom: 20 }}>
            <nav style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                    <Link href="/">Strona główna</Link>
                    {' | '}
                    {!isLoggedIn && <Link href="/login">Zaloguj się</Link>}
                </div>
                {isLoggedIn && (
                    <button onClick={handleLogout} style={{ marginLeft: 10 }}>
                        Wyloguj się
                    </button>
                )}
            </nav>
        </header>
    );
}
