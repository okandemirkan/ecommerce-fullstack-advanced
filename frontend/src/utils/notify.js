const NOTIFICATION_EVENT = "app-notification";

function emitNotification(message, type = "info") {
  window.dispatchEvent(new CustomEvent(NOTIFICATION_EVENT, {
    detail: {
      id: crypto.randomUUID(),
      message,
      type,
    },
  }));
}

export const notify = {
  success: (message) => emitNotification(message, "success"),
  error: (message) => emitNotification(message, "error"),
  warning: (message) => emitNotification(message, "warning"),
  info: (message) => emitNotification(message, "info"),
};

export { NOTIFICATION_EVENT };
