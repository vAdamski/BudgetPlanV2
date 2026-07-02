import { useState } from "react";
import { Link, useNavigate } from "react-router";

import { getApiErrorMessage } from "../api/apiClient";
import { useAuth } from "../auth/AuthContext";

export default function RegisterPage() {
  const { register } = useAuth();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    email: "",
    displayName: "",
    password: "",
    confirmPassword: "",
  });

  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  function handleChange(event) {
    const { name, value } = event.target;

    setForm((current) => ({
      ...current,
      [name]: value,
    }));
  }

  async function handleSubmit(event) {
    event.preventDefault();

    setError("");

    if (form.password !== form.confirmPassword) {
      setError("Hasła nie są identyczne.");
      return;
    }

    setIsSubmitting(true);

    try {
      await register({
        email: form.email,
        displayName: form.displayName,
        password: form.password,
      });

      navigate("/", {
        replace: true,
      });
    } catch (requestError) {
      setError(getApiErrorMessage(requestError));
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <main>
      <h1>Rejestracja</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="displayName">Nazwa użytkownika</label>

          <input
            id="displayName"
            name="displayName"
            value={form.displayName}
            onChange={handleChange}
            required
          />
        </div>

        <div>
          <label htmlFor="email">E-mail</label>

          <input
            id="email"
            name="email"
            type="email"
            autoComplete="email"
            value={form.email}
            onChange={handleChange}
            required
          />
        </div>

        <div>
          <label htmlFor="password">Hasło</label>

          <input
            id="password"
            name="password"
            type="password"
            autoComplete="new-password"
            value={form.password}
            onChange={handleChange}
            required
          />
        </div>

        <div>
          <label htmlFor="confirmPassword">Powtórz hasło</label>

          <input
            id="confirmPassword"
            name="confirmPassword"
            type="password"
            autoComplete="new-password"
            value={form.confirmPassword}
            onChange={handleChange}
            required
          />
        </div>

        {error && <p role="alert">{error}</p>}

        <button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Tworzenie konta..." : "Utwórz konto"}
        </button>
      </form>

      <p>
        Masz już konto? <Link to="/login">Zaloguj się</Link>
      </p>
    </main>
  );
}
