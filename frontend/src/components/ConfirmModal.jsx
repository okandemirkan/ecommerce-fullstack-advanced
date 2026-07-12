import { useEffect } from "react";
import "./ConfirmModal.css";

/**
 * Tasarıma uygun onay modalı.
 *
 * Props:
 *   isOpen    {boolean}  – modalın görünüp görünmeyeceği
 *   title     {string}   – modal başlığı
 *   message   {string}   – onay sorusu
 *   onConfirm {function} – "Evet" tıklandığında çağrılır
 *   onCancel  {function} – "İptal" tıklandığında ya da backdrop'a basıldığında çağrılır
 *   confirmLabel {string} – onay butonu metni (varsayılan: "Evet, Devam Et")
 *   cancelLabel  {string} – iptal butonu metni (varsayılan: "İptal")
 *   variant   {"danger"|"warning"|"primary"|"success"} – buton rengi (varsayılan: "danger")
 */
function ConfirmModal({
  isOpen,
  title = "Emin misiniz?",
  message,
  onConfirm,
  onCancel,
  confirmLabel = "Evet, Devam Et",
  cancelLabel = "İptal",
  variant = "danger",
}) {
  // ESC tuşuyla kapat
  useEffect(() => {
    if (!isOpen) return;
    const handleKey = (e) => { if (e.key === "Escape") onCancel?.(); };
    window.addEventListener("keydown", handleKey);
    return () => window.removeEventListener("keydown", handleKey);
  }, [isOpen, onCancel]);

  if (!isOpen) return null;

  return (
    <div className="cm-backdrop" onClick={onCancel} role="dialog" aria-modal="true">
      <div className="cm-panel" onClick={(e) => e.stopPropagation()}>
        <div className={`cm-icon-wrap cm-icon-${variant}`}>
          {variant === "danger"  && <span>⚠️</span>}
          {variant === "warning" && <span>⚠️</span>}
          {variant === "primary" && <span>ℹ️</span>}
          {variant === "success" && <span>✅</span>}
        </div>

        <h3 className="cm-title">{title}</h3>
        {message && <p className="cm-message">{message}</p>}

        <div className="cm-actions">
          <button className="cm-btn cm-btn-cancel" onClick={onCancel}>
            {cancelLabel}
          </button>
          <button className={`cm-btn cm-btn-confirm cm-btn-${variant}`} onClick={onConfirm}>
            {confirmLabel}
          </button>
        </div>
      </div>
    </div>
  );
}

export default ConfirmModal;
