import { useCallback, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FaMinus, FaPlus, FaShoppingBag, FaTrash } from "react-icons/fa";
import {
  addCartItem,
  clearCart,
  getCurrentUserCart,
  removeCartItem,
  updateCartItemQuantity,
} from "../services/cartService";
import { addOrderToCurrentUser, addOrderItemToCurrentUser } from "../services/orderService";
import { getAddresses } from "../services/userService";
import { notify } from "../utils/notify";
import { ADDRESS_TYPES, getAddressId } from "../utils/address";
import { formatPrice } from "../utils/format";
import "./CartPage.css";

function CartPage() {
  const navigate = useNavigate();
  const [cart, setCart] = useState({ carts: [], grandTotal: 0 });
  const [addresses, setAddresses] = useState([]);
  const [selectedAddressId, setSelectedAddressId] = useState("");
  const [loading, setLoading] = useState(true);
  const [updatingItemId, setUpdatingItemId] = useState(null);
  const [checkoutLoading, setCheckoutLoading] = useState(false);
  const [successVisible, setSuccessVisible] = useState(false);

  const cartItems = useMemo(() => cart.carts ?? [], [cart.carts]);
  const itemCount = useMemo(
    () => cartItems.reduce((sum, item) => sum + item.quantity, 0),
    [cartItems]
  );

  const fetchCart = useCallback(async () => {
    const currentCart = await getCurrentUserCart();
    setCart({
      carts: currentCart?.carts ?? [],
      grandTotal: currentCart?.grandTotal ?? 0,
    });
  }, []);

  const fetchPageData = useCallback(async () => {
    try {
      setLoading(true);
      const [currentCart, currentAddresses] = await Promise.all([
        getCurrentUserCart(),
        getAddresses(),
      ]);
      const addressList = currentAddresses ?? [];

      setCart({
        carts: currentCart?.carts ?? [],
        grandTotal: currentCart?.grandTotal ?? 0,
      });
      setAddresses(addressList);
      setSelectedAddressId(getAddressId(addressList[0]) ?? "");
    } catch (error) {
      notify.error(error?.response?.data?.message || "Sepet bilgileri alınamadı.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    const timerId = setTimeout(fetchPageData, 0);

    return () => clearTimeout(timerId);
  }, [fetchPageData]);

  async function handleQuantityChange(cartItem, quantity) {
    if (quantity < 1) {
      await handleRemoveItem(cartItem.cartItemId);
      return;
    }

    try {
      setUpdatingItemId(cartItem.cartItemId);
      if (quantity > cartItem.quantity) {
        if (cartItem.productId == null) {
          notify.warning("Sepet ürünü için productId bilgisi bulunamadı.");
          return;
        }
        await addCartItem(cartItem.productId, quantity - cartItem.quantity);
      } else {
        await updateCartItemQuantity(cartItem.cartItemId, quantity);
      }
      await fetchCart();
      window.dispatchEvent(new Event("cartUpdated"));
    } catch (error) {
      notify.error(error?.response?.data?.message || "Sepet adedi güncellenemedi.");
    } finally {
      setUpdatingItemId(null);
    }
  }

  async function handleRemoveItem(cartItemId) {
    try {
      setUpdatingItemId(cartItemId);
      await removeCartItem(cartItemId);
      await fetchCart();
      window.dispatchEvent(new Event("cartUpdated"));
    } catch (error) {
      notify.error(error?.response?.data?.message || "Ürün sepetten çıkarılamadı.");
    } finally {
      setUpdatingItemId(null);
    }
  }

  async function handleCheckout() {
    if (!cartItems.length) return;

    if (!selectedAddressId) {
      notify.warning("Siparişi tamamlamak için bir teslimat adresi seçmelisiniz.");
      return;
    }

    const hasMissingProductId = cartItems.some((item) => item.productId == null);

    if (hasMissingProductId) {
      notify.warning("Sipariş oluşturmak için sepet yanıtında productId alanı dönmeli.");
      return;
    }

    try {
      setCheckoutLoading(true);

      // İlk ürünle siparişi oluştur, orderId'yi al
      const firstItem = cartItems[0];
      const createdOrder = await addOrderToCurrentUser({
        addressId: Number(selectedAddressId),
        productId: firstItem.productId,
        quantity: firstItem.quantity,
      });

      // Kalan ürünleri aynı siparişe ekle
      const remainingItems = cartItems.slice(1);
      if (remainingItems.length > 0) {
        await Promise.all(
          remainingItems.map((item) =>
            addOrderItemToCurrentUser({
              orderId: createdOrder.orderId,
              productId: item.productId,
              quantity: item.quantity,
            })
          )
        );
      }

      await clearCart();
      window.dispatchEvent(new Event("cartUpdated"));
      setSuccessVisible(true);

      setTimeout(() => {
        navigate("/orders");
      }, 1800);
    } catch (error) {
      notify.error(error?.response?.data?.message || "Sipariş tamamlanamadı.");
    } finally {
      setCheckoutLoading(false);
    }
  }

  if (loading) return <div className="cart-loading">Sepet yükleniyor...</div>;

  return (
    <div className="cart-page">
      {successVisible && (
        <div className="checkout-success-overlay">
          <div className="checkout-success">
            <div className="checkout-success-icon">
              <FaShoppingBag />
            </div>
            <h2>Siparişiniz tamamlandı</h2>
            <p>Siparişlerim sayfasına yönlendiriliyorsunuz.</p>
          </div>
        </div>
      )}

      <header className="cart-header">
        <div>
          <h1>Sepetim</h1>
          <p>Sepetindeki ürünleri ve teslimat bilgilerini buradan kontrol edebilirsin.</p>
        </div>
        <div className="cart-count-card">
          <span>{itemCount}</span>
          <p>Toplam Ürün</p>
        </div>
      </header>

      {cartItems.length === 0 ? (
        <section className="cart-empty-state">
          <FaShoppingBag />
          <h2>Sepetiniz boş</h2>
          <p>Ürünleri inceleyip sepetinize ekleyerek alışverişe başlayabilirsiniz.</p>
          <button type="button" onClick={() => navigate("/")}>Ürünlere Git</button>
        </section>
      ) : (
        <div className="cart-layout">
          <section className="cart-items-panel">
            <div className="cart-section-title">
              <h2>Ürünler</h2>
              <span>{cartItems.length} kalem</span>
            </div>

            <div className="cart-page-items">
              {cartItems.map((item) => (
                <article className="cart-page-item" key={item.cartItemId}>
                  <div
                    className="cart-page-item-image cart-item-link"
                    onClick={() => navigate(`/product/${item.productId}`)}
                    title={item.productName}
                  >
                    <img
                      src={item.imageUrl || "https://placehold.co/120x120?text=Ürün"}
                      alt={item.productName}
                      onError={e => { e.target.src = "https://placehold.co/120x120?text=Ürün"; }}
                    />
                  </div>

                  <div className="cart-page-item-info">
                    <h3
                      className="cart-item-link"
                      onClick={() => navigate(`/product/${item.productId}`)}
                    >
                      {item.productName}
                    </h3>
                    <span>Birim fiyat: {formatPrice(item.price)}₺</span>
                    <strong>{formatPrice(item.totalPrice)}₺</strong>
                  </div>

                  <div className="cart-page-item-actions">
                    <button
                      type="button"
                      aria-label="Adedi azalt"
                      disabled={updatingItemId === item.cartItemId}
                      onClick={() => handleQuantityChange(item, item.quantity - 1)}
                    >
                      <FaMinus />
                    </button>
                    <span>{item.quantity}</span>
                    <button
                      type="button"
                      aria-label="Adedi artır"
                      disabled={updatingItemId === item.cartItemId}
                      onClick={() => handleQuantityChange(item, item.quantity + 1)}
                    >
                      <FaPlus />
                    </button>
                    <button
                      type="button"
                      aria-label="Sepetten çıkar"
                      disabled={updatingItemId === item.cartItemId}
                      onClick={() => handleRemoveItem(item.cartItemId)}
                    >
                      <FaTrash />
                    </button>
                  </div>
                </article>
              ))}
            </div>
          </section>

          <aside className="cart-summary-panel">
            <div className="cart-section-title">
              <h2>Sipariş Özeti</h2>
            </div>

            <div className="address-selector">
              <label htmlFor="address">Teslimat Adresi</label>
              {addresses.length === 0 ? (
                <div className="address-empty">
                  <p>Kayıtlı adresiniz yok.</p>
                  <button type="button" onClick={() => navigate("/profile")}>Adres Ekle</button>
                </div>
              ) : (
                <select
                  id="address"
                  value={selectedAddressId}
                  onChange={(event) => setSelectedAddressId(event.target.value)}
                >
                  {addresses.map((address) => (
                    <option key={getAddressId(address)} value={getAddressId(address)}>
                      {ADDRESS_TYPES[address.addressType]} - {address.district}, {address.city}
                    </option>
                  ))}
                </select>
              )}
            </div>

            <div className="summary-lines">
              <div>
                <span>Ürün adedi</span>
                <strong>{itemCount}</strong>
              </div>
              <div>
                <span>Ara toplam</span>
                <strong>{formatPrice(cart.grandTotal)}₺</strong>
              </div>
              <div className="summary-total">
                <span>Toplam</span>
                <strong>{formatPrice(cart.grandTotal)}₺</strong>
              </div>
            </div>

            <button
              className="checkout-btn"
              type="button"
              disabled={checkoutLoading || addresses.length === 0}
              onClick={handleCheckout}
            >
              {checkoutLoading ? "Sipariş oluşturuluyor..." : "Siparişi Tamamla"}
            </button>
          </aside>
        </div>
      )}
    </div>
  );
}

export default CartPage;
