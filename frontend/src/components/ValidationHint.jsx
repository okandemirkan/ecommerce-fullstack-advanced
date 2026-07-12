import "./ValidationHint.css";

function ValidationHint({ state = "neutral", children }) {
  const icon = state === "valid" ? "✓" : state === "invalid" ? "!" : "•";

  return (
    <p className={`validation-hint validation-hint-${state}`} aria-live="polite">
      <span aria-hidden="true">{icon}</span>
      {children}
    </p>
  );
}

export default ValidationHint;
