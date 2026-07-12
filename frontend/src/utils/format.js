export function formatPrice(price) {
  const numericPrice = Number(price);

  if (!Number.isFinite(numericPrice)) return price;

  return numericPrice.toLocaleString("tr-TR", {
    minimumFractionDigits: 0,
    maximumFractionDigits: 2,
  });
}
