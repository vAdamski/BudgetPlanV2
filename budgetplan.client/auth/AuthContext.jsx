import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";

import { authApi } from "../api/authApi";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [isInitializing, setIsInitializing] = useState(true);

  const refreshUser = useCallback(async () => {
    try {
      const currentUser = await authApi.getCurrentUser();

      setUser(currentUser);

      return currentUser;
    } catch (error) {
      if (error.status === 401) {
        setUser(null);
        return null;
      }

      throw error;
    }
  }, []);

  useEffect(() => {
    let isMounted = true;

    async function initialize() {
      try {
        await refreshUser();
      } catch (error) {
        console.error("Nie udało się odtworzyć sesji:", error);

        if (isMounted) {
          setUser(null);
        }
      } finally {
        if (isMounted) {
          setIsInitializing(false);
        }
      }
    }

    initialize();

    return () => {
      isMounted = false;
    };
  }, [refreshUser]);

  const login = useCallback(
    async ({ email, password }) => {
      await authApi.login({
        email,
        password,
      });

      return refreshUser();
    },
    [refreshUser],
  );

  const register = useCallback(
    async ({ email, password, displayName }) => {
      await authApi.register({
        email,
        password,
        displayName,
      });

      // Automatyczne logowanie po rejestracji.
      return login({
        email,
        password,
      });
    },
    [login],
  );

  const logout = useCallback(async () => {
    try {
      await authApi.logout();
    } finally {
      setUser(null);
    }
  }, []);

  const value = useMemo(
    () => ({
      user,
      isAuthenticated: user !== null,
      isInitializing,
      login,
      register,
      logout,
      refreshUser,
    }),
    [user, isInitializing, login, register, logout, refreshUser],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth musi być użyty wewnątrz AuthProvider.");
  }

  return context;
}
