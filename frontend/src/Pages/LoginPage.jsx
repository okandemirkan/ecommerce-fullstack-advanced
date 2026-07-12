import { useState } from "react"
import "./RegisterPage.css";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { notify } from "../utils/notify";

function LoginPage() {
  const [formData, setFormData] = useState({ eMail: "", password: "" });
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const isFormValid = formData.eMail.trim() !== "" && formData.password.trim() !== "";

  async function handleLogin() {
    if (loading || !isFormValid) return;
    try {
      setLoading(true);
      await login(formData);
      notify.success("Giriş Başarılı!");
      navigate("/");
    } catch (error) {
      notify.error(error?.response?.data?.message || error?.message || "Hata oluştu");
    } finally {
      setLoading(false);
    }
  }

  function handleKeyDown(e) {
    if (e.key === "Enter" && isFormValid) handleLogin();
  }

  return (
    <div className="register-container">
      <div className="register-card">
        <h2>Giriş Yap</h2>
        <div className="form-step">
          <input
            type="email"
            placeholder="EMail"
            value={formData.eMail}
            onChange={(e) => setFormData({ ...formData, eMail: e.target.value })}
            onKeyDown={handleKeyDown}
            disabled={loading}
          />

          <div className="password-field">
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Şifre"
              value={formData.password}
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
              onKeyDown={handleKeyDown}
              disabled={loading}
            />
            <button type="button" onClick={() => setShowPassword(!showPassword)} disabled={loading}>
              {showPassword ? <FaEyeSlash /> : <FaEye />}
            </button>
          </div>

          <button className="btn-primary" onClick={handleLogin} disabled={loading || !isFormValid}>
            {loading ? "Giriş yapılıyor..." : "Giriş Yap"}
          </button>
        </div>
      </div>
    </div>
  );
}

export default LoginPage
