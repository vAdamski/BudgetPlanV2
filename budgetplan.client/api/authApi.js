import { apiFetch } from "./apiClient";

export const authApi = {
  register(data) {
    return apiFetch("/api/auth/register", {
      method: "POST",
      body: JSON.stringify(data),
    });
  },

  login(data) {
    return apiFetch("/identity/login?useCookies=true", {
      method: "POST",
      body: JSON.stringify(data),
    });
  },

  logout() {
    return apiFetch("/api/auth/logout", {
      method: "POST",
    });
  },

  getCurrentUser() {
    return apiFetch("/api/auth/me");
  },
};
