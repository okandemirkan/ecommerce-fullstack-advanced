import { useState } from "react";
import "./RegisterPage.css";
import { register } from "../services/authService";
import { useNavigate } from "react-router-dom";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { notify } from "../utils/notify";
import ValidationHint from "../components/ValidationHint";
import ResponsiveSelect from "../components/ResponsiveSelect";
import { ADDRESS_TYPES } from "../utils/address";
import {
  getValidationState,
  hasValue,
  isValidAddressName,
  isValidEmail,
  isValidPassword,
  isValidPersonName,
  isValidTurkishPhone
} from "../utils/validation";

function RegisterPage() {
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();
  const [step, setStep] = useState(1);
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    userName: '',
    eMail: '',
    phoneNumber: '',
    password: '',
    verifyPassword: '',
    address: {
      city: '',
      district: '',
      fullAddress: '',
      zipCode: '',
      addressType: 'Home'
    }
  });

  const accountValidation = {
    userName: isValidPersonName(formData.userName),
    eMail: isValidEmail(formData.eMail),
    phoneNumber: isValidTurkishPhone(formData.phoneNumber),
    password: isValidPassword(formData.password),
    verifyPassword: hasValue(formData.verifyPassword) && formData.password === formData.verifyPassword
  };
  const addressValidation = {
    city: isValidAddressName(formData.address.city),
    district: isValidAddressName(formData.address.district),
    fullAddress: hasValue(formData.address.fullAddress)
  };
  const isAccountValid = Object.values(accountValidation).every(Boolean);
  const isAddressValid = Object.values(addressValidation).every(Boolean);

  async function handleRegister() {
    if (loading) return;
    if (!isAccountValid || !isAddressValid) {
      notify.warning("Lütfen kırmızı işaretli alanları düzeltin.");
      return;
    }
    try {
      setLoading(true);
      await register(formData);
      notify.success("Kayıt Başarılı Giriş Yapabilirsiniz!");
      navigate("/login");
    } catch (error) {
      notify.error(error?.response?.data?.message || error?.message || "Hata oluştu");
    } finally {
      setLoading(false);
    }
  }

  // Step 1: Enter → ileri adıma geç; Step 2: Enter → kayıt ol
  function handleKeyDown(e) {
    if (e.key !== "Enter") return;
    if (step === 1 && isAccountValid) setStep(2);
    else handleRegister();
  }

  function inputClass(value, isValid, optional = false) {
    const state = getValidationState(value, isValid, optional);
    return state === "neutral" ? "" : `validation-input-${state}`;
  }

  return (
    <div className="register-container">
      <div className="register-card">
        <h2>{step === 1 ? "Hesap Oluştur" : "Adres Bilgileri"}</h2>
        <div className="step-indicator">
          <span className={step === 1 ? "active" : ""}>1</span>
          <span className={step === 2 ? "active" : ""}>2</span>
        </div>

        {step === 1 && (
          <div className="form-step">
            <div className="validation-field">
              <input
                type="text"
                placeholder="Ad-Soyad"
                className={inputClass(formData.userName, accountValidation.userName)}
                value={formData.userName}
                onChange={(e) => setFormData({ ...formData, userName: e.target.value })}
                onKeyDown={handleKeyDown}
              />
              <ValidationHint state={getValidationState(formData.userName, accountValidation.userName)}>
                3–45 karakter olmalı ve sayı içermemeli.
              </ValidationHint>
            </div>

            <div className="validation-field">
              <input
                type="email"
                placeholder="E-posta"
                className={inputClass(formData.eMail, accountValidation.eMail)}
                value={formData.eMail}
                onChange={(e) => setFormData({ ...formData, eMail: e.target.value })}
                onKeyDown={handleKeyDown}
              />
              <ValidationHint state={getValidationState(formData.eMail, accountValidation.eMail)}>
                Örnek: kullanici@site.com
              </ValidationHint>
            </div>

            <div className="validation-field">
              <input
                type="tel"
                placeholder="Telefon Numarası"
                className={inputClass(formData.phoneNumber, accountValidation.phoneNumber)}
                value={formData.phoneNumber}
                onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
                onKeyDown={handleKeyDown}
              />
              <ValidationHint state={getValidationState(formData.phoneNumber, accountValidation.phoneNumber)}>
                05xxxxxxxxx, 5xxxxxxxxx veya +905xxxxxxxxx formatında olmalı.
              </ValidationHint>
            </div>

            <div className="validation-field">
              <div className={`password-field ${inputClass(formData.password, accountValidation.password)}`}>
                <input
                  type={showPassword ? "text" : "password"}
                  placeholder="Şifre"
                  value={formData.password}
                  onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                  onKeyDown={handleKeyDown}
                />
                <button type="button" onClick={() => setShowPassword(!showPassword)}>
                  {showPassword ? <FaEyeSlash /> : <FaEye />}
                </button>
              </div>
              <ValidationHint state={getValidationState(formData.password, accountValidation.password)}>
                En az 7 karakter olmalı.
              </ValidationHint>
            </div>

            <div className="validation-field">
              <input
                type="password"
                placeholder="Şifre Doğrulama"
                className={inputClass(formData.verifyPassword, accountValidation.verifyPassword)}
                value={formData.verifyPassword}
                onChange={(e) => setFormData({ ...formData, verifyPassword: e.target.value })}
                onKeyDown={handleKeyDown}
              />
              <ValidationHint state={getValidationState(formData.verifyPassword, accountValidation.verifyPassword)}>
                Şifreler aynı olmalı.
              </ValidationHint>
            </div>

            <button className="btn-primary" onClick={() => setStep(2)} disabled={!isAccountValid}>İleri</button>
          </div>
        )}

        {step === 2 && (
          <div className="form-step">
            <div className="validation-field">
              <input
                type="text"
                placeholder="Şehir"
                className={inputClass(formData.address.city, addressValidation.city)}
                value={formData.address.city}
                onChange={(e) => setFormData({ ...formData, address: { ...formData.address, city: e.target.value } })}
                onKeyDown={handleKeyDown}
                disabled={loading}
              />
              <ValidationHint state={getValidationState(formData.address.city, addressValidation.city)}>
                Şehir adı boş bırakılamaz ve sayı içeremez.
              </ValidationHint>
            </div>

            <div className="validation-field">
              <input
                type="text"
                placeholder="İlçe"
                className={inputClass(formData.address.district, addressValidation.district)}
                value={formData.address.district}
                onChange={(e) => setFormData({ ...formData, address: { ...formData.address, district: e.target.value } })}
                onKeyDown={handleKeyDown}
                disabled={loading}
              />
              <ValidationHint state={getValidationState(formData.address.district, addressValidation.district)}>
                İlçe adı boş bırakılamaz ve sayı içeremez.
              </ValidationHint>
            </div>

            <div className="validation-field">
              <input
                type="text"
                placeholder="Tam Adres"
                className={inputClass(formData.address.fullAddress, addressValidation.fullAddress)}
                value={formData.address.fullAddress}
                onChange={(e) => setFormData({ ...formData, address: { ...formData.address, fullAddress: e.target.value } })}
                onKeyDown={handleKeyDown}
                disabled={loading}
              />
              <ValidationHint state={getValidationState(formData.address.fullAddress, addressValidation.fullAddress)}>
                Açık adres zorunludur.
              </ValidationHint>
            </div>

            <input
              type="text"
              placeholder="Posta Kodu (opsiyonel)"
              value={formData.address.zipCode}
              onChange={(e) => setFormData({ ...formData, address: { ...formData.address, zipCode: e.target.value } })}
              onKeyDown={handleKeyDown}
              disabled={loading}
            />

            <ResponsiveSelect
              value={formData.address.addressType}
              onChange={addressType => setFormData({ ...formData, address: { ...formData.address, addressType } })}
              options={Object.entries(ADDRESS_TYPES).map(([optionValue, label]) => ({ value: optionValue, label }))}
              ariaLabel="Adres türü"
              disabled={loading}
            />

            <div className="form-buttons">
              <button className="btn-secondary" onClick={() => setStep(1)} disabled={loading}>Geri</button>
              <button className="btn-primary" onClick={handleRegister} disabled={loading || !isAddressValid}>
                {loading ? "Kayıt yapılıyor..." : "Kayıt Ol"}
              </button>
            </div>
          </div>
        )}

      </div>
    </div>
  );
}

export default RegisterPage
