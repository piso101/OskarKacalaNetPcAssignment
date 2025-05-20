'use client'; // Wymusza tryb klienta – wymagany do użycia hooków w komponencie AuthForm

import AuthForm from './../components/AuthForm';

/// <summary>
/// Strona logowania aplikacji NetPC.
/// Renderuje komponent formularza logowania użytkownika.
/// </summary>
export default function Login() {
    return <AuthForm />;
}