import { useState } from "react";
import "./RatingStars.css";

function RatingStars({ rating = 0, interactive = false, onRate, size = "md" }) {
  const [hovered, setHovered] = useState(0);
  const activeRating = interactive ? hovered || rating : rating;

  return (
    <div className={`rating-stars rating-stars-${size}`}>
      {[1, 2, 3, 4, 5].map((star) => (
        <span
          key={star}
          className={`rating-star ${interactive ? "interactive" : ""} ${star <= activeRating ? "filled" : ""}`}
          onMouseEnter={() => interactive && setHovered(star)}
          onMouseLeave={() => interactive && setHovered(0)}
          onClick={() => interactive && onRate && onRate(star)}
        >
          ★
        </span>
      ))}
    </div>
  );
}

export default RatingStars;
