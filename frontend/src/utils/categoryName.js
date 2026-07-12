const CATEGORY_NAME_NORMALIZATION = {
  "Bilgisayar Bile\u015fenleri ve Aksesuarlari": "Bilgisayar Bile\u015fenleri ve Aksesuarlar\u0131",
  "Bilgisayar Bilesenleri ve Aksesuarlari": "Bilgisayar Bile\u015fenleri ve Aksesuarlar\u0131",
  "TV ve Goruntu Sistemleri": "TV ve G\u00f6r\u00fcnt\u00fc Sistemleri",
  "TV ve G\u00f6r\u00fcnt\u00fc Sistemleri": "TV ve G\u00f6r\u00fcnt\u00fc Sistemleri",
  "Kamera ve Fotograf": "Kamera ve Foto\u011fraf",
  "Kamera ve Foto\u011fraf": "Kamera ve Foto\u011fraf",
  "Oyun ve Konsol": "Oyun Konsollar\u0131",
  "Oyun Konsollari": "Oyun Konsollar\u0131",
  "Oyun Konsollar\u0131": "Oyun Konsollar\u0131",
  "Giyilebilir Teknoloji": "Giyilebilir Teknoloji",
  "Ev Aletleri": "Ev Aletleri",
  "Ses Sistemleri": "Ses Sistemleri",
  "Bilgisayar ve Tablet": "Bilgisayar ve Tablet",
  "Telefon": "Telefon"
};

export function normalizeCategoryName(categoryName) {
  if (!categoryName) return categoryName;
  return CATEGORY_NAME_NORMALIZATION[categoryName] || categoryName;
}
