import { Navigate, Route, Routes } from "react-router";

import ProtectedRoute from "../auth/ProtectedRoute";
import AppLayout from "../layouts/AppLayout";

import DashboardPage from "../pages/DashboardPage";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";

// Dodasz je, gdy utworzysz strony:
// import TransactionsPage from "../pages/TransactionsPage";
// import CategoriesPage from "../pages/CategoriesPage";
// import SettlementPeriodsPage from "../pages/SettlementPeriodsPage";
// import ReportsPage from "../pages/ReportsPage";
// import SettingsPage from "../pages/SettingsPage";

export default function App() {
  return (
    <Routes>
      {/* Strony dostępne tylko dla zalogowanego użytkownika */}
      <Route
        element={
          <ProtectedRoute>
            <AppLayout />
          </ProtectedRoute>
        }
      >
        <Route index element={<DashboardPage />} />

        {/*
        <Route path="transactions" element={<TransactionsPage />} />
        <Route path="categories" element={<CategoriesPage />} />
        <Route
          path="settlement-periods"
          element={<SettlementPeriodsPage />}
        />
        <Route path="reports" element={<ReportsPage />} />
        <Route path="settings" element={<SettingsPage />} />
        */}
      </Route>

      {/* Strony publiczne bez sidebara */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}
