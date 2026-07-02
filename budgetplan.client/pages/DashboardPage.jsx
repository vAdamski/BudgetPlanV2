import { useAuth } from "../auth/AuthContext";

export default function DashboardPage() {
  const { user, logout } = useAuth();

  return (
    <div>
      <h1>BudgetPlan</h1>

      <p>
        Zalogowany jako: <strong>{user.displayName}</strong>
      </p>

      <p>{user.email}</p>
    </div>
  );
}
