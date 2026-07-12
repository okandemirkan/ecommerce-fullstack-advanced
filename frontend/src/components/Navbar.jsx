import { Link, useNavigate } from "react-router-dom";
import { useCallback, useEffect, useState } from "react";
import { FaMinus, FaPlus, FaShoppingCart, FaTimes, FaTrash } from "react-icons/fa";
import { AUTH_CHANGED_EVENT, logout, isLoggedIn, getRole } from "../services/authService";
import {
  addCartItem,
  getCurrentUserCart,
  removeCartItem,
  updateCartItemQuantity,
} from "../services/cartService";
import { notify } from "../utils/notify";
import { formatPrice } from "../utils/format";
import { useLanguage } from "../i18n/LanguageContext";
import "./Navbar.css";

function Navbar() {
  const navigate = useNavigate();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [cartOpen, setCartOpen] = useState(false);
  const [cartLoading, setCartLoading] = useState(false);
  const [updatingItemId, setUpdatingItemId] = useState(null);
  const [cart, setCart] = useState({ carts: [], grandTotal: 0 });
  const [logoutOverlay, setLogoutOverlay] = useState(false);
  const [authState, setAuthState] = useState(() => ({
    loggedIn: isLoggedIn(),
    role: getRole(),
  }));
  const { language, toggleLanguage } = useLanguage();
  const { loggedIn, role } = authState;

  const cartItems = cart.carts ?? [];
  const cartCount = cartItems.reduce((sum, item) => sum + item.quantity, 0);

  useEffect(() => {
    const refreshAuthState = () => {
      setAuthState({
        loggedIn: isLoggedIn(),
        role: getRole(),
      });
      setCart({ carts: [], grandTotal: 0 });
      setCartOpen(false);
      setDropdownOpen(false);
    };

    window.addEventListener(AUTH_CHANGED_EVENT, refreshAuthState);
    window.addEventListener("storage", refreshAuthState);

    return () => {
      window.removeEventListener(AUTH_CHANGED_EVENT, refreshAuthState);
      window.removeEventListener("storage", refreshAuthState);
    };
  }, []);

  const fetchCart = useCallback(async (showLoading = false) => {
    if (!loggedIn) return;

    try {
      if (showLoading) setCartLoading(true);
      const currentCart = await getCurrentUserCart();
      setCart({
        carts: currentCart?.carts ?? [],
        grandTotal: currentCart?.grandTotal ?? 0,
      });
    } catch {
      setCart({ carts: [], grandTotal: 0 });
    } finally {
      if (showLoading) setCartLoading(false);
    }
  }, [loggedIn]);

  useEffect(() => {
    if (!loggedIn) return;

    const handleCartUpdated = () => fetchCart();
    window.addEventListener("cartUpdated", handleCartUpdated);

    return () => window.removeEventListener("cartUpdated", handleCartUpdated);
  }, [fetchCart, loggedIn]);

  function handleLogout() {
    setDropdownOpen(false);
    setCartOpen(false);
    setLogoutOverlay(true);
    setTimeout(() => {
      logout();
      setCart({ carts: [], grandTotal: 0 });
      setLogoutOverlay(false);
      navigate("/");
    }, 350);
  }

  const handleCartToggle = () => {
    setCartOpen((current) => {
      const nextOpen = !current;
      if (nextOpen) fetchCart(true);
      return nextOpen;
    });
    setDropdownOpen(false);
  };

  const handleQuantityChange = async (cartItem, quantity) => {
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
    } catch (error) {
      notify.error(error?.response?.data?.message || "Sepet adedi güncellenemedi.");
    } finally {
      setUpdatingItemId(null);
    }
  };

  const handleRemoveItem = async (cartItemId) => {
    try {
      setUpdatingItemId(cartItemId);
      await removeCartItem(cartItemId);
      await fetchCart();
    } catch (error) {
      notify.error(error?.response?.data?.message || "Ürün sepetten çıkarılamadı.");
    } finally {
      setUpdatingItemId(null);
    }
  };

  const handleGoToCart = () => {
    setCartOpen(false);
    navigate("/cart");
  };

  return (
    <>
      {logoutOverlay && (
        <div className="logout-overlay" />
      )}
      <nav className="navbar">
        <div className="navbar-logo">
            <Link to ="/">Logo</Link>
        </div>
        <div className="navbar-links">
            <Link to ="/">Ürünler</Link>
            {loggedIn ? (
              <div className="account-menu">
                <Link to ="/orders">Siparişlerim</Link>
                {role === "Admin" && <Link to = "/admin">Admin Paneli</Link>}

                <div className="cart-menu">
                  <button
                    className="cart-btn"
                    type="button"
                    aria-label="Sepetim"
                    title="Sepetim"
                    onClick={handleCartToggle}
                  >
                    <FaShoppingCart />
                    {cartCount > 0 && <span className="cart-badge">{cartCount}</span>}
                  </button>

                  {cartOpen && (
                    <div className="cart-dropdown">
                      <div className="cart-dropdown-header">
                        <strong>Sepetim</strong>
                        <div className="cart-dropdown-header-actions">
                          <span>{cartCount} ürün</span>
                          <button
                            className="cart-close-btn"
                            type="button"
                            aria-label="Sepeti kapat"
                            title="Sepeti kapat"
                            onClick={() => setCartOpen(false)}
                          >
                            <FaTimes />
                          </button>
                        </div>
                      </div>

                      {cartLoading ? (
                        <div className="cart-empty">Sepet yükleniyor...</div>
                      ) : cartItems.length === 0 ? (
                        <div className="cart-empty">Sepetiniz boş.</div>
                      ) : (
                        <>
                          <div className="cart-items">
                            {cartItems.map((item) => (
                              <div className="cart-item" key={item.cartItemId}>
                                <div
                                  className="cart-item-info cart-item-link"
                                  onClick={() => { setCartOpen(false); navigate(`/product/${item.productId}`); }}
                                >
                                  <strong>{item.productName}</strong>
                                  <span>{formatPrice(item.price)}₺</span>
                                </div>
                                <div className="cart-item-actions">
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
                              </div>
                            ))}
                          </div>

                          <div className="cart-dropdown-footer">
                            <div>
                              <span>Toplam</span>
                              <strong>{formatPrice(cart.grandTotal)}₺</strong>
                            </div>
                            <button type="button" onClick={handleGoToCart}>
                              Sepetime Git
                            </button>
                          </div>
                        </>
                      )}
                    </div>
                  )}
                </div>

                <div className="profile-menu">
                  <button className="account-btn"
                    onClick={() => {
                      setDropdownOpen(!dropdownOpen);
                      setCartOpen(false);
                    }}> Hesabım ▾</button>
                    {dropdownOpen && (
                      <div className="account-dropdown">
                        <Link to ="/profile" onClick={()=> setDropdownOpen(false)}>Profilim</Link>
                        <button onClick={handleLogout}>Çıkış Yap</button>
                      </div>
                    )}
                </div>
              </div>
            ) : (
              <>
            <Link to ="/login">Giriş Yap</Link>
            <Link to ="/register">Kayıt Ol</Link>
            </>
            )}
            <button
              className="language-toggle"
              type="button"
              aria-label={language === "tr" ? "Switch to English" : "Türkçeye geç"}
              title={language === "tr" ? "Switch to English" : "Türkçeye geç"}
              onClick={toggleLanguage}
            >
              <span className={language === "tr" ? "active" : ""}>TR</span>
              <span className="language-toggle-divider">/</span>
              <span className={language === "en" ? "active" : ""}>ENG</span>
            </button>
        </div>
    </nav>
    </>
  );
}

export default Navbar
