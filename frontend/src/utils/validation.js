export const EMAIL_PATTERN = /^[^@\s]+@[a-zA-Z0-9]([a-zA-Z0-9-]*[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9-]*[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$/;
export const TURKISH_PHONE_PATTERN = /^(\+90|0)?5\d{9}$/;
export const PERSON_NAME_PATTERN = /^[A-Za-zÇĞİÖŞÜçğıöşü\s'-]+$/;

export function hasValue(value) {
  return String(value ?? "").trim().length > 0;
}

export function isValidPersonName(value) {
  const normalized = String(value ?? "").trim();
  return normalized.length >= 3
    && normalized.length <= 45
    && PERSON_NAME_PATTERN.test(normalized);
}

export function isValidEmail(value) {
  return EMAIL_PATTERN.test(String(value ?? "").trim());
}

export function isValidTurkishPhone(value) {
  return TURKISH_PHONE_PATTERN.test(String(value ?? "").trim());
}

export function isValidPassword(value) {
  return String(value ?? "").length >= 7;
}

export function isValidAddressName(value) {
  const normalized = String(value ?? "").trim();
  return normalized.length > 0 && !/\d/.test(normalized);
}

export function isValidAbsoluteUrl(value) {
  const normalized = String(value ?? "").trim();
  if (!normalized) return true;

  try {
    return Boolean(new URL(normalized).protocol);
  } catch {
    return false;
  }
}

export function isValidNonNegativeNumber(value) {
  if (value === "" || value === null || value === undefined) return false;
  const number = Number(value);
  return Number.isFinite(number) && number >= 0;
}

export function isValidNonNegativeInteger(value) {
  return isValidNonNegativeNumber(value) && Number.isInteger(Number(value));
}

export function getValidationState(value, isValid, optional = false) {
  const empty = !hasValue(value);
  if (empty && optional) return "neutral";
  if (empty) return "neutral";
  return isValid ? "valid" : "invalid";
}
