import { NavLink } from "react-router";
import {
  FaChartPie,
  FaChevronLeft,
  FaChevronRight,
  FaRightFromBracket,
  FaXmark,
} from "react-icons/fa6";

import { useAuth } from "../auth/AuthContext";

export default function Sidebar({
  collapsed,
  mobileOpen,
  onToggle,
  onCloseMobile,
}) {
  const auth = useAuth();

  const handleLogout = async () => {
    await auth.logout();
  };

  return (
    <>
      {mobileOpen && (
        <button
          type="button"
          className="sidebar-backdrop"
          onClick={onCloseMobile}
          aria-label="Zamknij menu"
        />
      )}

      <aside
        className={[
          "app-sidebar",
          collapsed ? "app-sidebar--collapsed" : "",
          mobileOpen ? "app-sidebar--mobile-open" : "",
        ].join(" ")}
      >
        <div className="sidebar-header">
          <div className="sidebar-brand">
            <div className="sidebar-brand__logo">BP</div>

            {!collapsed && (
              <div className="sidebar-brand__text">
                <strong>BudgetPlan</strong>
                <span>Twoje finanse</span>
              </div>
            )}
          </div>

          <button
            type="button"
            className="sidebar-mobile-close"
            onClick={onCloseMobile}
            aria-label="Zamknij menu"
          >
            <FaXmark />
          </button>
        </div>

        <nav className="sidebar-navigation">
          {!collapsed && (
            <span className="sidebar-section-title">Menu główne</span>
          )}

          <NavLink
            to="/"
            end
            title={collapsed ? "Dashboard" : undefined}
            className={({ isActive }) =>
              [
                "sidebar-navigation__item",
                isActive ? "sidebar-navigation__item--active" : "",
              ].join(" ")
            }
            onClick={onCloseMobile}
          >
            <FaChartPie className="sidebar-navigation__icon" />

            {!collapsed && <span>Dashboard</span>}
          </NavLink>

          {/*
          <NavLink to="/transactions">
            Transakcje
          </NavLink>

          <NavLink to="/categories">
            Kategorie
          </NavLink>
          */}
        </nav>

        <div className="sidebar-footer">
          <div className="sidebar-user">
            <div className="sidebar-user__avatar">
              {getUserInitials(auth.user?.displayName)}
            </div>

            {!collapsed && (
              <div className="sidebar-user__details">
                <strong>{auth.user?.displayName ?? "Użytkownik"}</strong>

                <span>{auth.user?.email}</span>
              </div>
            )}
          </div>

          <button
            type="button"
            className="sidebar-logout"
            onClick={handleLogout}
            title={collapsed ? "Wyloguj się" : undefined}
          >
            <FaRightFromBracket />

            {!collapsed && <span>Wyloguj się</span>}
          </button>
        </div>

        <button
          type="button"
          className="sidebar-collapse-button"
          onClick={onToggle}
          aria-label={collapsed ? "Rozwiń menu" : "Zwiń menu"}
        >
          {collapsed ? <FaChevronRight /> : <FaChevronLeft />}
        </button>
      </aside>
    </>
  );
}

function getUserInitials(displayName) {
  if (!displayName) {
    return "U";
  }

  return displayName
    .split(" ")
    .filter(Boolean)
    .slice(0, 2)
    .map((part) => part[0].toUpperCase())
    .join("");
}
