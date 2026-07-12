import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { FaCheckCircle, FaExclamationCircle, FaInfoCircle, FaTimes } from "react-icons/fa";
import { NOTIFICATION_EVENT } from "../utils/notify";
import "./NotificationCenter.css";

const ICONS = {
  success: FaCheckCircle,
  error: FaExclamationCircle,
  warning: FaExclamationCircle,
  info: FaInfoCircle,
};

function getPlacement(pathname) {
  if (pathname === "/login" || pathname === "/register" || pathname === "/profile") return "left";
  if (pathname.startsWith("/admin") || pathname.startsWith("/cart")) return "right";
  return "right";
}

function NotificationCenter() {
  const location = useLocation();
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    function handleNotification(event) {
      const notification = event.detail;
      setNotifications((current) => [notification, ...current].slice(0, 4));

      window.setTimeout(() => {
        setNotifications((current) => current.filter((item) => item.id !== notification.id));
      }, 4200);
    }

    window.addEventListener(NOTIFICATION_EVENT, handleNotification);
    return () => window.removeEventListener(NOTIFICATION_EVENT, handleNotification);
  }, []);

  function dismissNotification(id) {
    setNotifications((current) => current.filter((item) => item.id !== id));
  }

  return (
    <div className={`notification-center notification-center-${getPlacement(location.pathname)}`}>
      {notifications.map((notification) => {
        const Icon = ICONS[notification.type] || FaInfoCircle;

        return (
          <div className={`notification-toast notification-${notification.type}`} key={notification.id}>
            <Icon className="notification-icon" />
            <p>{notification.message}</p>
            <button type="button" aria-label="Bildirimi kapat" onClick={() => dismissNotification(notification.id)}>
              <FaTimes />
            </button>
          </div>
        );
      })}
    </div>
  );
}

export default NotificationCenter;
