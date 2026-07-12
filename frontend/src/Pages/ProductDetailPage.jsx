import { useEffect, useMemo, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import RatingStars from "../components/RatingStars";
import { addCartItem } from "../services/cartService";
import { isLoggedIn } from "../services/authService";
import { getProductById } from "../services/productService";
import { getProfile } from "../services/userService";
import { addReview, getReviewsByProductId, updateReview, deleteReview } from "../services/reviewService";
import { notify } from "../utils/notify";
import "./ProductDetailPage.css";

const PlaceHolderPageImage = "https://placehold.co/600x500?text=Ürün+Görseli";
const MAX_REVIEW_LENGTH = 2000;

function formatDate(date) {
  if (!date) return "";
  return new Date(date).toLocaleDateString("tr-TR");
}

function getStockInfo(stock) {
  if (stock <= 0)  return { label: "Stok Tükendi",      cls: "detail-stock detail-stock-out", icon: "❌" };
  if (stock < 5)   return { label: `Son ${stock} ürün!`, cls: "detail-stock detail-stock-low", icon: "🔴" };
  if (stock <= 10) return { label: `${stock} adet kaldı`, cls: "detail-stock detail-stock-mid", icon: "🟡" };
  return             { label: `${stock} adet stokta`, cls: "detail-stock detail-stock-ok",  icon: "🟢" };
}

function ProductDetailPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);
  const [reviews, setReviews] = useState([]);
  const [currentUser, setCurrentUser] = useState(null);
  const [userRating, setUserRating] = useState(0);
  const [reviewText, setReviewText] = useState("");
  const [loading, setLoading] = useState(true);
  const [productUnavailable, setProductUnavailable] = useState(false);
  const [submittingReview, setSubmittingReview] = useState(false);
  const [addingToCart, setAddingToCart] = useState(false);

  const currentUserId = currentUser?.id ?? currentUser?.userId;
  const userReview = useMemo(
    () => reviews.find((review) => review.userId === currentUserId),
    [reviews, currentUserId]
  );

  const avgRating = reviews.length
    ? (reviews.reduce((sum, review) => sum + review.rating, 0) / reviews.length).toFixed(1)
    : "0.0";
  const isReviewTooLong = reviewText.length > MAX_REVIEW_LENGTH;

  useEffect(() => {
    fetchPageData();
  }, [id]);

  useEffect(() => {
    let active = true;

    Promise.resolve().then(() => {
      if (!active) return;

      if (!userReview) {
        setUserRating(0);
        setReviewText("");
        return;
      }

      setUserRating(userReview.rating);
      setReviewText(userReview.comment || "");
    });

    return () => { active = false; };
  }, [userReview]);

  async function fetchPageData() {
    try {
      setLoading(true);
      setProductUnavailable(false);
      const [productData, reviewData] = await Promise.all([
        getProductById(id),
        fetchReviews()
      ]);

      setProduct(productData);
      setReviews(reviewData);

      if (isLoggedIn()) {
        try {
          setCurrentUser(await getProfile());
        } catch {
          setCurrentUser(null);
        }
      }
    } catch (error) {
      if (error?.response?.status === 404) {
        setProduct(null);
        setProductUnavailable(true);
        return;
      }

      notify.error(error?.response?.data?.message || "Ürün detayı alınamadı.");
    } finally {
      setLoading(false);
    }
  }

  async function fetchReviews() {
    try {
      return await getReviewsByProductId(id);
    } catch (error) {
      if (error?.response?.status === 404) return [];
      throw error;
    }
  }

  async function refreshReviews() {
    try {
      setReviews(await fetchReviews());
    } catch (error) {
      notify.error(error?.response?.data?.message || "Yorumlar alınamadı.");
    }
  }

  async function handleAddToCart() {
    if (!isLoggedIn()) {
      navigate("/login");
      return;
    }

    try {
      setAddingToCart(true);
      await addCartItem(product.productId ?? product.id, 1);
      window.dispatchEvent(new Event("cartUpdated"));
      notify.success("Ürün sepete eklendi.");
    } catch (error) {
      notify.error(error?.response?.data?.message || "Ürün sepete eklenemedi.");
    } finally {
      setAddingToCart(false);
    }
  }

  async function handleSubmitReview() {
    if (!isLoggedIn()) {
      navigate("/login");
      return;
    }

    if (!userRating) {
      notify.warning("Lütfen 1 ile 5 arasında yıldız seçin.");
      return;
    }

    if (isReviewTooLong) {
      notify.warning(`Yorum en fazla ${MAX_REVIEW_LENGTH} karakter olabilir.`);
      return;
    }

    try {
      setSubmittingReview(true);
      const reviewBody = {
        productId: Number(id),
        rating: userRating,
        comment: reviewText.trim() || null
      };

      if (userReview) {
        await updateReview(userReview.reviewId, {
          rating: reviewBody.rating,
          comment: reviewBody.comment
        });
        notify.success("Değerlendirmeniz güncellendi.");
      } else {
        await addReview(reviewBody);
        notify.success("Değerlendirmeniz eklendi.");
      }

      await refreshReviews();
    } catch (error) {
      notify.error(error?.response?.data?.message || "Değerlendirme kaydedilemedi.");
    } finally {
      setSubmittingReview(false);
    }
  }

  async function handleDeleteReview() {
    try {
      setSubmittingReview(true);
      await deleteReview(userReview.reviewId);
      notify.success("Değerlendirmeniz kaldırıldı.");
      await refreshReviews();
    } catch (error) {
      notify.error(error?.response?.data?.message || "Değerlendirme kaldırılamadı.");
    } finally {
      setSubmittingReview(false);
    }
  }

  if (loading) return <div className="detail-loading">Yükleniyor...</div>;
  if (productUnavailable) {
    return (
      <div className="product-unavailable-page">
        <div className="product-unavailable-card">
          <div className="product-unavailable-icon" aria-hidden="true">📦</div>
          <span className="product-unavailable-label">Ürün mevcut değil</span>
          <h1>Bu ürün artık satışta değil</h1>
          <p>
            Ürün mağazadan kaldırılmış olabilir. Sipariş geçmişinizdeki ürün
            bilgileri korunmaya devam eder.
          </p>
          <button type="button" onClick={() => navigate("/")}>
            Alışverişe Devam Et
          </button>
        </div>
      </div>
    );
  }
  if (!product) return <div className="detail-loading">Ürün bulunamadı.</div>;

  const stockInfo = getStockInfo(product.stock);

  return (
    <div className="detail-page">
      <div className="detail-container">
        <div className="detail-image-section">
          <div className="detail-image-wrapper">
            <img src={product.imageUrl || PlaceHolderPageImage} alt={product.productName} className="detail-image" />
          </div>
        </div>

        <div className="detail-info-section">
          <h1 className="detail-title">{product.productName}</h1>
          <p className="detail-description">{product.description}</p>

          <div className="detail-meta">
            <span className={stockInfo.cls}>{stockInfo.icon} {stockInfo.label}</span>
          </div>

          <div className="detail-price">{product.price.toLocaleString("tr-TR")}₺</div>
          <button className="btn-cart" disabled={addingToCart || product.stock <= 0} onClick={handleAddToCart}>
            🛒 {addingToCart ? "Ekleniyor..." : "Sepete Ekle"}
          </button>

          <div className="rating-summary">
            <RatingStars rating={Math.round(Number(avgRating))} />
            <span className="rating-score">{avgRating} / 5</span>
            <span className="rating-count">({reviews.length} değerlendirme)</span>
          </div>

          {isLoggedIn() ? (
            <div className="review-form">
              <h3>{userReview ? "Değerlendirmeni Güncelle" : "Değerlendirme Yap"}</h3>
              <RatingStars rating={userRating} interactive onRate={setUserRating} />

              <div className="review-textarea-field">
                <textarea
                  className={`review-textarea ${isReviewTooLong ? "review-textarea-invalid" : ""}`}
                  placeholder="Yorumunuzu yazın... (opsiyonel)"
                  value={reviewText}
                  onChange={(event) => setReviewText(event.target.value)}
                  rows={4}
                  aria-invalid={isReviewTooLong}
                />
                <span className={`review-character-count ${isReviewTooLong ? "limit-exceeded" : ""}`}>
                  {reviewText.length}/{MAX_REVIEW_LENGTH}
                </span>
              </div>

              <div className="review-form-actions">
                {userReview && (
                  <button
                    className="btn-delete-review"
                    disabled={submittingReview}
                    onClick={handleDeleteReview}
                  >
                    {submittingReview ? "Kaldırılıyor..." : "Değerlendirmeyi Kaldır"}
                  </button>
                )}
                <button
                  className="btn-submit-review"
                  disabled={submittingReview || isReviewTooLong}
                  onClick={handleSubmitReview}
                >
                  {submittingReview ? "Kaydediliyor..." : userReview ? "Güncelle" : "Gönder"}
                </button>
              </div>
            </div>
          ) : (
            <div className="review-login-box">
              <p>Değerlendirme yapmak için giriş yapmalısınız.</p>
              <button className="btn-submit-review" onClick={() => navigate("/login")}>Giriş Yap</button>
            </div>
          )}
        </div>
      </div>

      <div className="reviews-section">
        <h2>Kullanıcı Yorumları</h2>
        <div className="reviews-list">
          {reviews.length === 0 ? (
            <p className="empty-reviews">Bu ürün için henüz değerlendirme yok.</p>
          ) : (
            reviews.map((review) => (
              <div className="review-card" key={review.reviewId}>
                <div className="review-header">
                  <span className="review-user">{review.userName || `Kullanıcı #${review.userId}`}</span>
                  <RatingStars rating={review.rating} size="sm" />
                  <span className="review-date">{formatDate(review.createdAt)}</span>
                </div>
                {review.comment?.trim() && (
                  <p className="review-comment">{review.comment}</p>
                )}
              </div>
            ))
          )}
        </div>
      </div>
    </div>
  );
}

export default ProductDetailPage;
