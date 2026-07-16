import "./Pagination.css";

function buildPages(currentPage, totalPages) {
  const delta = 2;
  const pages = [];

  for (let i = 1; i <= totalPages; i++) {
    if (
      i === 1 ||
      i === totalPages ||
      (i >= currentPage - delta && i <= currentPage + delta)
    ) {
      pages.push(i);
    }
  }

  const withEllipsis = [];
  let prev = 0;
  for (const page of pages) {
    if (page - prev > 1) withEllipsis.push("ellipsis-" + page);
    withEllipsis.push(page);
    prev = page;
  }
  return withEllipsis;
}

function Pagination({ currentPage, totalPages, onPageChange, scrollToTopOnMobile = false }) {
  if (!totalPages || totalPages <= 1) return null;

  const items = buildPages(currentPage, totalPages);

  function handlePageChange(nextPage) {
    if (nextPage === currentPage) return;

    onPageChange(nextPage);
    if (scrollToTopOnMobile && window.matchMedia("(max-width: 768px)").matches) {
      window.scrollTo({ top: 0, left: 0, behavior: "auto" });
    }
  }

  return (
    <nav className="pagination" aria-label="Sayfalama">
      <button
        className="pagination-btn pagination-arrow"
        onClick={() => handlePageChange(currentPage - 1)}
        disabled={currentPage === 1}
        aria-label="Önceki sayfa"
      >
        ‹
      </button>

      {items.map((item) =>
        typeof item === "string" ? (
          <span key={item} className="pagination-ellipsis">…</span>
        ) : (
          <button
            key={item}
            className={`pagination-btn ${item === currentPage ? "pagination-active" : ""}`}
            onClick={() => handlePageChange(item)}
            aria-current={item === currentPage ? "page" : undefined}
          >
            {item}
          </button>
        )
      )}

      <button
        className="pagination-btn pagination-arrow"
        onClick={() => handlePageChange(currentPage + 1)}
        disabled={currentPage === totalPages}
        aria-label="Sonraki sayfa"
      >
        ›
      </button>
    </nav>
  );
}

export default Pagination;
