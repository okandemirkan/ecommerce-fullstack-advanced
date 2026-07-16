import { useCallback, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getCurrentUserOrders, cancelOrder, updateShippingAddress } from "../services/orderService";
import { getAddresses } from "../services/userService";
import { notify } from "../utils/notify";
import ConfirmModal from "../components/ConfirmModal";
import Pagination from "../components/Pagination";
import { ADDRESS_TYPES, getAddressId } from "../utils/address";
import "./OrderPage.css";

const STATUS_MAP = {
  Pending:   { label: "Hazırlanıyor",   color: "status-pending"   },
  Shipped:   { label: "Kargoda",        color: "status-shipped"   },
  Delivered: { label: "Teslim Edildi",  color: "status-delivered" },
  Canceled:  { label: "İptal Edildi",   color: "status-cancelled" },
};

const ORDERS_PAGE_SIZE = 5;

function formatDate(dateStr) {
  const date = new Date(dateStr);
  return date.toLocaleDateString("tr-TR", {
    day: "numeric", month: "long", year: "numeric",
    hour: "2-digit", minute: "2-digit",
  });
}

function normalizeAddressText(value) {
  return String(value ?? "")
    .toLocaleLowerCase("tr-TR")
    .replace(/[.,/#!$%^&*;:{}=\-_`~()]/g, " ")
    .replace(/\s+/g, " ")
    .trim();
}

function isCurrentShippingAddress(order, address) {
  const shippingAddress = normalizeAddressText(order?.shippingAddress);
  if (!shippingAddress || !address) return false;

  const requiredParts = [address.fullAddress, address.district, address.city]
    .map(normalizeAddressText)
    .filter(Boolean);

  return requiredParts.length > 0 && requiredParts.every(part => shippingAddress.includes(part));
}

function getCurrentShippingAddressId(order, addresses) {
  const currentAddress = addresses.find(address => isCurrentShippingAddress(order, address));
  return getAddressId(currentAddress) ?? "";
}

function OrdersPage() {
  const navigate = useNavigate();
  const [orders, setOrders] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [loading, setLoading] = useState(true);
  const [cancellingId, setCancellingId] = useState(null);
  const [confirmModal, setConfirmModal] = useState({ isOpen: false, order: null });

  // Address change state
  const [userAddresses, setUserAddresses] = useState([]);
  const [addressPanelOrderId, setAddressPanelOrderId] = useState(null); // which order's panel is open
  const [selectedAddressId, setSelectedAddressId] = useState("");
  const [savingAddress, setSavingAddress] = useState(false);

  const fetchOrders = useCallback(async () => {
    try {
      setLoading(true);
      const response = await getCurrentUserOrders(currentPage, ORDERS_PAGE_SIZE);
      setOrders(response?.items ?? []);
      setTotalPages(response?.totalPages ?? 1);
      setTotalCount(response?.totalCount ?? 0);
    } catch (err) {
      if (err?.response?.status === 404) {
        setOrders([]);
        setTotalPages(1);
        setTotalCount(0);
      } else notify.error("Siparişler alınamadı.");
    } finally {
      setLoading(false);
    }
  }, [currentPage]);

  async function fetchAddresses() {
    try {
      const addr = await getAddresses();
      setUserAddresses(addr ?? []);
    } catch {
      setUserAddresses([]);
    }
  }

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchOrders();
    });
    return () => { active = false; };
  }, [fetchOrders]);

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchAddresses();
    });
    return () => { active = false; };
  }, []);

  function handleCancelClick(order) {
    setConfirmModal({ isOpen: true, order });
  }

  async function handleCancelConfirmed() {
    const order = confirmModal.order;
    setConfirmModal({ isOpen: false, order: null });
    setCancellingId(order.orderId);
    try {
      await cancelOrder(order.orderId);
      if (orders.length === 1 && currentPage > 1) setCurrentPage(currentPage - 1);
      else fetchOrders();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Sipariş iptal edilemedi.");
    } finally {
      setCancellingId(null);
    }
  }

  function toggleAddressPanel(order) {
    const orderId = order.orderId;
    if (addressPanelOrderId === orderId) {
      setAddressPanelOrderId(null);
      setSelectedAddressId("");
    } else {
      setAddressPanelOrderId(orderId);
      setSelectedAddressId(getCurrentShippingAddressId(order, userAddresses));
    }
  }

  async function handleSaveAddress(order) {
    if (!selectedAddressId) {
      notify.warning("Lütfen bir adres seçin.");
      return;
    }
    const selectedAddress = userAddresses.find(address => getAddressId(address) == selectedAddressId);
    if (isCurrentShippingAddress(order, selectedAddress)) {
      notify.warning("Seçili adres zaten siparişin mevcut teslimat adresi.");
      return;
    }

    setSavingAddress(true);
    try {
      await updateShippingAddress(order.orderId, Number(selectedAddressId));
      notify.success("Teslimat adresi güncellendi.");
      setAddressPanelOrderId(null);
      setSelectedAddressId("");
      fetchOrders();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Adres güncellenemedi.");
    } finally {
      setSavingAddress(false);
    }
  }

  if (loading) return <div className="orders-loading">Yükleniyor...</div>;

  const sortedOrders = [...orders].sort(
    (a, b) => new Date(b.createdAt) - new Date(a.createdAt)
  );

  return (
    <div className="orders-page">
      <ConfirmModal
        isOpen={confirmModal.isOpen}
        title="Siparişi İptal Et"
        message="Bu siparişi iptal etmek istediğinize emin misiniz? Bu işlem geri alınamaz."
        confirmLabel="Evet, İptal Et"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={handleCancelConfirmed}
        onCancel={() => setConfirmModal({ isOpen: false, order: null })}
      />

      <div className="orders-header">
        <div className="orders-header-left">
          <h1 className="orders-title">Siparişlerim</h1>
          <p>Tüm siparişlerinizi buradan takip edebilirsiniz.</p>
        </div>
        <div className="orders-count">
          <span>{totalCount}</span>
          <p>Toplam Sipariş</p>
        </div>
      </div>

      {sortedOrders.length === 0 ? (
        <div className="orders-empty">
          <div className="empty-icon">📦</div>
          <p>Henüz siparişiniz bulunmamaktadır.</p>
        </div>
      ) : (
        <div className="orders-list">
          {sortedOrders.map((order) => {
            const status = STATUS_MAP[order.orderStatus] ?? STATUS_MAP.Pending;
            const isAddressChangeable = order.orderStatus === "Pending";
            const isAddressPanelOpen = addressPanelOrderId === order.orderId;
            const selectedAddress = userAddresses.find(address => getAddressId(address) == selectedAddressId);
            const selectedAddressIsCurrent = isAddressPanelOpen && isCurrentShippingAddress(order, selectedAddress);

            return (
              <div className="order-card" key={order.orderId}>
                {/* Üst bilgi */}
                <div className="order-card-top">
                  <div className="order-date-label">
                    <span className="order-label">Sipariş Tarihi</span>
                    <span className="order-date-value">{formatDate(order.createdAt)}</span>
                  </div>
                  <span className={`order-status ${status.color}`}>{status.label}</span>
                </div>

                {/* Ürün listesi */}
                <div className="order-items-list">
                  {(order.items ?? []).map((item) => (
                    <div className="order-item-row" key={item.orderItemId}>
                      <button
                        type="button"
                        className="order-card-image order-product-link"
                        onClick={() => navigate(`/product/${item.productId}`)}
                        aria-label={`${item.productName} ürün detayına git`}
                      >
                        <img
                          src={item.imageUrl || "https://placehold.co/72x72?text=Ürün"}
                          alt={item.productName}
                          onError={(e) => { e.target.src = "https://placehold.co/72x72?text=Ürün"; }}
                        />
                      </button>
                      <div className="order-item-info">
                        <button
                          type="button"
                          className="order-product order-product-link"
                          onClick={() => navigate(`/product/${item.productId}`)}
                        >
                          {item.productName}
                        </button>
                        <div className="order-meta">
                          <span className="order-label">Adet:</span>
                          <span>{item.quantity}</span>
                        </div>
                        <div className="order-meta">
                          <span className="order-label">Birim Fiyat:</span>
                          <span>{item.price.toLocaleString("tr-TR")}₺</span>
                        </div>
                      </div>
                      <div className="order-item-price">
                        {(item.price * item.quantity).toLocaleString("tr-TR")}₺
                      </div>
                    </div>
                  ))}
                </div>

                {/* Adres Değiştirme Paneli (sadece Pending siparişler) */}
                {isAddressChangeable && (
                  <div className="order-address-change-section">
                    <div className="order-address-change-header">
                      <div className="order-footer-address">
                        <span className="order-label">Teslimat Adresi:</span>
                        <span>{order.shippingAddress}</span>
                      </div>
                      <button
                        className="btn-change-address"
                        onClick={() => toggleAddressPanel(order)}
                      >
                        {isAddressPanelOpen ? "✕ Kapat" : "✏️ Adresi Değiştir"}
                      </button>
                    </div>

                    {isAddressPanelOpen && (
                      <div className="order-address-panel">
                        {userAddresses.length === 0 ? (
                          <p className="address-panel-empty">
                            Kayıtlı adresiniz bulunmuyor. Profil sayfasından adres ekleyebilirsiniz.
                          </p>
                        ) : (
                          <>
                            <p className="address-panel-hint">Siparişiniz için yeni bir teslimat adresi seçin:</p>
                            <div className="address-options">
                              {userAddresses.map(addr => {
                                const addressId = getAddressId(addr);
                                const isCurrentAddress = isCurrentShippingAddress(order, addr);
                                return (
                                <label
                                  key={addressId}
                                  className={`address-option ${selectedAddressId == addressId ? "selected" : ""} ${isCurrentAddress ? "current-address" : ""}`}
                                >
                                  <input
                                    type="radio"
                                    name={`addr-${order.orderId}`}
                                    value={addressId}
                                    checked={selectedAddressId == addressId}
                                    onChange={() => setSelectedAddressId(addressId)}
                                  />
                                  <div className="address-option-content">
                                    <span className="address-option-type">{ADDRESS_TYPES[addr.addressType] ?? addr.addressType}</span>
                                    <span className="address-option-text">{addr.fullAddress}</span>
                                    <span className="address-option-city">{addr.district}, {addr.city} {addr.zipCode}</span>
                                    {isCurrentAddress && <span className="address-option-current-note">Mevcut teslimat adresi</span>}
                                  </div>
                                </label>
                                );
                              })}
                            </div>
                            <div className="address-panel-actions">
                              <button
                                className="btn-cancel"
                                onClick={() => { setAddressPanelOrderId(null); setSelectedAddressId(""); }}
                              >
                                Vazgeç
                              </button>
                              <button
                                className="btn-save-address"
                                onClick={() => handleSaveAddress(order)}
                                disabled={savingAddress || !selectedAddressId || selectedAddressIsCurrent}
                              >
                                {savingAddress ? "Kaydediliyor..." : "💾 Kaydet"}
                              </button>
                            </div>
                          </>
                        )}
                      </div>
                    )}
                  </div>
                )}

                {/* Alt bilgi (Pending dışı siparişlerde adres + toplam) */}
                {!isAddressChangeable && (
                  <div className="order-card-footer">
                    <div className="order-footer-address">
                      <span className="order-label">Teslimat Adresi:</span>
                      <span>{order.shippingAddress}</span>
                    </div>
                    <div className="order-footer-right">
                      <div className="order-price-block">
                        <span className="order-label">Toplam Tutar</span>
                        <span className="order-price">{order.totalPrice.toLocaleString("tr-TR")}₺</span>
                      </div>
                    </div>
                  </div>
                )}

                {/* Tutar + İptal butonu (Pending siparişler için) */}
                {isAddressChangeable && (
                  <div className="order-card-footer">
                    <div className="order-price-block">
                      <span className="order-label">Toplam Tutar</span>
                      <span className="order-price">{order.totalPrice.toLocaleString("tr-TR")}₺</span>
                    </div>
                    <div className="order-footer-right">
                      <button
                        className="btn-cancel"
                        onClick={() => handleCancelClick(order)}
                        disabled={cancellingId != null}
                      >
                        {cancellingId === order.orderId ? "İptal ediliyor..." : "Siparişi İptal Et"}
                      </button>
                    </div>
                  </div>
                )}
              </div>
            );
          })}
          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={setCurrentPage}
            scrollToTopOnMobile
          />
        </div>
      )}
    </div>
  );
}

export default OrdersPage;
