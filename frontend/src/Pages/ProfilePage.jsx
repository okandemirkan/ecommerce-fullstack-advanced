import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { FaEye, FaEyeSlash, FaTrash, FaPlus, FaPen } from "react-icons/fa";
import { getProfile, updateProfile, changePassword, getAddresses, addAddress, deleteAddress, updateAddress, deleteCurrentUser } from "../services/userService";
import RatingStars from "../components/RatingStars";
import { getCurrentUserReviews } from "../services/reviewService";
import { notify } from "../utils/notify";
import { logout, login, setAuthToken } from "../services/authService";
import ValidationHint from "../components/ValidationHint";
import {
  getValidationState,
  hasValue,
  isValidAddressName,
  isValidEmail,
  isValidPassword,
  isValidPersonName,
  isValidTurkishPhone
} from "../utils/validation";
import { ADDRESS_TYPES, getAddressId } from "../utils/address";
import "./ProfilePage.css";

const emptyAddress = { city: "", district: "", fullAddress: "", zipCode: "", addressType: "Home" };
const emptyInfoForm = { userName: "", email: "", phoneNumber: "" };

function formatDate(date) {
  if (!date) return "";
  return new Date(date).toLocaleDateString("tr-TR");
}

function normalizeInfoForm(form) {
  return {
    userName: form.userName?.trim() || "",
    email: form.email?.trim() || "",
    phoneNumber: form.phoneNumber?.trim() || "",
  };
}

function ProfilePage() {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [addresses, setAddresses] = useState([]);
  const [reviews, setReviews] = useState([]);
  const [reviewsLoaded, setReviewsLoaded] = useState(false);
  const [reviewsError, setReviewsError] = useState("");
  const [activeTab, setActiveTab] = useState("info");

  const [infoForm, setInfoForm] = useState(emptyInfoForm);
  const [initialInfoForm, setInitialInfoForm] = useState(emptyInfoForm);

  const [passwordForm, setPasswordForm] = useState({ currentPassword: "", newPassword: "", verifyPassword: "" });
  const [showPasswords, setShowPasswords] = useState({ current: false, new: false, verify: false });

  const [showAddressForm, setShowAddressForm] = useState(false);
  const [addressForm, setAddressForm] = useState(emptyAddress);

  const [editingAddress, setEditingAddress] = useState(null);
  const [editForm, setEditForm] = useState(emptyAddress);

  // Hesap silme state'leri
  const [deleteStep, setDeleteStep] = useState("idle"); // "idle" | "credentials" | "confirm"
  const [deleteCredentials, setDeleteCredentials] = useState({ email: "", password: "" });
  const [verifyingCredentials, setVerifyingCredentials] = useState(false);
  const [showDeletePassword, setShowDeletePassword] = useState(false);
  const [deletingAccount, setDeletingAccount] = useState(false);

  useEffect(() => {
    fetchProfile();
    fetchAddresses();
  }, []);

  useEffect(() => {
    if (activeTab !== "reviews" || !user || reviewsLoaded) return;
    fetchReviews();
  }, [activeTab, user, reviewsLoaded]);

  // Tab değişince hesap silme akışını sıfırla
  useEffect(() => {
    let active = true;

    Promise.resolve().then(() => {
      if (!active || activeTab === "account") return;

      setDeleteStep("idle");
      setDeleteCredentials({ email: "", password: "" });
    });

    return () => { active = false; };
  }, [activeTab]);

  async function fetchProfile() {
    try {
      const data = await getProfile();
      const profileForm = { userName: data.userName, email: data.eMail, phoneNumber: data.phoneNumber };
      setUser(data);
      setInfoForm(profileForm);
      setInitialInfoForm(profileForm);
    } catch {
      notify.error("Profil bilgileri alınamadı.");
    }
  }

  async function fetchReviews() {
    try {
      setReviewsError("");
      setReviews(await getCurrentUserReviews());
      setReviewsLoaded(true);
    } catch (err) {
      if (err?.response?.status === 404) {
        setReviews([]);
        setReviewsLoaded(true);
        return;
      }
      if (err?.response?.status === 403) {
        setReviews([]);
        setReviewsLoaded(true);
        setReviewsError("Yorumlarınız şu an görüntülenemiyor.");
        return;
      }
      notify.error(err?.response?.data?.message || "Yorumlar alınamadı.");
    }
  }

  async function fetchAddresses() {
    try {
      const data = await getAddresses();
      setAddresses(data);
    } catch {
      notify.error("Adresler alınamadı.");
    }
  }

  async function handleUpdateInfo() {
    if (!isInfoFormValid) {
      notify.warning("Lütfen kırmızı işaretli alanları düzeltin.");
      return;
    }
    try {
      const updatedInfo = normalizeInfoForm(infoForm);
      await updateProfile(updatedInfo);
      notify.success("Bilgileriniz güncellendi.");
      fetchProfile();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Güncelleme başarısız.");
    }
  }

  async function handleChangePassword() {
    if (!isPasswordFormValid) {
      notify.warning("Lütfen şifre kurallarını kontrol edin.");
      return;
    }
    try {
      await changePassword(passwordForm);
      notify.success("Şifreniz değiştirildi.");
      setPasswordForm({ currentPassword: "", newPassword: "", verifyPassword: "" });
    } catch (err) {
      notify.error(err?.response?.data?.message || "Şifre değiştirme başarısız.");
    }
  }

  async function handleAddAddress() {
    if (!isAddressFormValid) {
      notify.warning("Lütfen adres alanlarını kontrol edin.");
      return;
    }
    try {
      await addAddress(addressForm);
      setAddressForm(emptyAddress);
      setShowAddressForm(false);
      fetchAddresses();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Adres eklenemedi.");
    }
  }

  async function handleDeleteAddress(id) {
    if (addresses.length <= 1) return notify.warning("En az 1 adresiniz olmalıdır.");
    try {
      await deleteAddress(id);
      fetchAddresses();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Adres silinemedi.");
    }
  }

  async function handleUpdateAddress(addressId) {
    if (!isEditAddressFormValid) {
      notify.warning("Lütfen adres alanlarını kontrol edin.");
      return;
    }
    try {
      await updateAddress(addressId, editForm);
      setEditingAddress(null);
      fetchAddresses();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Adres güncellenemedi.");
    }
  }

  // ── Hesap silme akışı ──────────────────────────────────────────────
  function handleStartDeleteFlow() {
    setDeleteStep("credentials");
    setDeleteCredentials({ email: "", password: "" });
  }

  function handleCancelDeleteFlow() {
    setDeleteStep("idle");
    setDeleteCredentials({ email: "", password: "" });
  }

  async function handleCredentialsSubmit(e) {
    e.preventDefault();
    if (!deleteCredentials.email.trim()) {
      notify.warning("Lütfen e-posta adresinizi girin.");
      return;
    }
    if (!deleteCredentials.password.trim()) {
      notify.warning("Lütfen şifrenizi girin.");
      return;
    }
    // Login API ile e-posta + şifre doğrulaması yap
    setVerifyingCredentials(true);
    try {
      // login token'ı localStorage'a yazar; mevcut token'ı saklayıp geri koyuyoruz
      const existingToken = localStorage.getItem("token");
      await login({ eMail: deleteCredentials.email.trim(), password: deleteCredentials.password });
      // Doğrulama başarılı; orijinal token'ı geri koy
      if (existingToken) setAuthToken(existingToken);
      setDeleteStep("confirm");
    } catch {
      notify.error("E-posta veya şifre hatalı. Lütfen tekrar deneyin.");
    } finally {
      setVerifyingCredentials(false);
    }
  }

  async function handleConfirmDelete() {
    setDeletingAccount(true);
    try {
      await deleteCurrentUser();
      notify.success("Hesabınız başarıyla silindi. Görüşmek üzere!");
      logout();
      setTimeout(() => {
        navigate("/");
      }, 1500);
    } catch (err) {
      setDeletingAccount(false);
      setDeleteStep("credentials");
      notify.error(err?.response?.data?.message || "Hesap silme işlemi başarısız.");
    }
  }

  if (!user) return <div className="profile-loading">Yükleniyor...</div>;

  const hasInfoChanges = JSON.stringify(normalizeInfoForm(infoForm)) !== JSON.stringify(normalizeInfoForm(initialInfoForm));
  const infoValidation = {
    userName: isValidPersonName(infoForm.userName),
    email: isValidEmail(infoForm.email),
    phoneNumber: isValidTurkishPhone(infoForm.phoneNumber)
  };
  const passwordValidation = {
    currentPassword: hasValue(passwordForm.currentPassword),
    newPassword: isValidPassword(passwordForm.newPassword),
    differentPassword: hasValue(passwordForm.newPassword)
      && passwordForm.newPassword !== passwordForm.currentPassword,
    verifyPassword: hasValue(passwordForm.verifyPassword)
      && passwordForm.newPassword === passwordForm.verifyPassword
  };
  const isInfoFormValid = Object.values(infoValidation).every(Boolean);
  const isPasswordFormValid = Object.values(passwordValidation).every(Boolean);
  const addressValidation = {
    city: isValidAddressName(addressForm.city),
    district: isValidAddressName(addressForm.district),
    fullAddress: hasValue(addressForm.fullAddress)
  };
  const editAddressValidation = {
    city: isValidAddressName(editForm.city),
    district: isValidAddressName(editForm.district),
    fullAddress: hasValue(editForm.fullAddress)
  };
  const isAddressFormValid = Object.values(addressValidation).every(Boolean);
  const isEditAddressFormValid = Object.values(editAddressValidation).every(Boolean);

  function validationClass(value, isValid) {
    const state = getValidationState(value, isValid);
    return state === "neutral" ? "" : `validation-input-${state}`;
  }

  return (
    <div className="profile-container">
      <aside className="profile-sidebar">
        <div className="profile-avatar">
          {user.userName.charAt(0).toUpperCase()}
        </div>
        <h3 className="profile-name">{user.userName}</h3>
        <p className="profile-email">{user.eMail}</p>
        {user.role && <span className="profile-role">{user.role}</span>}
        <nav className="profile-nav">
          <button className={activeTab === "info" ? "active" : ""} onClick={() => setActiveTab("info")}>
            Bilgilerim
          </button>
          <button className={activeTab === "password" ? "active" : ""} onClick={() => setActiveTab("password")}>
            Şifre Değiştir
          </button>
          <button className={activeTab === "addresses" ? "active" : ""} onClick={() => setActiveTab("addresses")}>
            Adreslerim
          </button>
          <button className={activeTab === "reviews" ? "active" : ""} onClick={() => setActiveTab("reviews")}>
            Yorumlarım
          </button>
          <button
            className={`profile-nav-danger ${activeTab === "account" ? "active" : ""}`}
            onClick={() => setActiveTab("account")}
          >
            ⚠️ Hesabı Kapat
          </button>
        </nav>
      </aside>

      <main className="profile-content">

        {activeTab === "info" && (
          <div className="profile-card">
            <h2>Bilgilerimi Güncelle</h2>
            <div className="form-step">
              <div className="validation-field">
                <input
                  type="text"
                  placeholder="Ad Soyad"
                  className={validationClass(infoForm.userName, infoValidation.userName)}
                  value={infoForm.userName}
                  onChange={(e) => setInfoForm({ ...infoForm, userName: e.target.value })}
                />
                <ValidationHint state={getValidationState(infoForm.userName, infoValidation.userName)}>
                  3–45 karakter olmalı ve sayı içermemeli.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  type="email"
                  placeholder="E-posta"
                  className={validationClass(infoForm.email, infoValidation.email)}
                  value={infoForm.email}
                  onChange={(e) => setInfoForm({ ...infoForm, email: e.target.value })}
                />
                <ValidationHint state={getValidationState(infoForm.email, infoValidation.email)}>
                  Geçerli bir e-posta adresi girin.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  type="tel"
                  placeholder="Telefon Numarası"
                  className={validationClass(infoForm.phoneNumber, infoValidation.phoneNumber)}
                  value={infoForm.phoneNumber}
                  onChange={(e) => setInfoForm({ ...infoForm, phoneNumber: e.target.value })}
                />
                <ValidationHint state={getValidationState(infoForm.phoneNumber, infoValidation.phoneNumber)}>
                  Türkiye cep telefonu formatında olmalı.
                </ValidationHint>
              </div>
              <button className="btn-primary" disabled={!hasInfoChanges || !isInfoFormValid} onClick={handleUpdateInfo}>Güncelle</button>
            </div>
          </div>
        )}

        {activeTab === "password" && (
          <div className="profile-card">
            <h2>Şifre Değiştir</h2>
            <div className="form-step">
              <div className="validation-field">
                <div className={`password-field ${validationClass(passwordForm.currentPassword, passwordValidation.currentPassword)}`}>
                  <input type={showPasswords.current ? "text" : "password"} placeholder="Mevcut Şifre"
                    value={passwordForm.currentPassword}
                    onChange={(e) => setPasswordForm({ ...passwordForm, currentPassword: e.target.value })} />
                  <button type="button" onClick={() => setShowPasswords({ ...showPasswords, current: !showPasswords.current })}>
                    {showPasswords.current ? <FaEyeSlash /> : <FaEye />}
                  </button>
                </div>
              </div>
              <div className="validation-field">
                <div className={`password-field ${validationClass(passwordForm.newPassword, passwordValidation.newPassword && passwordValidation.differentPassword)}`}>
                  <input type={showPasswords.new ? "text" : "password"} placeholder="Yeni Şifre"
                    value={passwordForm.newPassword}
                    onChange={(e) => setPasswordForm({ ...passwordForm, newPassword: e.target.value })} />
                  <button type="button" onClick={() => setShowPasswords({ ...showPasswords, new: !showPasswords.new })}>
                    {showPasswords.new ? <FaEyeSlash /> : <FaEye />}
                  </button>
                </div>
                <ValidationHint state={getValidationState(passwordForm.newPassword, passwordValidation.newPassword)}>
                  En az 7 karakter olmalı.
                </ValidationHint>
                <ValidationHint state={getValidationState(passwordForm.newPassword, passwordValidation.differentPassword)}>
                  Mevcut şifreden farklı olmalı.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <div className={`password-field ${validationClass(passwordForm.verifyPassword, passwordValidation.verifyPassword)}`}>
                  <input type={showPasswords.verify ? "text" : "password"} placeholder="Yeni Şifre Tekrar"
                    value={passwordForm.verifyPassword}
                    onChange={(e) => setPasswordForm({ ...passwordForm, verifyPassword: e.target.value })} />
                  <button type="button" onClick={() => setShowPasswords({ ...showPasswords, verify: !showPasswords.verify })}>
                    {showPasswords.verify ? <FaEyeSlash /> : <FaEye />}
                  </button>
                </div>
                <ValidationHint state={getValidationState(passwordForm.verifyPassword, passwordValidation.verifyPassword)}>
                  Yeni şifreler eşleşmeli.
                </ValidationHint>
              </div>
              <button className="btn-primary" disabled={!isPasswordFormValid} onClick={handleChangePassword}>Şifremi Değiştir</button>
            </div>
          </div>
        )}

        {activeTab === "addresses" && (
          <div className="profile-card">
            <div className="addresses-header">
              <h2>Adreslerim</h2>
              {addresses.length < 3 && (
                <button className="btn-add-address" onClick={() => setShowAddressForm(!showAddressForm)}>
                  <FaPlus /> Adres Ekle
                </button>
              )}
            </div>

            {showAddressForm && (
              <div className="address-form">
                <div className="form-step">
                  <div className="validation-field">
                    <input
                      type="text"
                      placeholder="Şehir"
                      className={validationClass(addressForm.city, addressValidation.city)}
                      value={addressForm.city}
                      onChange={(e) => setAddressForm({ ...addressForm, city: e.target.value })}
                    />
                    <ValidationHint state={getValidationState(addressForm.city, addressValidation.city)}>
                      Şehir zorunludur ve sayı içeremez.
                    </ValidationHint>
                  </div>
                  <div className="validation-field">
                    <input
                      type="text"
                      placeholder="İlçe"
                      className={validationClass(addressForm.district, addressValidation.district)}
                      value={addressForm.district}
                      onChange={(e) => setAddressForm({ ...addressForm, district: e.target.value })}
                    />
                    <ValidationHint state={getValidationState(addressForm.district, addressValidation.district)}>
                      İlçe zorunludur ve sayı içeremez.
                    </ValidationHint>
                  </div>
                  <div className="validation-field">
                    <input
                      type="text"
                      placeholder="Tam Adres"
                      className={validationClass(addressForm.fullAddress, addressValidation.fullAddress)}
                      value={addressForm.fullAddress}
                      onChange={(e) => setAddressForm({ ...addressForm, fullAddress: e.target.value })}
                    />
                    <ValidationHint state={getValidationState(addressForm.fullAddress, addressValidation.fullAddress)}>
                      Açık adres zorunludur.
                    </ValidationHint>
                  </div>
                  <input type="text" placeholder="Posta Kodu (opsiyonel)" value={addressForm.zipCode}
                    onChange={(e) => setAddressForm({ ...addressForm, zipCode: e.target.value })} />
                  <select value={addressForm.addressType}
                    onChange={(e) => setAddressForm({ ...addressForm, addressType: e.target.value })}>
                    <option value="Home">Ev</option>
                    <option value="Job">İş</option>
                    <option value="Other">Diğer</option>
                  </select>
                  <div className="form-buttons">
                    <button className="btn-secondary" onClick={() => setShowAddressForm(false)}>İptal</button>
                    <button className="btn-primary" onClick={handleAddAddress} disabled={!isAddressFormValid}>Kaydet</button>
                  </div>
                </div>
              </div>
            )}

            <div className="addresses-list">
              {addresses.length === 0 ? (
                <div className="addresses-empty">
                  <div className="addresses-empty-icon">📍</div>
                  <h3>Kayıtlı adresiniz bulunamadı</h3>
                  <p>Alışverişlerinizi daha hızlı tamamlamak için bir teslimat adresi ekleyebilirsiniz.</p>
                </div>
              ) : addresses.map((address) => {
                const addressId = getAddressId(address);
                return (
                <div className="address-card" key={addressId}>
                  {editingAddress === addressId ? (
                    <div className="address-edit-form form-step" style={{ flex: 1 }}>
                      <div className="validation-field">
                        <input
                          type="text"
                          placeholder="Şehir"
                          className={validationClass(editForm.city, editAddressValidation.city)}
                          value={editForm.city}
                          onChange={(e) => setEditForm({ ...editForm, city: e.target.value })}
                        />
                        <ValidationHint state={getValidationState(editForm.city, editAddressValidation.city)}>
                          Şehir zorunludur ve sayı içeremez.
                        </ValidationHint>
                      </div>
                      <div className="validation-field">
                        <input
                          type="text"
                          placeholder="İlçe"
                          className={validationClass(editForm.district, editAddressValidation.district)}
                          value={editForm.district}
                          onChange={(e) => setEditForm({ ...editForm, district: e.target.value })}
                        />
                        <ValidationHint state={getValidationState(editForm.district, editAddressValidation.district)}>
                          İlçe zorunludur ve sayı içeremez.
                        </ValidationHint>
                      </div>
                      <div className="validation-field">
                        <input
                          type="text"
                          placeholder="Tam Adres"
                          className={validationClass(editForm.fullAddress, editAddressValidation.fullAddress)}
                          value={editForm.fullAddress}
                          onChange={(e) => setEditForm({ ...editForm, fullAddress: e.target.value })}
                        />
                        <ValidationHint state={getValidationState(editForm.fullAddress, editAddressValidation.fullAddress)}>
                          Açık adres zorunludur.
                        </ValidationHint>
                      </div>
                      <input type="text" placeholder="Posta Kodu" value={editForm.zipCode}
                        onChange={(e) => setEditForm({ ...editForm, zipCode: e.target.value })} />
                      <select value={editForm.addressType}
                        onChange={(e) => setEditForm({ ...editForm, addressType: e.target.value })}>
                        <option value="Home">Ev</option>
                        <option value="Job">İş</option>
                        <option value="Other">Diğer</option>
                      </select>
                      <div className="form-buttons">
                        <button className="btn-secondary" onClick={() => setEditingAddress(null)}>İptal</button>
                        <button className="btn-primary" onClick={() => handleUpdateAddress(addressId)} disabled={!isEditAddressFormValid}>Kaydet</button>
                      </div>
                    </div>
                  ) : (
                    <>
                      <div className="address-info">
                        <span className="address-type-badge">{ADDRESS_TYPES[address.addressType]}</span>
                        <p>{address.fullAddress}</p>
                        <p>{address.district}, {address.city} {address.zipCode}</p>
                      </div>
                      <div className="address-actions">
                        <button className="btn-edit" onClick={() => {
                          setEditingAddress(addressId);
                          setEditForm({ city: address.city, district: address.district, fullAddress: address.fullAddress, zipCode: address.zipCode, addressType: address.addressType });
                        }}>
                          <FaPen />
                        </button>
                        <button className="btn-delete" onClick={() => handleDeleteAddress(addressId)}>
                          <FaTrash />
                        </button>
                      </div>
                    </>
                  )}
                </div>
                );
              })}
            </div>
          </div>
        )}

        {activeTab === "reviews" && (
          <div className="profile-card">
            <h2>Yorumlarım</h2>
            <div className="profile-reviews-list">
              {reviewsError ? (
                <p className="empty-text">{reviewsError}</p>
              ) : reviews.length === 0 ? (
                <p className="empty-text">Henüz yorumunuz bulunmuyor.</p>
              ) : (
                reviews.map((review) => (
                  <div className="profile-review-card" key={review.reviewId}>
                    <div className="profile-review-info">
                      <h3>{review.productName || `Ürün #${review.productId}`}</h3>
                      <div className="profile-review-meta">
                        <RatingStars rating={review.rating} size="sm" />
                        <span>{formatDate(review.createdAt)}</span>
                      </div>
                      {review.comment?.trim() && <p>{review.comment}</p>}
                    </div>
                    <button className="btn-edit-review" onClick={() => navigate(`/product/${review.productId}`)}>
                      Ürüne Git
                    </button>
                  </div>
                ))
              )}
            </div>
          </div>
        )}

        {/* ── Hesabı Kapat Tab ── */}
        {activeTab === "account" && (
          <div className="profile-card">
            <h2 className="danger-heading">Hesabı Kapat</h2>

            {deleteStep === "idle" && (
              <div className="account-close-section">
                <div className="account-close-warning">
                  <div className="warning-icon">⚠️</div>
                  <div className="warning-content">
                    <h3>Bu işlem geri alınamaz</h3>
                    <p>
                      Hesabınızı kapattığınızda tüm verileriniz, siparişleriniz, adresleriniz ve
                      yorumlarınız kalıcı olarak silinecektir. Bu işlem <strong>geri alınamaz</strong>.
                    </p>
                  </div>
                </div>
                <button className="btn-close-account" onClick={handleStartDeleteFlow}>
                  Hesabımı Kapatmak İstiyorum
                </button>
              </div>
            )}

            {deleteStep === "credentials" && (
              <div className="account-close-section">
                <div className="account-close-warning">
                  <div className="warning-icon">🔐</div>
                  <div className="warning-content">
                    <h3>Kimliğinizi doğrulayın</h3>
                    <p>Devam etmek için e-posta adresinizi ve şifrenizi girin.</p>
                  </div>
                </div>
                <form className="form-step delete-credentials-form" onSubmit={handleCredentialsSubmit}>
                  <input
                    type="email"
                    placeholder="E-posta adresiniz"
                    value={deleteCredentials.email}
                    onChange={(e) => setDeleteCredentials({ ...deleteCredentials, email: e.target.value })}
                    autoComplete="email"
                  />
                  <div className="password-field">
                    <input
                      type={showDeletePassword ? "text" : "password"}
                      placeholder="Şifreniz"
                      value={deleteCredentials.password}
                      onChange={(e) => setDeleteCredentials({ ...deleteCredentials, password: e.target.value })}
                      autoComplete="current-password"
                    />
                    <button type="button" onClick={() => setShowDeletePassword(p => !p)}>
                      {showDeletePassword ? <FaEyeSlash /> : <FaEye />}
                    </button>
                  </div>
                  <div className="form-buttons delete-form-buttons">
                    <button type="button" className="btn-secondary" onClick={handleCancelDeleteFlow} disabled={verifyingCredentials}>
                      Vazgeç
                    </button>
                    <button type="submit" className="btn-close-account" disabled={verifyingCredentials}>
                      {verifyingCredentials ? "Doğrulanıyor..." : "Devam Et"}
                    </button>
                  </div>
                </form>
              </div>
            )}

            {deleteStep === "confirm" && (
              <div className="account-close-section">
                <div className="account-close-warning account-close-warning--danger">
                  <div className="warning-icon">🗑️</div>
                  <div className="warning-content">
                    <h3>Son adım — Emin misiniz?</h3>
                    <p>
                      <strong>{user.eMail}</strong> hesabı kalıcı olarak silinecek.
                      Bu işlem geri alınamaz. Devam etmek istiyor musunuz?
                    </p>
                  </div>
                </div>
                <div className="form-buttons delete-form-buttons">
                  <button className="btn-secondary" onClick={handleCancelDeleteFlow} disabled={deletingAccount}>
                    Hayır, Vazgeç
                  </button>
                  <button className="btn-close-account" onClick={handleConfirmDelete} disabled={deletingAccount}>
                    {deletingAccount ? "Siliniyor..." : "Evet, Hesabımı Sil"}
                  </button>
                </div>
              </div>
            )}
          </div>
        )}
      </main>
    </div>
  );
}

export default ProfilePage;
