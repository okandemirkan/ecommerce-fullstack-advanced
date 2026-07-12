export const STATUS_MAP = {
  Pending: "Beklemede",
  Preparing: "Hazırlanıyor",
  Shipped: "Kargoda",
  Delivered: "Teslim Edildi",
  Canceled: "İptal Edildi",
};

export const STATUS_COLOR_MAP = {
  Pending: "status-pending",
  Preparing: "status-preparing",
  Shipped: "status-shipped",
  Delivered: "status-delivered",
  Canceled: "status-cancelled",
};

export const STATUS_TRANSITIONS = {
  Pending: ["Shipped", "Canceled"],
  Shipped: ["Delivered", "Canceled"],
  Delivered: [],
  Canceled: [],
};

export function getStatusTransitionLabel(status) {
  const labels = {
    Shipped: "Kargoya ver",
    Delivered: "Teslim edildi olarak işaretle",
    Canceled: "Siparişi iptal et",
  };

  return labels[status] ?? status;
}
