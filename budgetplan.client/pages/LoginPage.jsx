import { useState } from "react";
import { Link, useLocation, useNavigate } from "react-router";

import { getApiErrorMessage } from "../api/apiClient";
import { useAuth } from "../auth/AuthContext";

export default function LoginPage() {
  const { login } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();

  const [form, setForm] = useState({
    email: "",
    password: "",
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
    setIsSubmitting(true);

    try {
      await login(form);

      const destination = location.state?.from?.pathname ?? "/";

      navigate(destination, {
        replace: true,
      });
    } catch (requestError) {
      if (requestError.status === 401) {
        setError("Nieprawidłowy adres e-mail lub hasło.");
      } else {
        setError(getApiErrorMessage(requestError));
      }
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <main>
      <h1>Logowanie</h1>

      <form onSubmit={handleSubmit}>
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
            autoComplete="current-password"
            value={form.password}
            onChange={handleChange}
            required
          />
        </div>

        {error && <p role="alert">{error}</p>}

        <button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Logowanie..." : "Zaloguj się"}
        </button>
      </form>

      <p>
        Nie masz konta? <Link to="/register">Zarejestruj się</Link>
      </p>
    </main>
  );
}
