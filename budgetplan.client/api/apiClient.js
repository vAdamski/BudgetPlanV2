const apiUrl = import.meta.env.VITE_API_URL?.replace(/\/$/, "");

if (!apiUrl) {
  throw new Error("Brak zmiennej VITE_API_URL.");
}

export class ApiError extends Error {
  constructor(status, body) {
    super(`API request failed with status ${status}`);

    this.name = "ApiError";
    this.status = status;
    this.body = body;
  }
}

async function readResponse(response) {
  if (response.status === 204) {
    return null;
  }

  const text = await response.text();

  if (!text) {
    return null;
  }

  const contentType = response.headers.get("content-type") ?? "";

  if (contentType.includes("application/json")) {
    return JSON.parse(text);
  }

  return text;
}

export async function apiFetch(path, options = {}) {
  const headers = new Headers(options.headers);

  if (
    options.body &&
    !(options.body instanceof FormData) &&
    !headers.has("Content-Type")
  ) {
    headers.set("Content-Type", "application/json");
  }

  const response = await fetch(`${apiUrl}${path}`, {
    ...options,
    headers,

    // Wysyła cookie także do API działającego na innym originie.
    credentials: "include",
  });

  const body = await readResponse(response);

  if (!response.ok) {
    throw new ApiError(response.status, body);
  }

  return body;
}

export function getApiErrorMessage(error) {
  if (error?.body?.errors) {
    return Object.values(error.body.errors).flat().join(" ");
  }

  return (
    error?.body?.detail ??
    error?.body?.title ??
    error?.message ??
    "Wystąpił nieoczekiwany błąd."
  );
}
