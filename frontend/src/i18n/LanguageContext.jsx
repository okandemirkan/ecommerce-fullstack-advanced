import { useCallback, useEffect, useMemo, useState } from "react";
import { reverseTranslations, translations } from "./translations";
import { LanguageContext } from "./language-context";

const LANGUAGE_STORAGE_KEY = "siteLanguage";
function getInitialLanguage() {
  const savedLanguage = localStorage.getItem(LANGUAGE_STORAGE_KEY);
  return savedLanguage === "en" ? "en" : "tr";
}

function translatePattern(trimmed, language) {
  const enPatterns = [
    [/^(\d+)\s+adet$/i, "$1 items"],
    [/^(\d+)\s+\u00fcr\u00fcn$/i, "$1 products"],
    [/^(\d+)\s+adet kald\u0131$/i, "$1 left"],
    [/^(\d+)\s+adet stokta$/i, "$1 in stock"],
    [/^Son\s+(\d+)\s+\u00fcr\u00fcn$/i, "Only $1 left"],
    [/^Birim fiyat:\s*(.+)$/i, "Unit price: $1"],
    [/^\u{1F6D2}\s*Sepete Ekle$/iu, "\u{1F6D2} Add to Cart"],
    [/^\u{1F6D2}\s*Ekleniyor\.\.\.$/iu, "\u{1F6D2} Adding..."],
    [/^(.+)\s+kullan\u0131c\u0131s\u0131n\u0131n sipari\u015fleri$/i, "Orders of $1"],
    [/^(.+)\s+'in sipari\u015fleri$/i, "Orders for $1"],
    [/^"(.+)" arama sonucu$/i, 'Search results for "$1"'],
    [/^Aktif\s*\((\d+)\)$/i, "Active ($1)"],
    [/^Pasif\s*\((\d+)\)$/i, "Inactive ($1)"],
    [/^Kullan\u0131c\u0131lar\s*\((\d+)\)$/i, "Users ($1)"],
    [/^Kategoriler\s*\((\d+)\)$/i, "Categories ($1)"],
    [/^Kategori\s+#(\d+)$/i, "Category #$1"],
    [/^(.+)\s+-\s+Sipari\u015fleri$/i, "$1 - Orders"],
    [/^Toplam Sipari\u015f:\s*(\d+)$/i, "Total Orders: $1"],
    [/^(\d+)\s+de\u011ferlendirme$/i, "$1 reviews"]
  ];

  const trPatterns = [
    [/^(\d+)\s+items$/i, "$1 adet"],
    [/^(\d+)\s+products$/i, "$1 \u00fcr\u00fcn"],
    [/^(\d+)\s+left$/i, "$1 adet kald\u0131"],
    [/^(\d+)\s+in stock$/i, "$1 adet stokta"],
    [/^Only\s+(\d+)\s+left$/i, "Son $1 \u00fcr\u00fcn"],
    [/^Unit price:\s*(.+)$/i, "Birim fiyat: $1"],
    [/^\u{1F6D2}\s*Add to Cart$/iu, "\u{1F6D2} Sepete Ekle"],
    [/^\u{1F6D2}\s*Adding\.\.\.$/iu, "\u{1F6D2} Ekleniyor..."],
    [/^Orders of\s+(.+)$/i, "$1 kullan\u0131c\u0131s\u0131n\u0131n sipari\u015fleri"],
    [/^Orders for\s+(.+)$/i, "$1'in sipari\u015fleri"],
    [/^Search results for\s+"(.+)"$/i, '"$1" arama sonucu'],
    [/^Active\s*\((\d+)\)$/i, "Aktif ($1)"],
    [/^Inactive\s*\((\d+)\)$/i, "Pasif ($1)"],
    [/^Users\s*\((\d+)\)$/i, "Kullan\u0131c\u0131lar ($1)"],
    [/^Categories\s*\((\d+)\)$/i, "Kategoriler ($1)"],
    [/^Category\s+#(\d+)$/i, "Kategori #$1"],
    [/^(.+)\s+-\s+Orders$/i, "$1 - Sipari\u015fleri"],
    [/^Total Orders:\s*(\d+)$/i, "Toplam Sipari\u015f: $1"],
    [/^(\d+)\s+reviews$/i, "$1 de\u011ferlendirme"]
  ];

  const patterns = language === "en" ? enPatterns : trPatterns;
  for (const [pattern, replacement] of patterns) {
    if (pattern.test(trimmed)) return trimmed.replace(pattern, replacement);
  }

  return null;
}

function translateText(value, language) {
  if (typeof value !== "string") return value;

  const trimmed = value.trim();
  if (!trimmed) return value;

  const dictionary = language === "en" ? translations : reverseTranslations;
  const translated = dictionary[trimmed] ?? translatePattern(trimmed, language);
  if (!translated) return value;

  return value.replace(trimmed, translated);
}

function translateElementAttributes(element, language) {
  ["placeholder", "title", "aria-label"].forEach((attribute) => {
    if (!element.hasAttribute?.(attribute)) return;
    const currentValue = element.getAttribute(attribute);
    const nextValue = translateText(currentValue, language);
    if (nextValue !== currentValue) element.setAttribute(attribute, nextValue);
  });
}

function translateNode(node, language) {
  if (!node) return;

  if (node.nodeType === Node.TEXT_NODE) {
    const nextValue = translateText(node.nodeValue, language);
    if (nextValue !== node.nodeValue) node.nodeValue = nextValue;
    return;
  }

  if (node.nodeType !== Node.ELEMENT_NODE) return;
  if (node.closest?.("[data-no-translate]")) return;

  translateElementAttributes(node, language);
  node.childNodes.forEach((childNode) => translateNode(childNode, language));
}

export function LanguageProvider({ children }) {
  const [language, setLanguage] = useState(getInitialLanguage);

  useEffect(() => {
    localStorage.setItem(LANGUAGE_STORAGE_KEY, language);
    document.documentElement.lang = language;
  }, [language]);

  useEffect(() => {
    const translatePage = () => translateNode(document.body, language);
    translatePage();

    const observer = new MutationObserver((mutations) => {
      mutations.forEach((mutation) => {
        mutation.addedNodes.forEach((node) => translateNode(node, language));
        if (mutation.type === "attributes" || mutation.type === "characterData") {
          translateNode(mutation.target, language);
        }
      });
    });

    observer.observe(document.body, {
      childList: true,
      subtree: true,
      attributes: true,
      characterData: true,
      attributeFilter: ["placeholder", "title", "aria-label"]
    });

    return () => observer.disconnect();
  }, [language]);

  const toggleLanguage = useCallback(() => {
    setLanguage((currentLanguage) => currentLanguage === "tr" ? "en" : "tr");
  }, []);

  const value = useMemo(() => ({ language, toggleLanguage }), [language, toggleLanguage]);

  return (
    <LanguageContext.Provider value={value}>
      {children}
    </LanguageContext.Provider>
  );
}
