import { useState } from "react";
import { Outlet } from "react-router";
import { FaBars } from "react-icons/fa6";

import Sidebar from "../components/Sidebar";

export default function AppLayout() {
  const [sidebarCollapsed, setSidebarCollapsed] = useState(false);
  const [mobileSidebarOpen, setMobileSidebarOpen] = useState(false);

  return (
    <div
      className={`app-layout ${
        sidebarCollapsed ? "app-layout--collapsed" : ""
      }`}
    >
      <Sidebar
        collapsed={sidebarCollapsed}
        mobileOpen={mobileSidebarOpen}
        onToggle={() => {
          setSidebarCollapsed((currentValue) => !currentValue);
        }}
        onCloseMobile={() => {
          setMobileSidebarOpen(false);
        }}
      />

      <div className="app-main">
        <header className="app-topbar">
          <button
            type="button"
            className="app-mobile-menu-button"
            onClick={() => setMobileSidebarOpen(true)}
            aria-label="Otwórz menu"
          >
            <FaBars />
          </button>

          <div>
            <h1 className="app-topbar__title">BudgetPlan</h1>

            <p className="app-topbar__subtitle">Zarządzaj swoimi finansami</p>
          </div>
        </header>

        <main className="app-content">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
