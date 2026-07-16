import "./HomePage.css";
import { getAllProducts, getProductsByCategoryId, searchProductsByName } from "../services/productService";
import { getAllCategories } from "../services/categoryService";
import { getReviewsByProductId } from "../services/reviewService";
import { useState, useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { FaCheck, FaChevronDown, FaPlus, FaStar } from "react-icons/fa";
import { addCartItem } from "../services/cartService";
import { isLoggedIn } from "../services/authService";
import { notify } from "../utils/notify";
import { formatPrice } from "../utils/format";
import { normalizeCategoryName } from "../utils/categoryName";
import { useLanguage } from "../i18n/language-context";
import { translations } from "../i18n/translations";
import Pagination from "../components/Pagination";

function getStockInfo(stock) {
  if (stock <= 0)  return { label: "Tükendi",          cls: "stock-badge stock-out"  };
  if (stock < 5)   return { label: `Son ${stock} ürün`, cls: "stock-badge stock-low"  };
  if (stock <= 10) return { label: `${stock} adet`,     cls: "stock-badge stock-mid"  };
  return             { label: `${stock} adet`,           cls: "stock-badge stock-ok"   };
}

function calculateRatingSummary(reviews) {
  if (!Array.isArray(reviews) || reviews.length === 0) {
    return { average: 0, count: 0 };
  }

  const total = reviews.reduce((sum, review) => sum + Number(review.rating || 0), 0);
  return {
    average: Number((total / reviews.length).toFixed(1)),
    count: reviews.length
  };
}

const PAGE_SIZE = 12;
const SORT_OPTIONS = [
  { value: "Normal", tr: "Önerilen sıralama", en: "Recommended sorting" },
  { value: "LowToHigh", tr: "Fiyat: düşükten yükseğe", en: "Price: low to high" },
  { value: "HighToLow", tr: "Fiyat: yüksekten düşüğe", en: "Price: high to low" }
];

function getCategoryDisplayName(categoryName, language) {
  const normalizedName = normalizeCategoryName(categoryName);
  if (language !== "en") return normalizedName;
  const categoryMatch = normalizedName?.match(/^Kategori\s+#(\d+)$/i);
  if (categoryMatch) return `Category #${categoryMatch[1]}`;
  return translations[normalizedName] || normalizedName;
}

function HomePage() {
  const navigate = useNavigate();
  const { language } = useLanguage();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [addingProductId, setAddingProductId] = useState(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [categories, setCategories] = useState([]);
  const [ratingSummaries, setRatingSummaries] = useState({});
  const [selectedCategoryId, setSelectedCategoryId] = useState(null);
  const [sortType, setSortType] = useState("Normal");
  const [isSortMenuOpen, setIsSortMenuOpen] = useState(false);
  const [searchValue, setSearchValue] = useState("");
  const [activeSearch, setActiveSearch] = useState("");
  const sortMenuRef = useRef(null);

  useEffect(() => {
    function closeSortMenu(event) {
      if (!sortMenuRef.current?.contains(event.target)) setIsSortMenuOpen(false);
    }

    document.addEventListener("pointerdown", closeSortMenu);
    return () => document.removeEventListener("pointerdown", closeSortMenu);
  }, []);

  useEffect(() => {
    getAllCategories(1, 100)
      .then(data => setCategories(data.items ?? []))
      .catch(() => notify.error("Kategoriler alınamadı."));
  }, []);

  useEffect(() => {
    let active = true;

    async function fetchProducts() {
      try {
        setLoading(true);
        const pagedData = activeSearch
          ? await searchProductsByName(activeSearch, currentPage, PAGE_SIZE, sortType)
          : selectedCategoryId
            ? await getProductsByCategoryId(selectedCategoryId, currentPage, PAGE_SIZE, sortType)
            : await getAllProducts(currentPage, PAGE_SIZE, sortType);

        if (!active) return;
        const items = pagedData?.items ?? [];
        setProducts(items);
        setRatingSummaries({});
        setTotalPages(pagedData?.totalPages ?? 1);

        const summaries = await Promise.allSettled(
          items.map(async (product) => {
            const productId = product.id ?? product.productId;
            if (productId == null) return null;

            const reviews = await getReviewsByProductId(productId);
            return [productId, calculateRatingSummary(reviews)];
          })
        );

        if (!active) return;
        const nextRatingSummaries = {};
        summaries.forEach((result) => {
          if (result.status === "fulfilled" && result.value) {
            const [productId, summary] = result.value;
            nextRatingSummaries[productId] = summary;
          }
        });
        setRatingSummaries(nextRatingSummaries);
      } catch (error) {
        if (!active) return;
        setProducts([]);
        setRatingSummaries({});
        setTotalPages(1);
        notify.error(error?.response?.data?.message || "Ürünler alınamadı.");
      } finally {
        if (active) setLoading(false);
      }
    }

    fetchProducts();
    return () => { active = false; };
  }, [currentPage, selectedCategoryId, sortType, activeSearch]);

  function handleCategoryChange(categoryId) {
    setSelectedCategoryId(categoryId);
    setActiveSearch("");
    setSearchValue("");
    setCurrentPage(1);
  }

  function handleSortChange(nextSortType) {
    setSortType(nextSortType);
    setCurrentPage(1);
    setIsSortMenuOpen(false);
  }

  function handleProductSearch(event) {
    event.preventDefault();
    const query = searchValue.trim();
    if (query.length < 2) {
      notify.warning("Ürün aramak için en az 2 karakter girin.");
      return;
    }

    setSelectedCategoryId(null);
    setActiveSearch(query);
    setCurrentPage(1);
  }

  function clearProductSearch() {
    setSearchValue("");
    setActiveSearch("");
    setCurrentPage(1);
  }

  const handleAddToCart = async (event, product) => {
    event.stopPropagation();

    if (!isLoggedIn()) {
      navigate("/login");
      return;
    }

    const productId = product.id ?? product.productId;

    if (productId == null) {
      notify.warning("Ürün bilgisi eksik olduğu için sepete eklenemedi.");
      return;
    }

    try {
      setAddingProductId(productId);
      await addCartItem(productId, 1);
      window.dispatchEvent(new Event("cartUpdated"));
    } catch (error) {
      notify.error(error?.response?.data?.message || "Ürün sepete eklenemedi.");
    } finally {
      setAddingProductId(null);
    }
  };

  if (loading) return <div className="home-loading">Yükleniyor...</div>;

  return (
    <div className="home-container">
      <section className="category-filter" aria-label="Ürün kategorileri">
        <div className="category-filter-heading">
          <div>
            <span className="category-filter-eyebrow">Koleksiyonu keşfet</span>
            <h1>Kategoriye göre ürünler</h1>
          </div>
          <div className="product-toolbar">
            <span className="category-filter-count">
              {activeSearch
                ? `"${activeSearch}" arama sonucu`
                : selectedCategoryId
                  ? getCategoryDisplayName(categories.find(category => category.categoryId === selectedCategoryId)?.categoryName, language)
                  : "Tüm ürünler"}
            </span>
            <div
              className="sort-control"
              ref={sortMenuRef}
              onKeyDown={event => {
                if (event.key === "Escape") setIsSortMenuOpen(false);
              }}
            >
              <span>Sırala</span>
              <div className="sort-menu">
                <button
                  type="button"
                  className="sort-menu-trigger"
                  aria-haspopup="listbox"
                  aria-expanded={isSortMenuOpen}
                  onClick={() => setIsSortMenuOpen(current => !current)}
                >
                  <span>{SORT_OPTIONS.find(option => option.value === sortType)?.[language === "en" ? "en" : "tr"]}</span>
                  <FaChevronDown aria-hidden="true" />
                </button>
                {isSortMenuOpen && (
                  <div className="sort-menu-options" role="listbox" aria-label={language === "en" ? "Sort products" : "Ürünleri sırala"}>
                    {SORT_OPTIONS.map(option => (
                      <button
                        type="button"
                        role="option"
                        aria-selected={sortType === option.value}
                        className={sortType === option.value ? "selected" : ""}
                        key={option.value}
                        onClick={() => handleSortChange(option.value)}
                      >
                        <span>{option[language === "en" ? "en" : "tr"]}</span>
                        {sortType === option.value && <FaCheck aria-hidden="true" />}
                      </button>
                    ))}
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
        <form className="product-search-bar" onSubmit={handleProductSearch}>
          <input
            type="text"
            value={searchValue}
            onChange={event => setSearchValue(event.target.value)}
            placeholder="Ürün adıyla ara"
          />
          <button type="submit" disabled={searchValue.trim().length < 2}>Ara</button>
          {activeSearch && (
            <button type="button" className="product-search-clear" onClick={clearProductSearch}>
              Temizle
            </button>
          )}
        </form>
        <div className="category-chips">
          <button
            type="button"
            className={!selectedCategoryId ? "active" : ""}
            aria-pressed={!selectedCategoryId}
            onClick={() => handleCategoryChange(null)}
          >
            <span className="category-chip-mark">✦</span>
            <span>Tümü</span>
          </button>
          {categories.map(category => (
            <button
              type="button"
              key={category.categoryId}
              className={selectedCategoryId === category.categoryId ? "active" : ""}
              aria-pressed={selectedCategoryId === category.categoryId}
              onClick={() => handleCategoryChange(category.categoryId)}
            >
              <span className="category-chip-mark">{getCategoryDisplayName(category.categoryName, language)?.charAt(0)}</span>
              <span>{getCategoryDisplayName(category.categoryName, language)}</span>
            </button>
          ))}
        </div>
      </section>

      {products.length === 0 ? (
        <div className="home-empty">
          <h2>{activeSearch ? "Aramanızla eşleşen ürün bulunamadı" : "Bu kategoride henüz ürün yok"}</h2>
          <p>{activeSearch ? "Farklı bir ürün adı deneyebilirsiniz." : "Başka bir kategori seçerek ürünleri keşfetmeye devam edebilirsiniz."}</p>
        </div>
      ) : (
      <div className="products-grid">
        {products.map((product, index) => {
          const productId = product.id ?? product.productId;
          const stockInfo = getStockInfo(product.stock);
          const ratingSummary = ratingSummaries[productId];
          const hasRating = ratingSummary?.count > 0;

          return (
            <div className="product-card" key={productId ?? index} onClick={() => navigate(`/product/${productId}`)}>
              <button
                className="add-to-cart-btn"
                type="button"
                title="Sepete ekle"
                aria-label="Sepete ekle"
                disabled={addingProductId === productId || product.stock <= 0}
                onClick={(event) => handleAddToCart(event, product)}
              >
                <FaPlus />
              </button>
              <div className="product-image">
                <img src={product.imageUrl || "https://placehold.co/300x200?text=Ürün+Görseli"} alt={product.productName} />
                <div className={`product-rating-badge ${hasRating ? "" : "product-rating-empty"}`}>
                  <FaStar className="product-rating-star" aria-hidden="true" />
                  <span>{hasRating ? ratingSummary.average.toFixed(1) : "Yeni"}</span>
                  {hasRating && <span className="product-rating-count">({ratingSummary.count})</span>}
                </div>
              </div>
              <div className="product-info">
                {product.categoryName && <span className="product-category">{getCategoryDisplayName(product.categoryName, language)}</span>}
                <h3>{product.productName}</h3>
                <p>{product.description}</p>
                <div className="product-info-bottom">
                  <span className="price">{formatPrice(product.price)}₺</span>
                  <span className={stockInfo.cls}>{stockInfo.label}</span>
                </div>
              </div>
            </div>
          );
        })}
      </div>
      )}
      <Pagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={setCurrentPage}
        scrollToTopOnMobile
      />
    </div>
  );
}

export default HomePage;
