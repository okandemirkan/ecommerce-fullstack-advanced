import { useCallback, useState, useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import {
  getAllUsers, getUserById, getUserByMail, getUserByPhoneNumber, updateUser, makeAdmin, removeAdminRole,
  searchUsersByName,
  softDeleteUser, hardDeleteUser, restoreUser, getSoftDeletedUsers,
  getAddressesByUserId, addAddressByUserId, updateAddressByUserId, deleteAddressByAddressId,
  getOrdersByUserId, getLastOrders, addOrder, updateOrderStatus,
  addProduct, updateProduct, softDeleteProduct, hardDeleteProduct, addStock, updateProductImage, removeProductImage,
  getSoftDeletedProducts, restoreProduct, searchProductsByNameAsAdmin
} from "../services/adminService";
import RatingStars from "../components/RatingStars";
import ConfirmModal from "../components/ConfirmModal";
import Pagination from "../components/Pagination";
import { getAllProducts, getProductsByCategoryId } from "../services/productService";
import { deleteReviewAsAdmin, getReviewsByUserId } from "../services/reviewService";
import {
  addCategory, deleteCategory, getAllCategories, updateCategory
} from "../services/categoryService";
import { notify } from "../utils/notify";
import ValidationHint from "../components/ValidationHint";
import ResponsiveSelect from "../components/ResponsiveSelect";
import {
  getValidationState,
  hasValue,
  isValidAbsoluteUrl,
  isValidAddressName,
  isValidEmail,
  isValidNonNegativeInteger,
  isValidNonNegativeNumber,
  isValidPersonName,
  isValidTurkishPhone
} from "../utils/validation";
import { ADDRESS_TYPES, getAddressId } from "../utils/address";
import { normalizeCategoryName } from "../utils/categoryName";
import { getUserEmail, getUserId, normalizeUser } from "../utils/user";
import { getCurrentUserId } from "../services/authService";
import {
  STATUS_COLOR_MAP,
  STATUS_MAP,
  STATUS_TRANSITIONS,
  getStatusTransitionLabel
} from "../utils/orderStatus";
import "./AdminPage.css";

async function getUserDetailForSelection(user, userDetailOverride = null) {
  if (userDetailOverride) return userDetailOverride;

  const userId = getUserId(user);
  if (userId) return getUserById(userId);

  const email = getUserEmail(user);
  if (email) return getUserByMail(email);

  throw new Error("User identifier not found.");
}

const emptyAddress = { city: "", district: "", fullAddress: "", zipCode: "", addressType: "Home" };
const emptyProduct = { productName: "", categoryId: "", description: "", price: "", stock: "", imageUrl: "" };

function formatDate(date) {
  if (!date) return "";
  return new Date(date).toLocaleDateString("tr-TR");
}

function getUserTabLabel(tab) {
  const labels = {
    info: "Bilgiler",
    addresses: "Adresler",
    orders: "Siparişler",
    reviews: "Yorumlar"
  };
  return labels[tab];
}

// ─── USERS TAB ───────────────────────────────────────────────────────────────
const USER_PAGE_SIZE = 10;
const USER_DETAIL_ORDERS_PAGE_SIZE = 5;

function useMobileResultPanelScroll(targetKey) {
  const panelRef = useRef(null);
  const lastTargetKeyRef = useRef(null);

  useEffect(() => {
    if (!targetKey) {
      lastTargetKeyRef.current = null;
      return undefined;
    }

    if (lastTargetKeyRef.current === targetKey || !window.matchMedia("(max-width: 900px)").matches) {
      return undefined;
    }

    lastTargetKeyRef.current = targetKey;
    const animationFrame = window.requestAnimationFrame(() => {
      const panel = panelRef.current;
      if (!panel) return;

      const navbarHeight = document.querySelector(".navbar")?.getBoundingClientRect().height ?? 0;
      const targetTop = window.scrollY + panel.getBoundingClientRect().top - navbarHeight - 12;
      window.scrollTo({ top: Math.max(0, targetTop), behavior: "smooth" });
    });

    return () => window.cancelAnimationFrame(animationFrame);
  }, [targetKey]);

  return panelRef;
}

function UsersTab() {
  const navigate = useNavigate();
  const currentUserId = getCurrentUserId();
  const userDetailPanelRef = useRef(null);
  const lastAutoScrolledUserRef = useRef(null);
  // Aktif kullanıcılar
  const [users, setUsers] = useState([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  // Pasif kullanıcılar
  const [userView, setUserView] = useState("active"); // "active" | "deleted"
  const [deletedUsers, setDeletedUsers] = useState([]);
  const [deletedPage, setDeletedPage] = useState(1);
  const [deletedTotalPages, setDeletedTotalPages] = useState(1);
  const [deletedTotalCount, setDeletedTotalCount] = useState(0);
  const [deletedLoading, setDeletedLoading] = useState(false);
  const [selectedDeletedUser, setSelectedDeletedUser] = useState(null);
  const [restoreUserModal, setRestoreUserModal] = useState({ isOpen: false, user: null });
  const [searchType, setSearchType] = useState("email");
  const [searchValue, setSearchValue] = useState("");
  const [searchResult, setSearchResult] = useState(null);
  const [userSearchNotFound, setUserSearchNotFound] = useState(false);
  const [searchPage, setSearchPage] = useState(1);
  const [searchingUser, setSearchingUser] = useState(false);
  const [deletedUserIds, setDeletedUserIds] = useState(new Set());

  const [selectedUser, setSelectedUser] = useState(null);
  const [userDetail, setUserDetail] = useState(null);
  const [editForm, setEditForm] = useState({ userName: "", email: "", phoneNumber: "" });
  const [addresses, setAddresses] = useState([]);
  const [orders, setOrders] = useState([]);
  const [ordersPage, setOrdersPage] = useState(1);
  const [ordersTotalPages, setOrdersTotalPages] = useState(1);
  const [reviews, setReviews] = useState([]);
  const [showAddAddress, setShowAddAddress] = useState(false);
  const [addressForm, setAddressForm] = useState(emptyAddress);
  const [editingAddress, setEditingAddress] = useState(null);
  const [editAddressForm, setEditAddressForm] = useState(emptyAddress);
  const [activeUserTab, setActiveUserTab] = useState("info");
  // Soft delete modal
  const [softDeleteUserModal, setSoftDeleteUserModal] = useState(false);
  // Hard delete modal - iki aşamalı
  const [hardDeleteStep, setHardDeleteStep] = useState("idle"); // "idle" | "confirm1" | "confirm2"
  const [deleteReviewModal, setDeleteReviewModal] = useState({ isOpen: false, reviewId: null });
  const addressValidation = {
    city: isValidAddressName(addressForm.city),
    district: isValidAddressName(addressForm.district),
    fullAddress: hasValue(addressForm.fullAddress)
  };
  const editAddressValidation = {
    city: isValidAddressName(editAddressForm.city),
    district: isValidAddressName(editAddressForm.district),
    fullAddress: hasValue(editAddressForm.fullAddress)
  };
  const editUserValidation = {
    userName: isValidPersonName(editForm.userName),
    email: isValidEmail(editForm.email),
    phoneNumber: isValidTurkishPhone(editForm.phoneNumber)
  };
  const isAddressFormValid = Object.values(addressValidation).every(Boolean);
  const isEditAddressFormValid = Object.values(editAddressValidation).every(Boolean);
  const isEditUserFormValid = Object.values(editUserValidation).every(Boolean);
  const isSearchValid = searchType === "email"
    ? isValidEmail(searchValue)
    : searchType === "phone"
      ? isValidTurkishPhone(searchValue)
      : searchValue.trim().length >= 3;
  const searchValidationState = getValidationState(searchValue, isSearchValid);

  useEffect(() => {
    const selectedDetailUser = userDetail ?? selectedDeletedUser;
    const selectedDetailKey = selectedDetailUser
      ? `${userDetail ? "active" : "deleted"}:${getUserId(selectedDetailUser) ?? getUserEmail(selectedDetailUser)}`
      : userSearchNotFound
        ? `not-found:${searchType}:${searchValue.trim()}`
        : searchResult
          ? `search-result:${searchType}:${searchValue.trim()}:${searchPage}`
          : null;

    if (!selectedDetailKey) {
      lastAutoScrolledUserRef.current = null;
      return undefined;
    }

    if (lastAutoScrolledUserRef.current === selectedDetailKey || !window.matchMedia("(max-width: 900px)").matches) {
      return undefined;
    }

    lastAutoScrolledUserRef.current = selectedDetailKey;
    const animationFrame = window.requestAnimationFrame(() => {
      const detailPanel = userDetailPanelRef.current;
      if (!detailPanel) return;

      const navbarHeight = document.querySelector(".navbar")?.getBoundingClientRect().height ?? 0;
      const targetTop = window.scrollY + detailPanel.getBoundingClientRect().top - navbarHeight - 12;
      window.scrollTo({ top: Math.max(0, targetTop), behavior: "smooth" });
    });

    return () => window.cancelAnimationFrame(animationFrame);
  }, [userDetail, selectedDeletedUser, userSearchNotFound, searchResult, searchPage, searchType, searchValue]);

  function addressInputClass(value, isValid) {
    const state = getValidationState(value, isValid);
    return state === "neutral" ? "" : `validation-input-${state}`;
  }

  const fetchUsers = useCallback(async () => {
    try {
      const data = await getAllUsers(page, USER_PAGE_SIZE);
      setUsers(data.items);
      setTotalPages(data.totalPages);
      setTotalCount(data.totalCount);
    }
    catch { notify.error("Kullanıcılar alınamadı."); }
  }, [page]);

  const fetchDeletedUsers = useCallback(async () => {
    try {
      setDeletedLoading(true);
      const data = await getSoftDeletedUsers(deletedPage, USER_PAGE_SIZE);
      const items = data.items ?? [];
      setDeletedUsers(items);
      setDeletedTotalPages(data.totalPages ?? 1);
      setDeletedTotalCount(data.totalCount ?? 0);
      setDeletedUserIds(prev => new Set([...prev, ...items.map(getUserId).filter(Boolean)]));
    } catch (err) {
      if (err?.response?.status === 404) setDeletedUsers([]);
      else notify.error("Pasif kullanıcılar alınamadı.");
    } finally {
      setDeletedLoading(false);
    }
  }, [deletedPage]);

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchUsers();
    });
    return () => { active = false; };
  }, [fetchUsers]);

  async function handleUserSearch(event, pageToLoad = 1) {
    event?.preventDefault();
    if (!isSearchValid || searchingUser) return;

    try {
      setSearchingUser(true);
      setUserSearchNotFound(false);
      const result = searchType === "email"
        ? await getUserByMail(searchValue.trim())
        : searchType === "phone"
          ? await getUserByPhoneNumber(searchValue.trim())
          : await searchUsersByName(searchValue.trim(), pageToLoad, USER_PAGE_SIZE);

      try {
        const deletedData = await getSoftDeletedUsers(1, 10000);
        setDeletedUserIds(new Set((deletedData?.items ?? []).map(getUserId).filter(Boolean)));
      } catch {
        setDeletedUserIds(new Set());
      }

      setSearchResult(result);
      setUserSearchNotFound(searchType === "name" && (result?.items ?? []).length === 0);
      setSearchPage(pageToLoad);
      setSelectedUser(null);
      setUserDetail(null);
      setSelectedDeletedUser(null);
    } catch (err) {
      setSearchResult(null);
      setSelectedUser(null);
      setUserSearchNotFound(err?.response?.status === 404);
      if (err?.response?.status === 404) {
        notify.warning("Bu bilgilerle eşleşen kullanıcı bulunamadı.");
      } else {
        notify.error(err?.response?.data?.message || "Kullanıcı aranamadı.");
      }
    } finally {
      setSearchingUser(false);
    }
  }

  function clearUserSearch() {
    setSearchValue("");
    setSearchResult(null);
    setUserSearchNotFound(false);
    setSearchPage(1);
    setSelectedUser(null);
    setUserDetail(null);
    setSelectedDeletedUser(null);
  }

  function clearSelectedUserDetail() {
    setSelectedUser(null);
    setSelectedDeletedUser(null);
    setUserDetail(null);
    setAddresses([]);
    setOrders([]);
    setOrdersPage(1);
    setOrdersTotalPages(1);
    setReviews([]);
    setShowAddAddress(false);
    setEditingAddress(null);
    setActiveUserTab("info");
  }

  function switchUserView(view) {
    setUserView(view);
    setSelectedUser(null);
    setSelectedDeletedUser(null);
    setUserDetail(null);
    setHardDeleteStep("idle");
    clearUserSearch();
    if (view === "deleted") fetchDeletedUsers();
  }

  useEffect(() => {
    let active = true;

    Promise.resolve().then(() => {
      if (active && userView === "deleted") fetchDeletedUsers();
    });

    return () => { active = false; };
  }, [fetchDeletedUsers, userView]);

  function isUserDeleted(user) {
    return user?.isDeleted === true || deletedUserIds.has(getUserId(user));
  }

  function upsertUserById(list, nextUser) {
    const nextUserId = getUserId(nextUser);
    if (!nextUserId) return list;
    const exists = list.some(user => getUserId(user) === nextUserId);
    if (exists) {
      return list.map(user => getUserId(user) === nextUserId ? normalizeUser(user, nextUser) : user);
    }
    return [nextUser, ...list];
  }

  function removeUserById(list, userId) {
    return list.filter(user => getUserId(user) !== userId);
  }

  function updateSearchResultUser(nextUser) {
    const nextUserId = getUserId(nextUser);
    if (!nextUserId) return;

    setSearchResult(prev => {
      if (!prev) return prev;
      if (Array.isArray(prev.items)) {
        return {
          ...prev,
          items: prev.items.map(user => getUserId(user) === nextUserId ? normalizeUser(user, nextUser) : user)
        };
      }
      return getUserId(prev) === nextUserId ? normalizeUser(prev, nextUser) : prev;
    });
  }

  async function selectUser(user, userDetailOverride = null) {
    if (isUserDeleted(user)) {
      setSelectedDeletedUser(user);
      setSelectedUser(null);
      setUserDetail(null);
      setAddresses([]);
      setOrders([]);
      setOrdersPage(1);
      setOrdersTotalPages(1);
      setReviews([]);
      setHardDeleteStep("idle");
      return;
    }

    await selectActiveUser(user, userDetailOverride);
  }

  async function selectActiveUser(user, userDetailOverride = null) {
    let loadedUser;

    try {
      const detail = await getUserDetailForSelection(user, userDetailOverride);
      loadedUser = normalizeUser(user, detail);
      const normalizedDetail = normalizeUser(detail);
      setSelectedUser(loadedUser);
      setSelectedDeletedUser(null);
      setUserDetail(normalizedDetail);
      setEditForm({ userName: normalizedDetail.userName, email: normalizedDetail.eMail, phoneNumber: normalizedDetail.phoneNumber });
      setActiveUserTab("info");
      setHardDeleteStep("idle");
    } catch {
      notify.error("Kullanıcı detayı alınamadı.");
      return;
    }

    try {
      const addr = await getAddressesByUserId(getUserId(loadedUser));
      setAddresses(addr);
    } catch {
      setAddresses([]);
    }

    await fetchUserDetailOrders(loadedUser, 1);

    try {
      setReviews(await getReviewsByUserId(getUserId(loadedUser)));
    } catch {
      setReviews([]);
    }
  }

  async function fetchUserDetailOrders(user, pageToLoad = 1) {
    const userId = getUserId(user);
    if (!userId) return;

    try {
      const ord = await getOrdersByUserId(userId, pageToLoad, USER_DETAIL_ORDERS_PAGE_SIZE);
      setOrders(ord?.items ?? []);
      setOrdersPage(pageToLoad);
      setOrdersTotalPages(ord?.totalPages ?? 1);
    } catch {
      setOrders([]);
      setOrdersPage(1);
      setOrdersTotalPages(1);
    }
  }

  async function handleUpdateUser() {
    if (!isEditUserFormValid) {
      notify.warning("Lütfen kırmızı işaretli alanları düzeltin.");
      return;
    }
    try {
      await updateUser(getUserId(selectedUser), {
        userName: editForm.userName?.trim() || "",
        email: editForm.email?.trim() || "",
        phoneNumber: editForm.phoneNumber?.trim() || ""
      });
      notify.success("Kullanıcı güncellendi.");
      fetchUsers();
    } catch (err) { notify.error(err?.response?.data?.message || "Güncelleme başarısız."); }
  }

  async function handleMakeAdmin() {
    try { await makeAdmin(getUserId(selectedUser)); notify.success("Admin yapıldı."); selectUser(selectedUser); }
    catch (err) { notify.error(err?.response?.data?.message || "İşlem başarısız."); }
  }

  async function handleRemoveAdmin() {
    try { await removeAdminRole(getUserId(selectedUser)); notify.success("Admin rolü kaldırıldı."); selectUser(selectedUser); }
    catch (err) { notify.error(err?.response?.data?.message || "İşlem başarısız."); }
  }

  // Soft delete - pasife al
  async function handleSoftDeleteUserConfirmed() {
    setSoftDeleteUserModal(false);
    const targetUser = normalizeUser(selectedUser, userDetail);
    const targetId = getUserId(targetUser);
    if (!targetId) return;

    try {
      await softDeleteUser(targetId);
      const deletedUser = { ...targetUser, isDeleted: true };
      notify.success(`"${selectedUser.userName}" pasife alındı.`);
      setDeletedUserIds(prev => new Set([...prev, targetId]));
      setUsers(prev => removeUserById(prev, targetId));
      setDeletedUsers(prev => upsertUserById(prev, deletedUser));
      setTotalCount(prev => Math.max(0, prev - 1));
      setDeletedTotalCount(prev => prev + 1);
      updateSearchResultUser(deletedUser);
      setUserView("deleted");
      setSelectedUser(null);
      setUserDetail(null);
      setSelectedDeletedUser(deletedUser);
      fetchUsers();
      fetchDeletedUsers(); // pasif listesini de güncelle
    } catch (err) { notify.error(err?.response?.data?.message || "Pasife alma başarısız."); }
  }

  // Hard delete - birinci onay
  function openHardDeleteFirstConfirm() {
    setHardDeleteStep("confirm1");
  }

  // Hard delete - ikinci onay (kesin)
  async function handleHardDeleteConfirmed() {
    setHardDeleteStep("idle");
    const targetId = selectedUser ? getUserId(selectedUser) : getUserId(selectedDeletedUser);
    const targetName = selectedUser ? selectedUser.userName : selectedDeletedUser?.userName;
    try {
      await hardDeleteUser(targetId);
      notify.success(`"${targetName}" kalıcı olarak silindi.`);
      setDeletedUserIds(prev => {
        const next = new Set(prev);
        next.delete(targetId);
        return next;
      });
      setSelectedUser(null);
      setSelectedDeletedUser(null);
      setUserDetail(null);
      fetchUsers();
      fetchDeletedUsers();
    } catch (err) { notify.error(err?.response?.data?.message || "Kalıcı silme başarısız."); }
  }

  // Restore - pasif kullanıcıyı geri getir
  async function handleRestoreUserConfirmed() {
    const user = restoreUserModal.user;
    const userId = getUserId(user);
    setRestoreUserModal({ isOpen: false, user: null });
    if (!userId) return;

    try {
      await restoreUser(userId);
      const activeUser = { ...user, isDeleted: false };
      notify.success(`"${user.userName}" yeniden aktive edildi.`);
      setDeletedUserIds(prev => {
        const next = new Set(prev);
        next.delete(userId);
        return next;
      });
      setDeletedUsers(prev => removeUserById(prev, userId));
      setUsers(prev => upsertUserById(prev, activeUser));
      setDeletedTotalCount(prev => Math.max(0, prev - 1));
      setTotalCount(prev => prev + 1);
      updateSearchResultUser(activeUser);
      setSelectedDeletedUser(null);
      setUserView("active");
      fetchDeletedUsers();
      fetchUsers();
      selectActiveUser(activeUser, activeUser);
    } catch (err) { notify.error(err?.response?.data?.message || "Geri alma başarısız."); }
  }

  async function handleAddAddress() {
    if (!isAddressFormValid) {
      notify.warning("Lütfen adres alanlarını kontrol edin.");
      return;
    }
    try {
      await addAddressByUserId(getUserId(selectedUser), addressForm);
      setAddressForm(emptyAddress); setShowAddAddress(false);
      setAddresses(await getAddressesByUserId(getUserId(selectedUser)));
    } catch (err) { notify.error(err?.response?.data?.message || "Adres eklenemedi."); }
  }

  async function handleUpdateAddress(addressId) {
    if (!isEditAddressFormValid) {
      notify.warning("Lütfen adres alanlarını kontrol edin.");
      return;
    }
    try {
      await updateAddressByUserId(getUserId(selectedUser), addressId, editAddressForm);
      setEditingAddress(null);
      setAddresses(await getAddressesByUserId(getUserId(selectedUser)));
    } catch (err) { notify.error(err?.response?.data?.message || "Adres güncellenemedi."); }
  }

  async function handleDeleteAddress(addressId) {
    try {
      await deleteAddressByAddressId(getUserId(selectedUser), addressId);
      setAddresses(await getAddressesByUserId(getUserId(selectedUser)));
    } catch (err) { notify.error(err?.response?.data?.message || "Adres silinemedi."); }
  }

  async function handleDeleteReview(reviewId) {
    setDeleteReviewModal({ isOpen: true, reviewId });
  }

  async function handleDeleteReviewConfirmed() {
    const reviewId = deleteReviewModal.reviewId;
    setDeleteReviewModal({ isOpen: false, reviewId: null });
    try {
      await deleteReviewAsAdmin(reviewId, getUserId(selectedUser));
      setReviews(await getReviewsByUserId(getUserId(selectedUser)));
    } catch (err) {
      notify.error(err?.response?.data?.message || "Yorum silinemedi.");
    }
  }

  const searchUsers = searchType === "name" && searchResult
    ? searchResult.items ?? []
    : searchResult
      ? [searchResult]
      : [];

  return (
    <div className="admin-split users-admin-split">
      {/* Soft delete onay modalı */}
      <ConfirmModal
        isOpen={softDeleteUserModal}
        title="Kullanıcıyı Pasife Al"
        message={`"${selectedUser?.userName}" adlı kullanıcı pasife alınacak. Kullanıcı sisteme giriş yapamayacak ancak verileri korunacak. Devam etmek istiyor musunuz?`}
        confirmLabel="Evet, Pasife Al"
        cancelLabel="Vazgeç"
        variant="warning"
        onConfirm={handleSoftDeleteUserConfirmed}
        onCancel={() => setSoftDeleteUserModal(false)}
      />
      {/* Hard delete - 1. onay */}
      <ConfirmModal
        isOpen={hardDeleteStep === "confirm1"}
        title="Kalıcı Silme — Emin misiniz?"
        message={`"${selectedUser?.userName || selectedDeletedUser?.userName}" adlı kullanıcı ve tüm verileri kalıcı olarak silinecek. Bu işlem GERİ ALINAMAZ. Devam etmek istiyor musunuz?`}
        confirmLabel="Evet, Devam Et"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={() => setHardDeleteStep("confirm2")}
        onCancel={() => setHardDeleteStep("idle")}
      />
      {/* Hard delete - 2. onay */}
      <ConfirmModal
        isOpen={hardDeleteStep === "confirm2"}
        title="Son Onay — Kesinlikle emin misiniz?"
        message={`Bu işlem geri alınamaz. "${selectedUser?.userName || selectedDeletedUser?.userName}" kalıcı olarak silinsin mi?`}
        confirmLabel="Evet, Kalıcı Olarak Sil"
        cancelLabel="İptal"
        variant="danger"
        onConfirm={handleHardDeleteConfirmed}
        onCancel={() => setHardDeleteStep("idle")}
      />
      {/* Restore user onay modalı */}
      <ConfirmModal
        isOpen={restoreUserModal.isOpen}
        title="Kullanıcıyı Aktive Et"
        message={`"${restoreUserModal.user?.userName}" adlı kullanıcı yeniden aktive edilecek. Onaylıyor musunuz?`}
        confirmLabel="Evet, Aktive Et"
        cancelLabel="Vazgeç"
        variant="success"
        onConfirm={handleRestoreUserConfirmed}
        onCancel={() => setRestoreUserModal({ isOpen: false, user: null })}
      />
      <ConfirmModal
        isOpen={deleteReviewModal.isOpen}
        title="Yorumu Sil"
        message="Bu yorumu silmek istediğinize emin misiniz? Bu işlem geri alınamaz."
        confirmLabel="Evet, Sil"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={handleDeleteReviewConfirmed}
        onCancel={() => setDeleteReviewModal({ isOpen: false, reviewId: null })}
      />

      {/* Sol Panel */}
      <div className="admin-list-panel">
        {/* Aktif / Pasif Tab */}
        <div className="admin-user-tabs" style={{ marginBottom: 16 }}>
          <button
            className={userView === "active" ? "active" : ""}
            onClick={() => switchUserView("active")}
          >
            Aktif ({totalCount})
          </button>
          <button
            className={userView === "deleted" ? "active" : ""}
            onClick={() => switchUserView("deleted")}
          >
            Pasif {deletedTotalCount > 0 ? `(${deletedTotalCount})` : ""}
          </button>
        </div>

        <div className="admin-section-header" style={{ marginBottom: 12 }}>
          <h3 style={{ margin: 0 }}>
            {userView === "active" ? "Kullanıcılar" : "Pasif Kullanıcılar"}
          </h3>
          {userView === "deleted" && (
            <button className="btn-add-small" onClick={fetchDeletedUsers}>Yenile</button>
          )}
        </div>

        {userView === "active" && (
          <form className="admin-user-search" onSubmit={handleUserSearch}>
            <div className="admin-search-tabs">
              <button
                type="button"
                className={searchType === "email" ? "active" : ""}
                onClick={() => { setSearchType("email"); clearUserSearch(); }}
              >
                E-posta
              </button>
              <button
                type="button"
                className={searchType === "phone" ? "active" : ""}
                onClick={() => { setSearchType("phone"); clearUserSearch(); }}
              >
                Telefon
              </button>
              <button
                type="button"
                className={searchType === "name" ? "active" : ""}
                onClick={() => { setSearchType("name"); clearUserSearch(); }}
              >
                İsim
              </button>
            </div>
            <div className="admin-search-row">
              <input
                type={searchType === "email" ? "email" : searchType === "phone" ? "tel" : "text"}
                className={searchValidationState === "neutral" ? "" : `validation-input-${searchValidationState}`}
                placeholder={searchType === "email" ? "kullanici@site.com" : searchType === "phone" ? "05xxxxxxxxx" : "En az 3 harf girin"}
                value={searchValue}
                onChange={event => {
                  setSearchValue(event.target.value);
                  setSearchResult(null);
                  setUserSearchNotFound(false);
                }}
              />
              <button type="submit" disabled={!isSearchValid || searchingUser}>
                {searchingUser ? "..." : "Ara"}
              </button>
              {searchValue && (
                <button type="button" className="admin-search-clear" onClick={clearUserSearch} aria-label="Aramayı temizle">×</button>
              )}
            </div>
            <ValidationHint state={searchValidationState}>
              {searchType === "email"
                ? "Geçerli bir e-posta adresi girin."
                : searchType === "phone"
                  ? "05xxxxxxxxx, 5xxxxxxxxx veya +905xxxxxxxxx formatında girin."
                  : "İsim araması için en az 3 karakter girin."}
            </ValidationHint>
          </form>
        )}

        <div className="admin-list">
          {userView === "active" ? (
            users.map((user) => (
              <div
                key={getUserId(user) ?? getUserEmail(user)}
                className={`admin-list-item ${getUserId(selectedUser) === getUserId(user) ? "active" : ""}`}
                onClick={() => selectUser(user)}
              >
                <div className="admin-list-avatar">{user.userName?.charAt(0).toUpperCase()}</div>
                <div>
                  <div className="admin-list-name">{user.userName}</div>
                  <div className="admin-list-sub">{user.eMail}</div>
                </div>
                {user.role === "Admin" && <span className="role-badge">Admin</span>}
              </div>
            ))
          ) : deletedLoading ? (
            <p className="empty-text">Yükleniyor...</p>
          ) : deletedUsers.length === 0 ? (
            <p className="empty-text">Pasife alınmış kullanıcı yok.</p>
          ) : (
            deletedUsers.map((user) => (
              <div
                key={getUserId(user) ?? getUserEmail(user)}
                className={`admin-list-item ${getUserId(selectedDeletedUser) === getUserId(user) ? "active" : ""}`}
                onClick={() => {
                  setSelectedDeletedUser(user);
                  setSelectedUser(null);
                  setUserDetail(null);
                  setHardDeleteStep("idle");
                }}
              >
                <div className="admin-list-avatar" style={{ opacity: 0.5 }}>{user.userName?.charAt(0).toUpperCase()}</div>
                <div style={{ flex: 1, minWidth: 0 }}>
                  <div className="admin-list-name" style={{ color: "var(--text-muted)" }}>{user.userName}</div>
                  <div className="admin-list-sub">{user.eMail}</div>
                </div>
                <span className="deleted-badge">Pasif</span>
              </div>
            ))
          )}
        </div>
        {userView === "active" ? (
          <Pagination currentPage={page} totalPages={totalPages} onPageChange={setPage} />
        ) : userView === "deleted" ? (
          <Pagination currentPage={deletedPage} totalPages={deletedTotalPages} onPageChange={setDeletedPage} />
        ) : null}
      </div>

      {/* Sağ Panel */}
      {userView === "active" && !selectedDeletedUser ? (
        userDetail ? (
          <div ref={userDetailPanelRef} className="admin-detail-panel">
            <div className="admin-detail-header">
              <div className="admin-detail-avatar">{userDetail.userName?.charAt(0).toUpperCase()}</div>
              <div>
                <h3>{userDetail.userName}</h3>
                <p>{userDetail.eMail}</p>
              </div>
              <div className="admin-detail-actions">
                {userDetail.role === "Admin"
                  ? <button className="btn-warning" onClick={handleRemoveAdmin} disabled={getUserId(userDetail) === currentUserId}>Admin Rolünü Kaldır</button>
                  : <button className="btn-success" onClick={handleMakeAdmin}>Admin Yap</button>
                }
                <button className="btn-warning" onClick={() => setSoftDeleteUserModal(true)} disabled={getUserId(userDetail) === currentUserId}>Pasife Al</button>
                <button className="btn-danger" onClick={openHardDeleteFirstConfirm} disabled={getUserId(userDetail) === currentUserId}>Kalıcı Sil</button>
                <button
                  type="button"
                  className="btn-close-detail"
                  onClick={clearSelectedUserDetail}
                  aria-label="Kullanıcı detayını kapat"
                  title="Genel ekrana dön"
                >
                  ×
                </button>
              </div>
            </div>

            <div className="admin-user-tabs">
              {["info", "addresses", "orders", "reviews"].map(tab => (
                <button key={tab} className={activeUserTab === tab ? "active" : ""} onClick={() => setActiveUserTab(tab)}>
                  {getUserTabLabel(tab)}
                </button>
              ))}
            </div>

            {activeUserTab === "info" && (
              <div className="form-step">
                <div className="validation-field">
                  <input
                    type="text"
                    placeholder="Ad Soyad"
                    className={addressInputClass(editForm.userName, editUserValidation.userName)}
                    value={editForm.userName}
                    onChange={e => setEditForm({ ...editForm, userName: e.target.value })}
                  />
                  <ValidationHint state={getValidationState(editForm.userName, editUserValidation.userName)}>
                    3-45 karakter olmalı ve sayı içermemeli.
                  </ValidationHint>
                </div>
                <div className="validation-field">
                  <input
                    type="email"
                    placeholder="E-Mail"
                    className={addressInputClass(editForm.email, editUserValidation.email)}
                    value={editForm.email}
                    onChange={e => setEditForm({ ...editForm, email: e.target.value })}
                  />
                  <ValidationHint state={getValidationState(editForm.email, editUserValidation.email)}>
                    Geçerli bir e-posta adresi girin.
                  </ValidationHint>
                </div>
                <div className="validation-field">
                  <input
                    type="tel"
                    placeholder="Telefon"
                    className={addressInputClass(editForm.phoneNumber, editUserValidation.phoneNumber)}
                    value={editForm.phoneNumber}
                    onChange={e => setEditForm({ ...editForm, phoneNumber: e.target.value })}
                  />
                  <ValidationHint state={getValidationState(editForm.phoneNumber, editUserValidation.phoneNumber)}>
                    Türkiye cep telefonu formatında olmalı.
                  </ValidationHint>
                </div>
                <button className="btn-primary" onClick={handleUpdateUser} disabled={!isEditUserFormValid}>Güncelle</button>
              </div>
            )}

            {activeUserTab === "addresses" && (
              <div className="admin-addresses">
                <div className="admin-section-header">
                  <span>{addresses.length} Adres</span>
                  {addresses.length < 3 && (
                    <button className="btn-add-small" onClick={() => setShowAddAddress(!showAddAddress)}>+ Adres Ekle</button>
                  )}
                </div>
                {showAddAddress && (
                  <div className="form-step admin-form-box">
                    <div className="validation-field">
                      <input
                        placeholder="Şehir"
                        className={addressInputClass(addressForm.city, addressValidation.city)}
                        value={addressForm.city}
                        onChange={e => setAddressForm({ ...addressForm, city: e.target.value })}
                      />
                      <ValidationHint state={getValidationState(addressForm.city, addressValidation.city)}>
                        Şehir zorunludur ve sayı içeremez.
                      </ValidationHint>
                    </div>
                    <div className="validation-field">
                      <input
                        placeholder="İlçe"
                        className={addressInputClass(addressForm.district, addressValidation.district)}
                        value={addressForm.district}
                        onChange={e => setAddressForm({ ...addressForm, district: e.target.value })}
                      />
                      <ValidationHint state={getValidationState(addressForm.district, addressValidation.district)}>
                        İlçe zorunludur ve sayı içeremez.
                      </ValidationHint>
                    </div>
                    <div className="validation-field">
                      <input
                        placeholder="Tam Adres"
                        className={addressInputClass(addressForm.fullAddress, addressValidation.fullAddress)}
                        value={addressForm.fullAddress}
                        onChange={e => setAddressForm({ ...addressForm, fullAddress: e.target.value })}
                      />
                      <ValidationHint state={getValidationState(addressForm.fullAddress, addressValidation.fullAddress)}>
                        Açık adres zorunludur.
                      </ValidationHint>
                    </div>
                    <input placeholder="Posta Kodu" value={addressForm.zipCode} onChange={e => setAddressForm({ ...addressForm, zipCode: e.target.value })} />
                    <ResponsiveSelect
                      value={addressForm.addressType}
                      onChange={addressType => setAddressForm({ ...addressForm, addressType })}
                      options={Object.entries(ADDRESS_TYPES).map(([optionValue, label]) => ({ value: optionValue, label }))}
                      ariaLabel="Adres türü"
                    />
                    <div className="form-buttons">
                      <button className="btn-secondary" onClick={() => setShowAddAddress(false)}>İptal</button>
                      <button className="btn-primary" onClick={handleAddAddress} disabled={!isAddressFormValid}>Kaydet</button>
                    </div>
                  </div>
                )}
                {addresses.map(addr => {
                  const addressId = getAddressId(addr);
                  return (
                  <div className="address-card" key={addressId}>
                    {editingAddress === addressId ? (
                      <div className="form-step" style={{ flex: 1 }}>
                        <div className="validation-field">
                          <input
                            placeholder="Şehir"
                            className={addressInputClass(editAddressForm.city, editAddressValidation.city)}
                            value={editAddressForm.city}
                            onChange={e => setEditAddressForm({ ...editAddressForm, city: e.target.value })}
                          />
                          <ValidationHint state={getValidationState(editAddressForm.city, editAddressValidation.city)}>
                            Şehir zorunludur ve sayı içeremez.
                          </ValidationHint>
                        </div>
                        <div className="validation-field">
                          <input
                            placeholder="İlçe"
                            className={addressInputClass(editAddressForm.district, editAddressValidation.district)}
                            value={editAddressForm.district}
                            onChange={e => setEditAddressForm({ ...editAddressForm, district: e.target.value })}
                          />
                          <ValidationHint state={getValidationState(editAddressForm.district, editAddressValidation.district)}>
                            İlçe zorunludur ve sayı içeremez.
                          </ValidationHint>
                        </div>
                        <div className="validation-field">
                          <input
                            placeholder="Tam Adres"
                            className={addressInputClass(editAddressForm.fullAddress, editAddressValidation.fullAddress)}
                            value={editAddressForm.fullAddress}
                            onChange={e => setEditAddressForm({ ...editAddressForm, fullAddress: e.target.value })}
                          />
                          <ValidationHint state={getValidationState(editAddressForm.fullAddress, editAddressValidation.fullAddress)}>
                            Açık adres zorunludur.
                          </ValidationHint>
                        </div>
                        <input placeholder="Posta Kodu" value={editAddressForm.zipCode} onChange={e => setEditAddressForm({ ...editAddressForm, zipCode: e.target.value })} />
                        <ResponsiveSelect
                          value={editAddressForm.addressType}
                          onChange={addressType => setEditAddressForm({ ...editAddressForm, addressType })}
                          options={Object.entries(ADDRESS_TYPES).map(([optionValue, label]) => ({ value: optionValue, label }))}
                          ariaLabel="Adres türü"
                        />
                        <div className="form-buttons">
                          <button className="btn-secondary" onClick={() => setEditingAddress(null)}>İptal</button>
                          <button className="btn-primary" onClick={() => handleUpdateAddress(addressId)} disabled={!isEditAddressFormValid}>Kaydet</button>
                        </div>
                      </div>
                    ) : (
                      <>
                        <div className="address-info">
                          <span className="address-type-badge">{ADDRESS_TYPES[addr.addressType]}</span>
                          <p>{addr.fullAddress}</p>
                          <p>{addr.district}, {addr.city} {addr.zipCode}</p>
                        </div>
                        <div className="address-actions">
                          <button className="btn-edit-sm" onClick={() => { setEditingAddress(addressId); setEditAddressForm({ city: addr.city, district: addr.district, fullAddress: addr.fullAddress, zipCode: addr.zipCode, addressType: addr.addressType }); }}>✏️</button>
                          <button className="btn-delete-sm" onClick={() => handleDeleteAddress(addressId)}>Sil</button>
                        </div>
                      </>
                    )}
                  </div>
                  );
                })}
              </div>
            )}

            {activeUserTab === "orders" && (
              <div className="admin-orders">
                {orders.length === 0 ? (
                  <p className="empty-text">Bu kullanıcıya ait sipariş bulunmuyor.</p>
                ) : (
                  <>
                    {orders.map(order => (
                      <div className="admin-order-card" key={order.orderId}>
                        <div>
                          {(order.items ?? []).map(item => (
                            <div key={item.orderItemId} className="order-product">
                              {item.productName} <span className="order-meta">× {item.quantity}</span>
                            </div>
                          ))}
                          <div className="order-meta">{new Date(order.createdAt).toLocaleDateString("tr-TR")}</div>
                          <div className="order-meta">{order.shippingAddress}</div>
                        </div>
                        <div style={{ textAlign: "right" }}>
                          <div className="order-price">{order.totalPrice.toLocaleString("tr-TR")}₺</div>
                          <span className={`order-status-badge ${STATUS_COLOR_MAP[order.orderStatus]}`}>
                            {STATUS_MAP[order.orderStatus]}
                          </span>
                        </div>
                      </div>
                    ))}
                    <Pagination
                      currentPage={ordersPage}
                      totalPages={ordersTotalPages}
                      onPageChange={(nextPage) => fetchUserDetailOrders(selectedUser, nextPage)}
                    />
                  </>
                )}
              </div>
            )}

            {activeUserTab === "reviews" && (
              <div className="admin-reviews">
                {reviews.length === 0 ? (
                  <p className="empty-text">Bu kullanıcıya ait yorum bulunmuyor.</p>
                ) : (
                  reviews.map(review => (
                    <div className="admin-review-card" key={review.reviewId}>
                      <div className="admin-review-info">
                        <div className="order-product">{review.productName || `Ürün #${review.productId}`}</div>
                        <div className="admin-review-meta">
                          <RatingStars rating={review.rating} size="sm" />
                          <span>{formatDate(review.createdAt)}</span>
                        </div>
                        {review.comment?.trim() && <p>{review.comment}</p>}
                      </div>
                      <div className="admin-review-actions">
                        {review.productId && (
                          <button
                            className="btn-secondary btn-sm"
                            onClick={() => navigate(`/product/${review.productId}`)}
                          >
                            Ürüne Git
                          </button>
                        )}
                        <button className="btn-danger" onClick={() => handleDeleteReview(review.reviewId)}>
                          Yorumu Sil
                        </button>
                      </div>
                    </div>
                  ))
                )}
              </div>
            )}
          </div>
        ) : searchResult || userSearchNotFound ? (
          <div ref={userDetailPanelRef} className="admin-detail-panel admin-search-results-panel">
            <div className="admin-section-title-row">
              <div>
                <span className="admin-section-kicker">Arama Sonuçları</span>
                <h3>
                  {userSearchNotFound
                    ? "0 kullanıcı bulundu"
                    : searchType === "name"
                    ? `${searchResult.totalCount ?? searchUsers.length} kullanıcı bulundu`
                    : "1 kullanıcı bulundu"}
                </h3>
              </div>
              <button className="btn-secondary btn-sm" onClick={clearUserSearch}>
                Aramayı Temizle
              </button>
            </div>

            {searchUsers.length === 0 ? (
              <div className="admin-empty-state">
                <div className="admin-empty-state-icon">Fatura</div>
                <h4>Kullanıcı bulunamadı</h4>
                <p>Arama kriterini değiştirerek tekrar deneyebilirsiniz.</p>
              </div>
            ) : (
              <>
                <div className="admin-search-results-grid">
                  {searchUsers.map(user => (
                    <button
                      type="button"
                      className="admin-search-result-card"
                      key={getUserId(user) ?? getUserEmail(user)}
                      onClick={() => selectUser(user, searchType === "name" ? null : user)}
                    >
                      <div className="admin-list-avatar">{user.userName?.charAt(0).toUpperCase()}</div>
                      <div className="admin-search-result-info">
                        <h4>{user.userName}</h4>
                        <p>{user.eMail}</p>
                        <span>{user.phoneNumber}</span>
                      </div>
                      {isUserDeleted(user) && <span className="deleted-badge">Pasif</span>}
                      {user.role === "Admin" && <span className="role-badge">Admin</span>}
                    </button>
                  ))}
                </div>
                {searchType === "name" && (
                  <Pagination
                    currentPage={searchPage}
                    totalPages={searchResult.totalPages ?? 1}
                    onPageChange={(nextPage) => handleUserSearch(null, nextPage)}
                  />
                )}
              </>
            )}
          </div>
        ) : (
          <div className="admin-empty-detail">
            <p>Detaylarını görmek için bir kullanıcı seçin.</p>
          </div>
        )
      ) : (
        /* Pasif kullanıcı sağ panel */
        selectedDeletedUser ? (
          <div ref={userDetailPanelRef} className="admin-detail-panel">
            <div className="admin-detail-header">
              <div className="admin-detail-avatar" style={{ opacity: 0.5 }}>{selectedDeletedUser.userName?.charAt(0).toUpperCase()}</div>
              <div>
                <h3>{selectedDeletedUser.userName}</h3>
                <p>{selectedDeletedUser.eMail}</p>
                <span className="deleted-badge" style={{ marginTop: 4, display: "inline-block" }}>Pasif / Devre Dışı</span>
              </div>
              <div style={{ display: "flex", gap: "8px" }}>
                <button
                  className="btn-restore"
                  onClick={() => setRestoreUserModal({ isOpen: true, user: selectedDeletedUser })}
                >
                  Yeniden Aktive Et
                </button>
                <button
                  className="btn-danger"
                  onClick={() => setHardDeleteStep("confirm1")}
                >
                  Kalıcı Sil
                </button>
              </div>
            </div>
            <div className="deleted-restore-note">
              ⚠️ Bu kullanıcı pasife alınmış ve sisteme giriş yapamıyor. "Yeniden Aktive Et" butonu ile hesabı tekrar aktif edebilirsiniz veya "Kalıcı Sil" butonu ile tamamen silebilirsiniz.
            </div>
          </div>
        ) : (
          <div className="admin-empty-detail">
            <p>Detaylarını görmek için bir pasif kullanıcı seçin.</p>
          </div>
        )
      )}
    </div>
  );
}

// ─── PRODUCTS TAB ─────────────────────────────────────────────────────────────
const PRODUCT_PAGE_SIZE = 10;

function ProductsTab() {
  // Aktif Ürünler
  const [products, setProducts] = useState([]);
  const [productPage, setProductPage] = useState(1);
  const [productTotalPages, setProductTotalPages] = useState(1);
  const [productTotalCount, setProductTotalCount] = useState(0);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [editForm, setEditForm] = useState(emptyProduct);
  const [stockQty, setStockQty] = useState(1);
  const [showAddForm, setShowAddForm] = useState(false);
  const [addForm, setAddForm] = useState(emptyProduct);
  const [deleteProductModal, setDeleteProductModal] = useState(false);
  const [hardDeleteProductModal, setHardDeleteProductModal] = useState(false);
  const [imageUrlInput, setImageUrlInput] = useState("");
  const [imageLoading, setImageLoading] = useState(false);
  const [categories, setCategories] = useState([]);
  const [selectedProductCategoryId, setSelectedProductCategoryId] = useState("");
  const addValidation = {
    productName: hasValue(addForm.productName),
    categoryId: hasValue(addForm.categoryId),
    price: isValidNonNegativeNumber(addForm.price),
    stock: isValidNonNegativeInteger(addForm.stock),
    imageUrl: isValidAbsoluteUrl(addForm.imageUrl)
  };
  const editValidation = {
    productName: hasValue(editForm.productName),
    categoryId: hasValue(editForm.categoryId),
    price: isValidNonNegativeNumber(editForm.price),
    stock: isValidNonNegativeInteger(editForm.stock),
    imageUrl: isValidAbsoluteUrl(editForm.imageUrl)
  };
  const isAddFormValid = Object.values(addValidation).every(Boolean);
  const isEditFormValid = Object.values(editValidation).every(Boolean);

  function validationClass(value, isValid, optional = false) {
    const state = getValidationState(value, isValid, optional);
    return state === "neutral" ? "" : `validation-input-${state}`;
  }

  // Pasif Ürünler
  const [productView, setProductView] = useState("active"); // "active" | "deleted"
  const [deletedProducts, setDeletedProducts] = useState([]);
  const [deletedProductPage, setDeletedProductPage] = useState(1);
  const [deletedProductTotalPages, setDeletedProductTotalPages] = useState(1);
  const [deletedProductTotalCount, setDeletedProductTotalCount] = useState(0);
  const [selectedDeletedProduct, setSelectedDeletedProduct] = useState(null);
  const [restoringId, setRestoringId] = useState(null);
  const [deletedLoading, setDeletedLoading] = useState(false);
  const [restoreConfirmModal, setRestoreConfirmModal] = useState({ isOpen: false, product: null });
  const [productSearchValue, setProductSearchValue] = useState("");
  const [productSearchResult, setProductSearchResult] = useState(null);
  const [productSearchPage, setProductSearchPage] = useState(1);
  const [productSearching, setProductSearching] = useState(false);
  const [deletedProductIds, setDeletedProductIds] = useState(new Set());
  const productResultPanelRef = useMobileResultPanelScroll(
    showAddForm
      ? "add-product"
      : selectedProduct
        ? `product:${selectedProduct.productId}`
        : selectedDeletedProduct
          ? `deleted-product:${selectedDeletedProduct.productId}`
          : null
  );

  const fetchProducts = useCallback(async () => {
    try {
      const data = selectedProductCategoryId
        ? await getProductsByCategoryId(
            Number(selectedProductCategoryId),
            productPage,
            PRODUCT_PAGE_SIZE
          )
        : await getAllProducts(productPage, PRODUCT_PAGE_SIZE);
      setProducts(data.items);
      setProductTotalPages(data.totalPages);
      setProductTotalCount(data.totalCount);
    } catch { notify.error("Ürünler alınamadı."); }
  }, [productPage, selectedProductCategoryId]);

  const fetchDeletedProducts = useCallback(async () => {
    try {
      setDeletedLoading(true);
      const res = await getSoftDeletedProducts(deletedProductPage, PRODUCT_PAGE_SIZE);
      const items = res?.items ?? [];
      setDeletedProducts(items);
      setDeletedProductTotalPages(res?.totalPages ?? 1);
      setDeletedProductTotalCount(res?.totalCount ?? 0);
      setDeletedProductIds(prev => new Set([...prev, ...items.map(product => product.productId)]));
    } catch (err) {
      if (err?.response?.status === 404) setDeletedProducts([]);
      else notify.error("Pasif Ürünler alınamadı.");
    } finally {
      setDeletedLoading(false);
    }
  }, [deletedProductPage]);

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchProducts();
    });
    return () => { active = false; };
  }, [fetchProducts]);
  useEffect(() => {
    getAllCategories(1, 100)
      .then(data => setCategories(data.items ?? []))
      .catch(() => notify.error("Kategoriler alınamadı."));
  }, []);

  useEffect(() => {
    let active = true;

    Promise.resolve().then(() => {
      if (active && productView === "deleted") fetchDeletedProducts();
    });

    return () => { active = false; };
  }, [fetchDeletedProducts, productView]);

  function switchView(view) {
    setProductView(view);
    setSelectedProduct(null);
    setSelectedDeletedProduct(null);
    setShowAddForm(false);
    setSelectedProductCategoryId("");
    clearProductSearch();
    if (view === "deleted") fetchDeletedProducts();
  }

  async function handleProductSearch(event, pageToLoad = 1) {
    event?.preventDefault();
    const query = productSearchValue.trim();
    if (query.length < 2 || productSearching) return;

    try {
      setProductSearching(true);
      setSelectedProductCategoryId("");
      setProductPage(1);
      const data = await searchProductsByNameAsAdmin(query, pageToLoad, PRODUCT_PAGE_SIZE);
      try {
        const deletedData = await getSoftDeletedProducts(1, 10000);
        setDeletedProductIds(new Set((deletedData?.items ?? []).map(product => product.productId)));
      } catch {
        setDeletedProductIds(new Set());
      }
      setProductSearchResult(data);
      setProductSearchPage(pageToLoad);
      setSelectedProduct(null);
      setSelectedDeletedProduct(null);
      setShowAddForm(false);
    } catch (err) {
      setProductSearchResult(null);
      notify.error(err?.response?.data?.message || "Ürün araması yapılamadı.");
    } finally {
      setProductSearching(false);
    }
  }

  function clearProductSearch() {
    setProductSearchValue("");
    setProductSearchResult(null);
    setProductSearchPage(1);
    setSelectedProduct(null);
    setSelectedDeletedProduct(null);
  }

  function handleProductCategoryChange(categoryId) {
    setSelectedProductCategoryId(categoryId);
    setProductPage(1);
    setProductSearchValue("");
    setProductSearchResult(null);
    setProductSearchPage(1);
    setSelectedProduct(null);
    setSelectedDeletedProduct(null);
    setShowAddForm(false);
  }

  function isProductDeleted(product) {
    return product?.isDeleted === true || deletedProductIds.has(product?.productId);
  }

  function selectProduct(product) {
    if (isProductDeleted(product)) {
      setSelectedDeletedProduct(product);
      setSelectedProduct(null);
      setShowAddForm(false);
      return;
    }

    setSelectedProduct(product);
    setSelectedDeletedProduct(null);
    setEditForm({ productName: product.productName, categoryId: product.categoryId, description: product.description, price: product.price, stock: product.stock, imageUrl: product.imageUrl || "" });
    setImageUrlInput(product.imageUrl || "");
    setStockQty(1);
  }

  async function handleUpdateProduct() {
    if (!isEditFormValid) {
      notify.warning("Lütfen kırmızı işaretli Ürün alanlarını düzeltin.");
      return;
    }
    try {
      const updatedProduct = {
        productName: editForm.productName,
        categoryId: Number(editForm.categoryId),
        description: editForm.description,
        price: Number(editForm.price),
        stock: Number(editForm.stock),
        imageUrl: editForm.imageUrl || null
      };
      await updateProduct(selectedProduct.productId, updatedProduct);
      setSelectedProduct(previous => ({ ...previous, ...updatedProduct }));
      notify.success("Ürün güncellendi.");
      fetchProducts();
    } catch (err) { notify.error(err?.response?.data?.message || "Güncelleme başarısız."); }
  }

  async function handleUpdateProductImage() {
    if (!imageUrlInput.trim() || !isValidAbsoluteUrl(imageUrlInput)) {
      notify.warning("Lütfen geçerli bir görsel URL'si girin.");
      return;
    }
    try {
      setImageLoading(true);
      await updateProductImage(selectedProduct.productId, imageUrlInput.trim());
      notify.success("Ürün görseli güncellendi.");
      setSelectedProduct(prev => ({ ...prev, imageUrl: imageUrlInput.trim() }));
      fetchProducts();
    } catch (err) {
      notify.error(err?.response?.data?.message || err?.response?.data?.title || `Hata ${err?.response?.status}: Görsel güncellenemedi.`);
    } finally { setImageLoading(false); }
  }

  async function handleRemoveProductImage() {
    try {
      setImageLoading(true);
      await removeProductImage(selectedProduct.productId);
      notify.success("Ürün görseli kaldırıldı.");
      setImageUrlInput("");
      setSelectedProduct(prev => ({ ...prev, imageUrl: null }));
      fetchProducts();
    } catch (err) { notify.error(err?.response?.data?.message || "Görsel kaldırılamadı."); }
    finally { setImageLoading(false); }
  }

  function openDeleteProductModal() { setDeleteProductModal(true); }

  async function handleDeleteProductConfirmed() {
    setDeleteProductModal(false);
    try {
      await softDeleteProduct(selectedProduct.productId);
      notify.success(`"${selectedProduct.productName}" pasife alındı.`);
      setDeletedProductIds(prev => new Set([...prev, selectedProduct.productId]));
      setSelectedProduct(null);
      fetchProducts();
    } catch (err) { notify.error(err?.response?.data?.message || "Silme başarısız."); }
  }

  async function handleHardDeleteProductConfirmed() {
    setHardDeleteProductModal(false);
    const targetId = selectedProduct ? selectedProduct.productId : selectedDeletedProduct?.productId;
    const targetName = selectedProduct ? selectedProduct.productName : selectedDeletedProduct?.productName;
    try {
      await hardDeleteProduct(targetId);
      notify.success(`"${targetName}" kalıcı olarak silindi.`);
      setDeletedProductIds(prev => {
        const next = new Set(prev);
        next.delete(targetId);
        return next;
      });
      setSelectedProduct(null);
      setSelectedDeletedProduct(null);
      fetchProducts();
      fetchDeletedProducts();
    } catch (err) {
      const message = err?.response?.data?.message;
      if (message?.includes("associated with an order")) {
        notify.warning("Bu ürün bir siparişte kullanıldığı için kalıcı silinemez. Pasife alabilirsiniz.");
      } else {
        notify.error(message || "Kalıcı silme başarısız.");
      }
    }
  }

  async function handleAddStock() {
    try {
      await addStock(selectedProduct.productId, stockQty);
      notify.success(`${stockQty} adet stok eklendi.`);
      fetchProducts();
    } catch (err) { notify.error(err?.response?.data?.message || "Stok eklenemedi."); }
  }

  async function handleAddProduct() {
    if (!isAddFormValid) {
      notify.warning("Lütfen kırmızı işaretli Ürün alanlarını düzeltin.");
      return;
    }
    try {
      await addProduct({ product: { ...addForm, categoryId: Number(addForm.categoryId), imageUrl: addForm.imageUrl || null } });
      setAddForm(emptyProduct);
      setShowAddForm(false);
      fetchProducts();
    } catch (err) { notify.error(err?.response?.data?.message || "Ürün eklenemedi."); }
  }

  function openRestoreConfirm(product) {
    setRestoreConfirmModal({ isOpen: true, product });
  }

  async function handleRestoreProductConfirmed() {
    const product = restoreConfirmModal.product;
    setRestoreConfirmModal({ isOpen: false, product: null });
    try {
      setRestoringId(product.productId);
      await restoreProduct(product.productId);
      notify.success(`"${product.productName}" yeniden aktive edildi.`);
      setDeletedProductIds(prev => {
        const next = new Set(prev);
        next.delete(product.productId);
        return next;
      });
      setSelectedDeletedProduct(null);
      fetchDeletedProducts();
      fetchProducts();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Restore başarısız.");
    } finally {
      setRestoringId(null);
    }
  }

  const visibleProducts = productSearchResult
    ? productSearchResult.items ?? []
    : productView === "active"
      ? products
      : deletedProducts;

  // ─── RENDER ────────────────────────────────────────────────
  return (
    <div className="admin-split compact-admin-list products-admin-split">
      <ConfirmModal
        isOpen={deleteProductModal}
        title="Ürün Sil (Soft Delete)"
        message={`"${selectedProduct?.productName}" adlı Ürün silinecek. Veritabanından kalıcı olarak silinmez, yalnızca pasife alınır.`}
        confirmLabel="Evet, Pasife Al"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={handleDeleteProductConfirmed}
        onCancel={() => setDeleteProductModal(false)}
      />

      <ConfirmModal
        isOpen={hardDeleteProductModal}
        title="Ürün Kalıcı Sil"
        message={`"${selectedProduct?.productName || selectedDeletedProduct?.productName}" adlı Ürün veritabanından KALICI olarak silinecek. Bu işlem geri alınamaz! Devam etmek istiyor musunuz?`}
        confirmLabel="Evet, Kalıcı Olarak Sil"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={handleHardDeleteProductConfirmed}
        onCancel={() => setHardDeleteProductModal(false)}
      />

      <ConfirmModal
        isOpen={restoreConfirmModal.isOpen}
        title="Ürün Aktive Et"
        message={`"${restoreConfirmModal.product?.productName}" adlı ürün yeniden aktive edilecek ve kullanıcılara görünür hale gelecek. Onaylıyor musunuz?`}
        confirmLabel="Evet, Aktive Et"
        cancelLabel="Vazgeç"
        variant="success"
        onConfirm={handleRestoreProductConfirmed}
        onCancel={() => setRestoreConfirmModal({ isOpen: false, product: null })}
      />

      {/* Sol Panel */}
      <div className="admin-list-panel">
        {/* Aktif / Pasif Tab */}
        <div className="admin-user-tabs" style={{ marginBottom: 16 }}>
          <button
            className={productView === "active" ? "active" : ""}
            onClick={() => switchView("active")}
          >
            Aktif ({productTotalCount})
          </button>
          <button
            className={productView === "deleted" ? "active" : ""}
            onClick={() => switchView("deleted")}
          >
            Pasif {deletedProductTotalCount > 0 ? `(${deletedProductTotalCount})` : ""}
          </button>
        </div>

        {productView === "active" && (
          <div className="admin-section-header" style={{ marginBottom: 12 }}>
            <h3 style={{ margin: 0 }}>Ürünler</h3>
            <button
              className="btn-add-small"
              onClick={() => { setShowAddForm(true); setSelectedProduct(null); setSelectedDeletedProduct(null); }}
            >
              + Ürün Ekle
            </button>
          </div>
        )}

        {productView === "deleted" && (
          <div className="admin-section-header" style={{ marginBottom: 12 }}>
            <h3 style={{ margin: 0 }}>Pasif Ürünler</h3>
            <button className="btn-add-small" onClick={fetchDeletedProducts}>
              Yenile
            </button>
          </div>
        )}

        <form className="admin-user-search" onSubmit={handleProductSearch}>
          {productView === "active" && (
            <div className="admin-product-category-filter">
              <label htmlFor="admin-product-category-filter">Kategori</label>
              <ResponsiveSelect
                id="admin-product-category-filter"
                value={selectedProductCategoryId}
                onChange={handleProductCategoryChange}
                options={[
                  { value: "", label: "Tüm Kategoriler" },
                  ...categories.map(category => ({
                    value: category.categoryId,
                    label: normalizeCategoryName(category.categoryName),
                  })),
                ]}
                ariaLabel="Kategoriye göre filtrele"
              />
            </div>
          )}
          <div className="admin-search-row">
            <input
              type="text"
              value={productSearchValue}
              onChange={event => {
                setProductSearchValue(event.target.value);
                setProductSearchResult(null);
              }}
              placeholder="Ürün adıyla ara"
            />
            <button type="submit" disabled={productSearchValue.trim().length < 2 || productSearching}>
              {productSearching ? "..." : "Ara"}
            </button>
            {productSearchValue && (
              <button type="button" className="admin-search-clear" onClick={clearProductSearch} aria-label="Aramayı temizle">
                ×
              </button>
            )}
          </div>
          <ValidationHint state={getValidationState(productSearchValue, productSearchValue.trim().length >= 2)}>
            Ürün aramak için en az 2 karakter girin. Admin araması pasif ürünleri de kapsar.
          </ValidationHint>
        </form>

        <div className="admin-list">
          {productView === "active" || productSearchResult ? (
            visibleProducts.length === 0 ? (
              <p className="empty-text">
                {selectedProductCategoryId ? "Bu kategoride ürün bulunamadı." : "Ürün bulunamadı."}
              </p>
            ) : visibleProducts.map(product => (
              <div
                key={product.productId}
                className={`admin-list-item ${(selectedProduct?.productId === product.productId || selectedDeletedProduct?.productId === product.productId) ? "active" : ""}`}
                onClick={() => { selectProduct(product); setShowAddForm(false); }}
              >
                <div style={{ flex: 1, minWidth: 0 }}>
                  <div className="admin-list-name">{product.productName}</div>
                  <div className="admin-list-sub">{product.price.toLocaleString("tr-TR")}₺ × Stok: {product.stock}</div>
                </div>
                {isProductDeleted(product) && <span className="deleted-badge">Pasif</span>}
              </div>
            ))
          ) : deletedLoading ? (
            <p className="empty-text">Yükleniyor...</p>
          ) : visibleProducts.length === 0 ? (
            <p className="empty-text">Pasife alınmış Ürün yok.</p>
          ) : (
            visibleProducts.map(product => (
              <div
                key={product.productId}
                className={`admin-list-item ${selectedDeletedProduct?.productId === product.productId ? "active" : ""}`}
                onClick={() => setSelectedDeletedProduct(product)}
              >
                <div style={{ flex: 1, minWidth: 0 }}>
                  <div className="admin-list-name" style={{ color: "var(--text-muted)" }}>{product.productName}</div>
                  <div className="admin-list-sub">{product.price.toLocaleString("tr-TR")}₺ × Stok: {product.stock}</div>
                </div>
                <span className="deleted-badge">Pasif</span>
              </div>
            ))
          )}
        </div>
        {productSearchResult ? (
          <Pagination currentPage={productSearchPage} totalPages={productSearchResult.totalPages ?? 1} onPageChange={(nextPage) => handleProductSearch(null, nextPage)} />
        ) : productView === "active" ? (
          <Pagination currentPage={productPage} totalPages={productTotalPages} onPageChange={setProductPage} />
        ) : (
          <Pagination currentPage={deletedProductPage} totalPages={deletedProductTotalPages} onPageChange={setDeletedProductPage} />
        )}
      </div>

      {/* Sağ Panel */}
      {productView === "active" && !selectedDeletedProduct ? (
        showAddForm ? (
          <div ref={productResultPanelRef} className="admin-detail-panel">
            <div className="admin-detail-header">
              <h3>Yeni Ürün Ekle</h3>
              <button className="btn-secondary" onClick={() => setShowAddForm(false)}>İptal</button>
            </div>
            <div className="form-step">
              <div className="validation-field">
                <input
                  placeholder="Ürün Adı"
                  className={validationClass(addForm.productName, addValidation.productName)}
                  value={addForm.productName}
                  onChange={e => setAddForm({ ...addForm, productName: e.target.value })}
                />
                <ValidationHint state={getValidationState(addForm.productName, addValidation.productName)}>
                  Ürün adı zorunludur.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <ResponsiveSelect
                  value={addForm.categoryId}
                  onChange={categoryId => setAddForm({ ...addForm, categoryId })}
                  options={[
                    { value: "", label: "Kategori Seçin" },
                    ...categories.map(category => ({
                      value: category.categoryId,
                      label: normalizeCategoryName(category.categoryName),
                    })),
                  ]}
                  ariaLabel="Kategori seçin"
                  triggerClassName={validationClass(addForm.categoryId, addValidation.categoryId)}
                />
                <ValidationHint state={getValidationState(addForm.categoryId, addValidation.categoryId)}>
                  Bir kategori seçilmelidir.
                </ValidationHint>
              </div>
              <textarea
                className="product-description-input"
                rows={6}
                placeholder="Ürün açıklaması"
                value={addForm.description}
                onChange={e => setAddForm({ ...addForm, description: e.target.value })}
              />
              <div className="validation-field">
                <input
                  type="number"
                  min="0"
                  step="0.01"
                  placeholder="Fiyat (₺)"
                  className={validationClass(addForm.price, addValidation.price)}
                  value={addForm.price}
                  onChange={e => setAddForm({ ...addForm, price: e.target.value === "" ? "" : Number(e.target.value) })}
                />
                <ValidationHint state={getValidationState(addForm.price, addValidation.price)}>
                  Fiyat 0 veya daha büyük olmalıdır.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  type="number"
                  min="0"
                  step="1"
                  placeholder="Başlangıç Stoğu"
                  className={validationClass(addForm.stock, addValidation.stock)}
                  value={addForm.stock}
                  onChange={e => setAddForm({ ...addForm, stock: e.target.value === "" ? "" : Number(e.target.value) })}
                />
                <ValidationHint state={getValidationState(addForm.stock, addValidation.stock)}>
                  Stok negatif olamaz ve tam sayı olmalıdır.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  placeholder="Görsel URL (opsiyonel)"
                  className={validationClass(addForm.imageUrl, addValidation.imageUrl, true)}
                  value={addForm.imageUrl}
                  onChange={e => setAddForm({ ...addForm, imageUrl: e.target.value })}
                />
                <ValidationHint state={getValidationState(addForm.imageUrl, addValidation.imageUrl, true)}>
                  Opsiyonel; girilirse tam bir URL olmalıdır.
                </ValidationHint>
              </div>
              {addForm.imageUrl && (
                <div className="product-image-preview">
                  <img src={addForm.imageUrl} alt="Önizleme" onError={e => { e.target.style.display = 'none'; }} />
                </div>
              )}
              <div className="form-buttons">
                <button className="btn-secondary" onClick={() => { setShowAddForm(false); setAddForm(emptyProduct); }}>İptal</button>
                <button className="btn-primary" onClick={handleAddProduct} disabled={!isAddFormValid}>Kaydet</button>
              </div>
            </div>
          </div>
        ) : selectedProduct ? (
          <div ref={productResultPanelRef} className="admin-detail-panel">
            <div className="admin-detail-header">
              <h3>{selectedProduct.productName}</h3>
              <div style={{ display: "flex", gap: "8px" }}>
                <button className="btn-warning" onClick={openDeleteProductModal} style={{ padding: "6px 12px", fontSize: "0.85rem", whiteSpace: "nowrap" }}>
                  Pasife Al
                </button>
                <button className="btn-danger" onClick={() => setHardDeleteProductModal(true)} style={{ padding: "6px 12px", fontSize: "0.85rem", whiteSpace: "nowrap" }}>
                  Kalıcı Sil
                </button>
              </div>
            </div>

            {selectedProduct.imageUrl && (
              <div className="product-current-image">
                <img src={selectedProduct.imageUrl} alt={selectedProduct.productName} onError={e => { e.target.style.display = 'none'; }} />
              </div>
            )}

            <h4 className="admin-section-title">Ürün Bilgilerini Güncelle</h4>
            <div className="form-step">
              <div className="validation-field">
                <input
                  placeholder="Ürün Adı"
                  className={validationClass(editForm.productName, editValidation.productName)}
                  value={editForm.productName}
                  onChange={e => setEditForm({ ...editForm, productName: e.target.value })}
                />
                <ValidationHint state={getValidationState(editForm.productName, editValidation.productName)}>
                  Ürün adı zorunludur.
                </ValidationHint>
              </div>
              <ResponsiveSelect
                value={editForm.categoryId}
                onChange={categoryId => setEditForm({ ...editForm, categoryId })}
                options={[
                  { value: "", label: "Kategori Seçin" },
                  ...categories.map(category => ({
                    value: category.categoryId,
                    label: normalizeCategoryName(category.categoryName),
                  })),
                ]}
                ariaLabel="Kategori seçin"
                triggerClassName={validationClass(editForm.categoryId, editValidation.categoryId)}
              />
              <textarea
                className="product-description-input"
                rows={6}
                placeholder="Ürün açıklaması"
                value={editForm.description}
                onChange={e => setEditForm({ ...editForm, description: e.target.value })}
              />
              <div className="validation-field">
                <input
                  type="number"
                  min="0"
                  step="0.01"
                  placeholder="Fiyat (₺)"
                  className={validationClass(editForm.price, editValidation.price)}
                  value={editForm.price}
                  onChange={e => setEditForm({ ...editForm, price: e.target.value === "" ? "" : Number(e.target.value) })}
                />
                <ValidationHint state={getValidationState(editForm.price, editValidation.price)}>
                  Fiyat negatif olamaz.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  type="number"
                  min="0"
                  step="1"
                  inputMode="numeric"
                  placeholder="Stok"
                  className={validationClass(editForm.stock, editValidation.stock)}
                  value={editForm.stock}
                  onChange={e => setEditForm({
                    ...editForm,
                    stock: e.target.value === "" ? "" : Number(e.target.value)
                  })}
                />
                <ValidationHint state={getValidationState(editForm.stock, editValidation.stock)}>
                  Stok negatif olamaz ve tam sayı olmalıdır.
                </ValidationHint>
              </div>
              <div className="validation-field">
                <input
                  placeholder="Görsel URL (opsiyonel)"
                  className={validationClass(editForm.imageUrl, editValidation.imageUrl, true)}
                  value={editForm.imageUrl}
                  onChange={e => setEditForm({ ...editForm, imageUrl: e.target.value })}
                />
                <ValidationHint state={getValidationState(editForm.imageUrl, editValidation.imageUrl, true)}>
                  Opsiyonel; girilirse tam bir URL olmalıdır.
                </ValidationHint>
              </div>
              <button className="btn-primary" onClick={handleUpdateProduct} disabled={!isEditFormValid}>Güncelle</button>
            </div>

            <h4 className="admin-section-title">Ürün Görseli Yönet</h4>
            <div className="image-manage-section">
              <input
                placeholder="Görsel URL girin..."
                value={imageUrlInput}
                onChange={e => setImageUrlInput(e.target.value)}
                className="image-url-input"
              />
              {imageUrlInput && (
                <div className="product-image-preview">
                  <img src={imageUrlInput} alt="Önizleme" onError={e => { e.target.style.display = 'none'; }} />
                </div>
              )}
              <div className="image-manage-buttons">
                <button className="btn-primary" onClick={handleUpdateProductImage} disabled={imageLoading}>
                  {imageLoading ? "İşleniyor..." : "Görseli Güncelle"}
                </button>
                {selectedProduct.imageUrl && (
                  <button className="btn-danger" onClick={handleRemoveProductImage} disabled={imageLoading}>
                    {imageLoading ? "İşleniyor..." : "Görseli Kaldır"}
                  </button>
                )}
              </div>
            </div>

            <h4 className="admin-section-title">Stok Ekle</h4>
            <div className="stock-row">
              <input type="number" min={1} value={stockQty} onChange={e => setStockQty(Number(e.target.value))} className="stock-input" />
              <button className="btn-primary" onClick={handleAddStock}>Stok Ekle</button>
            </div>
          </div>
        ) : (
          <div className="admin-empty-detail">
            <p>Detaylarını görmek için bir Ürün seçin.</p>
          </div>
        )
      ) : (
        /* Pasif Ürün Sağ Panel */
        selectedDeletedProduct ? (
          <div ref={productResultPanelRef} className="admin-detail-panel">
            <div className="admin-detail-header">
              <div>
                <h3>{selectedDeletedProduct.productName}</h3>
                <span className="deleted-badge" style={{ marginTop: 4, display: "inline-block" }}>Pasif / Arşivlenmiş</span>
              </div>
              <div style={{ display: "flex", gap: "8px" }}>
                <button
                  className="btn-restore"
                  disabled={restoringId === selectedDeletedProduct.productId}
                  onClick={() => openRestoreConfirm(selectedDeletedProduct)}
                >
                  {restoringId === selectedDeletedProduct.productId ? "Aktive ediliyor..." : "Yeniden Aktive Et"}
                </button>
                <button
                  className="btn-danger"
                  disabled={restoringId === selectedDeletedProduct.productId}
                  onClick={() => setHardDeleteProductModal(true)}
                >
                  Kalıcı Sil
                </button>
              </div>
            </div>

            {selectedDeletedProduct.imageUrl && (
              <div className="product-current-image">
                <img
                  src={selectedDeletedProduct.imageUrl}
                  alt={selectedDeletedProduct.productName}
                  onError={e => { e.target.style.display = 'none'; }}
                />
              </div>
            )}

            <div className="deleted-product-info">
              <div className="deleted-info-row">
                <span className="deleted-info-label">Açıklama</span>
                <span>{selectedDeletedProduct.description || "-"}</span>
              </div>
              <div className="deleted-info-row">
                <span className="deleted-info-label">Fiyat</span>
                <span>{selectedDeletedProduct.price?.toLocaleString("tr-TR")}₺</span>
              </div>
              <div className="deleted-info-row">
                <span className="deleted-info-label">Son Stok</span>
                <span>{selectedDeletedProduct.stock} adet</span>
              </div>
            </div>

            <div className="deleted-restore-note">
              ⚠️ Bu ürün pasife alınmış ve kullanıcılara gösterilmiyor. &quot;Yeniden Aktive Et&quot; butonu ile listeye geri ekleyebilirsiniz.
            </div>
          </div>
        ) : (
          <div className="admin-empty-detail">
            <p>Detaylarını görmek için bir pasif Ürün seçin.</p>
          </div>
        )
      )}
    </div>
  );
}

// ─── ORDERS TAB ───────────────────────────────────────────────────────────────
const ORDERS_TAB_USER_PAGE_SIZE = 10;
const LAST_ORDERS_PAGE_SIZE = 5;
const SELECTED_USER_ORDERS_PAGE_SIZE = 5;

function OrdersTab() {
  const [users, setUsers] = useState([]);
  const [userPage, setUserPage] = useState(1);
  const [userTotalPages, setUserTotalPages] = useState(1);
  const [userTotalCount, setUserTotalCount] = useState(0);
  const [searchType, setSearchType] = useState("email");
  const [searchValue, setSearchValue] = useState("");
  const [searchResult, setSearchResult] = useState(null);
  const [searchPage, setSearchPage] = useState(1);
  const [searchingUser, setSearchingUser] = useState(false);
  const [orderSearchNotFound, setOrderSearchNotFound] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);
  const [orders, setOrders] = useState([]);
  const [selectedOrdersPage, setSelectedOrdersPage] = useState(1);
  const [selectedOrdersTotalPages, setSelectedOrdersTotalPages] = useState(1);
  const [lastOrders, setLastOrders] = useState([]);
  const [lastOrdersPage, setLastOrdersPage] = useState(1);
  const [lastOrdersTotalPages, setLastOrdersTotalPages] = useState(1);
  const [lastOrdersLoading, setLastOrdersLoading] = useState(false);
  const [showAddOrder, setShowAddOrder] = useState(false);
  const [orderForm, setOrderForm] = useState({ userId: 0, addressId: "", productId: 0, quantity: "" });
  const [products, setProducts] = useState([]);
  const [userAddresses, setUserAddresses] = useState([]);
  const [selectedStatuses, setSelectedStatuses] = useState({});
  const [updatingOrderId, setUpdatingOrderId] = useState(null);
  const orderResultPanelRef = useMobileResultPanelScroll(
    selectedUser
      ? `order-user:${getUserId(selectedUser) ?? getUserEmail(selectedUser)}`
      : orderSearchNotFound
        ? `order-user-not-found:${searchType}:${searchValue.trim()}`
        : null
  );
  const orderNameSearchListRef = useMobileResultPanelScroll(
    searchType === "name" && searchResult && !orderSearchNotFound
      ? `order-name-search:${searchValue.trim()}:${searchPage}`
      : null
  );
  const isSearchValid = searchType === "email"
    ? isValidEmail(searchValue)
    : searchType === "phone"
      ? isValidTurkishPhone(searchValue)
      : searchValue.trim().length >= 3;
  const searchValidationState = getValidationState(searchValue, isSearchValid);

  useEffect(() => {
    getAllProducts(1, 999).then(data => setProducts(data.items));
  }, []);

  const fetchOrdersTabUsers = useCallback(async () => {
    try {
      const data = await getAllUsers(userPage, ORDERS_TAB_USER_PAGE_SIZE);
      setUsers(data.items);
      setUserTotalPages(data.totalPages);
      setUserTotalCount(data.totalCount);
    } catch { notify.error("Kullanıcılar alınamadı."); }
  }, [userPage]);

  const fetchLastOrders = useCallback(async () => {
    try {
      setLastOrdersLoading(true);
      const data = await getLastOrders(lastOrdersPage, LAST_ORDERS_PAGE_SIZE);
      setLastOrders(data.items ?? []);
      setLastOrdersTotalPages(data.totalPages ?? 1);
    } catch {
      setLastOrders([]);
      notify.error("Son siparişler alınamadı.");
    } finally {
      setLastOrdersLoading(false);
    }
  }, [lastOrdersPage]);

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchOrdersTabUsers();
    });
    return () => { active = false; };
  }, [fetchOrdersTabUsers]);

  useEffect(() => {
    let active = true;
    Promise.resolve().then(() => {
      if (active) fetchLastOrders();
    });
    return () => { active = false; };
  }, [fetchLastOrders]);

  async function handleOrderUserSearch(event, pageToLoad = 1) {
    event?.preventDefault();
    if (!isSearchValid || searchingUser) return;

    try {
      setSearchingUser(true);
      setOrderSearchNotFound(false);
      const result = searchType === "email"
        ? await getUserByMail(searchValue.trim())
        : searchType === "phone"
          ? await getUserByPhoneNumber(searchValue.trim())
          : await searchUsersByName(searchValue.trim(), pageToLoad, ORDERS_TAB_USER_PAGE_SIZE);

      setSearchResult(result);
      setSearchPage(pageToLoad);
      const nameSearchItems = searchType === "name" ? result?.items ?? [] : [];
      const hasResult = searchType === "name" ? nameSearchItems.length > 0 : Boolean(result);
      setOrderSearchNotFound(!hasResult);
      if (hasResult) setSelectedUser(null);
      if (searchType !== "name" && hasResult) {
        await fetchOrders(result);
      }
    } catch (err) {
      setSearchResult(null);
      setSelectedUser(null);
      setOrderSearchNotFound(err?.response?.status === 404);
      if (err?.response?.status === 404) {
        notify.warning("Bu bilgilerle eşleşen kullanıcı bulunamadı.");
      } else {
        notify.error(err?.response?.data?.message || "Kullanıcı aranamadı.");
      }
    } finally {
      setSearchingUser(false);
    }
  }

  function clearOrderUserSearch() {
    setSearchValue("");
    setSearchResult(null);
    setSearchPage(1);
    setOrderSearchNotFound(false);
  }

  async function handleLastOrderClick(order) {
    const userId = order.userId ?? order.UserId ?? order.customerId ?? order.CustomerId;
    if (!userId) {
      notify.warning("Bu işlem için Get-Last-Orders response'una userId eklenmeli.");
      return;
    }

    try {
      const user = await getUserById(userId);
      await fetchOrders(user);
    } catch (err) {
      notify.error(err?.response?.data?.message || "Siparişin kullanıcısı getirilemedi.");
    }
  }

  async function fetchOrders(user, pageToLoad = 1) {
    let normalizedUser = normalizeUser(user);

    if (!getUserId(normalizedUser)) {
      try {
        const detail = await getUserDetailForSelection(user);
        normalizedUser = normalizeUser(user, detail);
      } catch (err) {
        notify.error(err?.response?.data?.message || "Kullanıcı detayı alınamadı.");
        return;
      }
    }

    const userId = getUserId(normalizedUser);
    setOrderSearchNotFound(false);
    setSelectedUser(normalizedUser);
    try {
      const res = await getOrdersByUserId(userId, pageToLoad, SELECTED_USER_ORDERS_PAGE_SIZE);
      setOrders(res?.items ?? []);
      setSelectedOrdersPage(pageToLoad);
      setSelectedOrdersTotalPages(res?.totalPages ?? 1);
      setSelectedStatuses({});
      setOrderForm({ userId, addressId: "", productId: 0, quantity: "" });
    } catch (err) {
      if (err?.response?.status === 404) setOrders([]);
      else notify.error("Siparişler alınamadı.");
      setSelectedOrdersPage(1);
      setSelectedOrdersTotalPages(1);
    }
    try {
      const addr = await getAddressesByUserId(userId);
      setUserAddresses(addr || []);
    } catch {
      setUserAddresses([]);
    }
  }

  function clearSelectedOrderUser() {
    setSelectedUser(null);
    setOrderSearchNotFound(false);
    setOrders([]);
    setSelectedOrdersPage(1);
    setSelectedOrdersTotalPages(1);
    setUserAddresses([]);
    setSelectedStatuses({});
    setShowAddOrder(false);
    setOrderForm({ userId: 0, addressId: "", productId: 0, quantity: "" });
  }

  async function handleAddOrder() {
    try {
      await addOrder({ ...orderForm, userId: getUserId(selectedUser) });
      fetchOrders(selectedUser, 1);
      setShowAddOrder(false);
    } catch (err) { notify.error(err?.response?.data?.message || "Sipariş eklenemedi."); }
  }

  async function handleStatusChange(order) {
    const allowedStatuses = STATUS_TRANSITIONS[order.orderStatus] ?? [];
    const status = selectedStatuses[order.orderId] ?? allowedStatuses[0];
    if (!status) return;

    try {
      setUpdatingOrderId(order.orderId);
      await updateOrderStatus(order.orderId, status);
      notify.success(`Sipariş durumu "${STATUS_MAP[status]}" olarak güncellendi.`);
      await fetchOrders(selectedUser, selectedOrdersPage);
    } catch (err) { notify.error(err?.response?.data?.message || "İşlem başarısız."); }
    finally { setUpdatingOrderId(null); }
  }

  const visibleOrderUsers = searchType === "name" && searchResult
    ? searchResult.items ?? []
    : searchResult
      ? [searchResult]
      : users;

  return (
    <div className="admin-split compact-admin-list orders-admin-split">
      <div className="admin-list-panel">
        <div className="admin-section-header" style={{ marginBottom: 12 }}>
          <h3 style={{ margin: 0 }}>Kullanıcılar ({userTotalCount})</h3>
        </div>
        <form className="admin-user-search" onSubmit={handleOrderUserSearch}>
          <div className="admin-search-tabs">
            <button
              type="button"
              className={searchType === "email" ? "active" : ""}
              onClick={() => {
                setSearchType("email");
                setSearchValue("");
                setSearchResult(null);
                setOrderSearchNotFound(false);
              }}
            >
              E-posta
            </button>
            <button
              type="button"
              className={searchType === "phone" ? "active" : ""}
              onClick={() => {
                setSearchType("phone");
                setSearchValue("");
                setSearchResult(null);
                setOrderSearchNotFound(false);
              }}
            >
              Telefon
            </button>
            <button
              type="button"
              className={searchType === "name" ? "active" : ""}
              onClick={() => {
                setSearchType("name");
                setSearchValue("");
                setSearchResult(null);
                setOrderSearchNotFound(false);
              }}
            >
              İsim
            </button>
          </div>
          <div className="admin-search-row">
            <input
              type={searchType === "email" ? "email" : searchType === "phone" ? "tel" : "text"}
              className={searchValidationState === "neutral" ? "" : `validation-input-${searchValidationState}`}
              placeholder={searchType === "email" ? "kullanici@site.com" : searchType === "phone" ? "05xxxxxxxxx" : "En az 3 harf girin"}
              value={searchValue}
              onChange={event => {
                setSearchValue(event.target.value);
                setSearchResult(null);
                setOrderSearchNotFound(false);
              }}
            />
            <button type="submit" disabled={!isSearchValid || searchingUser}>
              {searchingUser ? "..." : "Ara"}
            </button>
            {searchValue && (
              <button type="button" className="admin-search-clear" onClick={clearOrderUserSearch} aria-label="Aramayı temizle">×</button>
            )}
          </div>
          <ValidationHint state={searchValidationState}>
            {searchType === "email"
              ? "Geçerli bir e-posta adresi girin."
              : searchType === "phone"
                ? "05xxxxxxxxx, 5xxxxxxxxx veya +905xxxxxxxxx formatında girin."
                : "İsim araması için en az 3 karakter girin."}
          </ValidationHint>
        </form>
        <div ref={orderNameSearchListRef} className="admin-list">
          {visibleOrderUsers.map(user => (
            <div
              key={getUserId(user) ?? getUserEmail(user)}
              className={`admin-list-item ${getUserId(selectedUser) === getUserId(user) ? "active" : ""}`}
              onClick={() => fetchOrders(user)}
            >
              <div className="admin-list-avatar">{user.userName?.charAt(0).toUpperCase()}</div>
              <div>
                <div className="admin-list-name">{user.userName}</div>
                <div className="admin-list-sub">{user.eMail}</div>
              </div>
            </div>
          ))}
        </div>
        {searchType === "name" && searchResult ? (
          <Pagination
            currentPage={searchPage}
            totalPages={searchResult.totalPages ?? 1}
            onPageChange={(nextPage) => handleOrderUserSearch(null, nextPage)}
          />
        ) : !searchResult && (
          <Pagination currentPage={userPage} totalPages={userTotalPages} onPageChange={setUserPage} />
        )}
      </div>

      {selectedUser ? (
        <div ref={orderResultPanelRef} className="admin-detail-panel">
          <div className="admin-detail-header">
            <h3>{selectedUser.userName} - Siparişleri</h3>
            <div className="admin-detail-actions">
              <button className="btn-success" onClick={() => setShowAddOrder(!showAddOrder)}>+ Sipariş Ekle</button>
              <button
                type="button"
                className="btn-close-detail"
                onClick={clearSelectedOrderUser}
                aria-label="Son siparişlere dön"
                title="Son siparişlere dön"
              >
                ×
              </button>
            </div>
          </div>

          {showAddOrder && (
            <div className="form-step admin-form-box">
              <ResponsiveSelect
                value={orderForm.productId}
                onChange={productId => setOrderForm({ ...orderForm, productId: Number(productId) })}
                options={[
                  { value: 0, label: "Ürün Seçin" },
                  ...products.map(product => ({ value: product.productId, label: product.productName })),
                ]}
                ariaLabel="Ürün seçin"
              />
              <ResponsiveSelect
                value={orderForm.addressId}
                onChange={addressId => setOrderForm({ ...orderForm, addressId: Number(addressId) })}
                options={[
                  { value: "", label: "Adres Seçin" },
                  ...userAddresses.map(address => ({
                    value: getAddressId(address),
                    label: `${address.district}, ${address.city} - ${ADDRESS_TYPES[address.addressType]}`,
                  })),
                ]}
                ariaLabel="Adres seçin"
              />
              <input type="number" min={1} placeholder="Adet" value={orderForm.quantity}
                onChange={e => setOrderForm({ ...orderForm, quantity: e.target.value === "" ? "" : Number(e.target.value) })} />
              <div className="form-buttons">
                <button className="btn-secondary" onClick={() => setShowAddOrder(false)}>İptal</button>
                <button className="btn-primary" onClick={handleAddOrder}>Kaydet</button>
              </div>
            </div>
          )}

          {orders.length === 0 ? (
            <p className="empty-text">Bu kullanıcıya ait sipariş bulunmuyor.</p>
          ) : (
            <>
              {orders.map(order => {
                const allowedStatuses = STATUS_TRANSITIONS[order.orderStatus] ?? [];
                const selectedStatus = selectedStatuses[order.orderId] ?? allowedStatuses[0] ?? "";
                const isTerminal = allowedStatuses.length === 0;

                return (
                <div className={`admin-order-card ${isTerminal ? "terminal-order" : ""}`} key={order.orderId}>
                  <div>
                    {(order.items ?? []).map(item => (
                      <div key={item.orderItemId} className="order-product">
                        {item.productName} <span className="order-meta">× {item.quantity}</span>
                      </div>
                    ))}
                    <div className="order-meta">{new Date(order.createdAt).toLocaleDateString("tr-TR")}</div>
                    <div className="order-meta">{order.shippingAddress}</div>
                  </div>
                  <div style={{ textAlign: "right" }}>
                    <div className="order-price">{order.totalPrice.toLocaleString("tr-TR")}₺</div>
                    <span className={`order-status-badge ${STATUS_COLOR_MAP[order.orderStatus]}`}>
                      {STATUS_MAP[order.orderStatus]}
                    </span>
                    {isTerminal ? (
                      <div className="order-status-locked">
                        Bu sipariş tamamlanmış durumda ve değiştirilemez.
                      </div>
                    ) : (
                      <div className="order-status-editor">
                        <label htmlFor={`status-${order.orderId}`}>Yeni durum</label>
                        <div className="order-status-editor-row">
                          <ResponsiveSelect
                            id={`status-${order.orderId}`}
                            value={selectedStatus}
                            disabled={updatingOrderId === order.orderId}
                            onChange={status => setSelectedStatuses(current => ({
                              ...current,
                              [order.orderId]: status,
                            }))}
                            options={allowedStatuses.map(status => ({
                              value: status,
                              label: getStatusTransitionLabel(status),
                            }))}
                            ariaLabel="Yeni sipariş durumu"
                          />
                          <button
                            type="button"
                            className={`btn-action ${selectedStatus === "Canceled" ? "danger" : ""}`}
                            disabled={updatingOrderId === order.orderId}
                            onClick={() => handleStatusChange(order)}
                          >
                            {updatingOrderId === order.orderId ? "Güncelleniyor..." : "Durumu Güncelle"}
                          </button>
                        </div>
                      </div>
                    )}
                  </div>
                </div>
                );
              })}
              <Pagination
                currentPage={selectedOrdersPage}
                totalPages={selectedOrdersTotalPages}
                onPageChange={(nextPage) => fetchOrders(selectedUser, nextPage)}
              />
            </>
          )}
        </div>
      ) : orderSearchNotFound ? (
        <div ref={orderResultPanelRef} className="admin-detail-panel admin-last-orders-panel">
          <div className="admin-empty-state">
            <div className="admin-empty-state-icon">?</div>
            <h4>Kullanıcı bulunamadı</h4>
            <p>Arama kriterleriyle eşleşen bir kullanıcı bulunamadı.</p>
          </div>
        </div>
      ) : (
        <div className="admin-detail-panel admin-last-orders-panel">
          <div className="admin-section-title-row">
            <div>
              <span className="admin-section-kicker">Genel Bakış</span>
              <h3>Son Siparişler</h3>
            </div>
            <button className="btn-secondary btn-sm" onClick={fetchLastOrders} disabled={lastOrdersLoading}>
              {lastOrdersLoading ? "Yükleniyor..." : "Yenile"}
            </button>
          </div>

          {lastOrdersLoading ? (
            <p className="empty-text">Son siparişler yükleniyor...</p>
          ) : lastOrders.length === 0 ? (
            <div className="admin-empty-state">
              <div className="admin-empty-state-icon">Fatura</div>
              <h4>Henüz sipariş bulunmuyor</h4>
              <p>Sisteme yeni sipariş düştüğünde burada listelenecek.</p>
            </div>
          ) : (
            <>
              <div className="admin-orders">
                {lastOrders.map(order => (
                  <div
                    className="admin-order-card last-order-card"
                    key={order.orderId}
                    role="button"
                    tabIndex={0}
                    onClick={() => handleLastOrderClick(order)}
                    onKeyDown={event => {
                      if (event.key === "Enter" || event.key === " ") handleLastOrderClick(order);
                    }}
                  >
                    <div className="last-order-main">
                      <div className="last-order-topline">
                        <span className="last-order-id">#{order.orderId}</span>
                        <span className="last-order-user">{order.userName}</span>
                      </div>
                      {(order.items ?? []).slice(0, 2).map(item => (
                        <div key={item.orderItemId} className="order-product">
                          {item.productName} <span className="order-meta">× {item.quantity}</span>
                        </div>
                      ))}
                      {(order.items?.length ?? 0) > 2 && (
                        <div className="order-meta">+{order.items.length - 2} Ürün daha</div>
                      )}
                      <div className="order-meta">{new Date(order.createdAt).toLocaleString("tr-TR")}</div>
                      <div className="order-meta">{order.shippingAddress}</div>
                    </div>
                    <div className="last-order-summary">
                      <div className="order-price">{order.totalPrice.toLocaleString("tr-TR")}₺</div>
                      <span className={`order-status-badge ${STATUS_COLOR_MAP[order.orderStatus]}`}>
                        {STATUS_MAP[order.orderStatus] ?? order.orderStatus}
                      </span>
                    </div>
                  </div>
                ))}
              </div>
              <Pagination
                currentPage={lastOrdersPage}
                totalPages={lastOrdersTotalPages}
                onPageChange={setLastOrdersPage}
              />
            </>
          )}
        </div>
      )}
    </div>
  );
}

// ─── MAIN ─────────────────────────────────────────────────────────────────────
const CATEGORY_PAGE_SIZE = 10;

function CategoriesTab() {
  const [categories, setCategories] = useState([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [totalCount, setTotalCount] = useState(0);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [categoryName, setCategoryName] = useState("");
  const [newCategoryName, setNewCategoryName] = useState("");
  const [showAddForm, setShowAddForm] = useState(false);
  const [deleteModalOpen, setDeleteModalOpen] = useState(false);
  const categoryResultPanelRef = useMobileResultPanelScroll(
    showAddForm
      ? "add-category"
      : selectedCategory
        ? `category:${selectedCategory.categoryId}`
        : null
  );

  useEffect(() => {
    let active = true;

    getAllCategories(page, CATEGORY_PAGE_SIZE)
      .then(data => {
        if (!active) return;
        setCategories(data.items ?? []);
        setTotalPages(data.totalPages ?? 1);
        setTotalCount(data.totalCount ?? 0);
      })
      .catch(() => {
        if (active) notify.error("Kategoriler alınamadı.");
      });

    return () => { active = false; };
  }, [page]);

  async function fetchCategories() {
    try {
      const data = await getAllCategories(page, CATEGORY_PAGE_SIZE);
      setCategories(data.items ?? []);
      setTotalPages(data.totalPages ?? 1);
      setTotalCount(data.totalCount ?? 0);
    } catch {
      notify.error("Kategoriler alınamadı.");
    }
  }

  function selectCategory(category) {
    setSelectedCategory(category);
    setCategoryName(normalizeCategoryName(category.categoryName));
    setShowAddForm(false);
  }

  async function handleAddCategory() {
    const trimmedName = newCategoryName.trim();
    if (!trimmedName) {
      notify.warning("Kategori adı boş bırakılamaz.");
      return;
    }

    try {
      await addCategory(trimmedName);
      notify.success("Kategori eklendi.");
      setNewCategoryName("");
      setShowAddForm(false);
      await fetchCategories();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Kategori eklenemedi.");
    }
  }

  async function handleUpdateCategory() {
    const trimmedName = categoryName.trim();
    if (!trimmedName) {
      notify.warning("Kategori adı boş bırakılamaz.");
      return;
    }

    try {
      const updated = await updateCategory(selectedCategory.categoryId, trimmedName);
      notify.success("Kategori güncellendi.");
      setSelectedCategory(updated);
      await fetchCategories();
    } catch (err) {
      notify.error(err?.response?.data?.message || "Kategori güncellenemedi.");
    }
  }

  async function handleDeleteCategory() {
    setDeleteModalOpen(false);
    try {
      await deleteCategory(selectedCategory.categoryId);
      notify.success(`"${normalizeCategoryName(selectedCategory.categoryName)}" kategorisi silindi.`);
      setSelectedCategory(null);
      setCategoryName("");
      await fetchCategories();
    } catch (err) {
      const message = err?.response?.data?.message;
      if (err?.response?.status === 400 && message === "Category cannot be deleted because it contains products.") {
        notify.warning("Bu kategoriye ait ürünler bulunduğu için kategori silinemez. Önce kategorideki ürünleri kaldırın veya başka bir kategoriye taşıyın.");
      } else {
        notify.error(message || "Kategori silinemedi.");
      }
    }
  }

  return (
    <div className="admin-split compact-admin-list categories-admin-split">
      <ConfirmModal
        isOpen={deleteModalOpen}
        title="Kategoriyi Sil"
        message={`"${selectedCategory?.categoryName}" kategorisini silmek istediğinize emin misiniz?`}
        confirmLabel="Evet, Sil"
        cancelLabel="Vazgeç"
        variant="danger"
        onConfirm={handleDeleteCategory}
        onCancel={() => setDeleteModalOpen(false)}
      />

      <div className="admin-list-panel">
        <div className="admin-section-header" style={{ marginBottom: 12 }}>
          <h3 style={{ margin: 0 }}>Kategoriler ({totalCount})</h3>
          <button
            className="btn-add-small"
            onClick={() => { setShowAddForm(true); setSelectedCategory(null); }}
          >
            + Kategori Ekle
          </button>
        </div>

        <div className="admin-list">
          {categories.length === 0 ? (
            <p className="empty-text">Kategori bulunamadı.</p>
          ) : categories.map(category => (
            <div
              key={category.categoryId}
              className={`admin-list-item ${selectedCategory?.categoryId === category.categoryId ? "active" : ""}`}
              onClick={() => selectCategory(category)}
            >
              <div className="admin-list-avatar">{normalizeCategoryName(category.categoryName)?.charAt(0).toUpperCase()}</div>
              <div>
                <div className="admin-list-name">{normalizeCategoryName(category.categoryName)}</div>
                <div className="admin-list-sub">Kategori #{category.categoryId}</div>
              </div>
            </div>
          ))}
        </div>

        <Pagination currentPage={page} totalPages={totalPages} onPageChange={setPage} />
      </div>

      {showAddForm ? (
        <div ref={categoryResultPanelRef} className="admin-detail-panel">
          <div className="admin-detail-header">
            <h3>Yeni Kategori Ekle</h3>
          </div>
          <div className="form-step">
            <input
              placeholder="Kategori Adı"
              value={newCategoryName}
              onChange={event => setNewCategoryName(event.target.value)}
            />
            <div className="form-buttons">
              <button className="btn-secondary" onClick={() => setShowAddForm(false)}>İptal</button>
              <button className="btn-primary" onClick={handleAddCategory} disabled={!newCategoryName.trim()}>Kaydet</button>
            </div>
          </div>
        </div>
      ) : selectedCategory ? (
        <div ref={categoryResultPanelRef} className="admin-detail-panel">
          <div className="admin-detail-header">
            <div>
              <h3>{normalizeCategoryName(selectedCategory.categoryName)}</h3>
              <p>Kategori #{selectedCategory.categoryId}</p>
            </div>
            <div className="admin-detail-actions">
              <button className="btn-danger" onClick={() => setDeleteModalOpen(true)}>Sil</button>
            </div>
          </div>
          <div className="form-step">
            <input
              placeholder="Kategori Adı"
              value={categoryName}
              onChange={event => setCategoryName(event.target.value)}
            />
            <button className="btn-primary" onClick={handleUpdateCategory} disabled={!categoryName.trim()}>Güncelle</button>
          </div>
        </div>
      ) : (
        <div className="admin-empty-detail">
          <p>Düzenlemek için bir kategori seçin veya yeni kategori ekleyin.</p>
        </div>
      )}
    </div>
  );
}

function AdminPage() {
  const [activeTab, setActiveTab] = useState("users");

  return (
    <div className="admin-page">
      <aside className="admin-sidebar">
        <h2 className="admin-sidebar-title">Admin Paneli</h2>
        <nav className="admin-nav">
          <button className={activeTab === "users" ? "active" : ""} onClick={() => setActiveTab("users")}>Kullanıcılar</button>
          <button className={activeTab === "products" ? "active" : ""} onClick={() => setActiveTab("products")}>Ürünler</button>
          <button className={activeTab === "categories" ? "active" : ""} onClick={() => setActiveTab("categories")}>Kategoriler</button>
          <button className={activeTab === "orders" ? "active" : ""} onClick={() => setActiveTab("orders")}>Siparişler</button>
        </nav>
      </aside>

      <main className="admin-main">
        {activeTab === "users" && <UsersTab />}
        {activeTab === "products" && <ProductsTab />}
        {activeTab === "categories" && <CategoriesTab />}
        {activeTab === "orders" && <OrdersTab />}
      </main>
    </div>
  );
}

export default AdminPage;
