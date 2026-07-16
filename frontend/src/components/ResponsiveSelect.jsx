import { useEffect, useLayoutEffect, useRef, useState } from "react";
import { createPortal } from "react-dom";
import { FaCheck, FaChevronDown } from "react-icons/fa";
import "./ResponsiveSelect.css";

function ResponsiveSelect({
  value,
  onChange,
  options,
  ariaLabel,
  disabled = false,
  className = "",
  triggerClassName = "",
  id,
}) {
  const [isOpen, setIsOpen] = useState(false);
  const [menuPosition, setMenuPosition] = useState(null);
  const rootRef = useRef(null);
  const triggerRef = useRef(null);
  const menuRef = useRef(null);
  const selectedOption = options.find(option => String(option.value) === String(value)) ?? options[0];

  useLayoutEffect(() => {
    if (!isOpen || !triggerRef.current) return;

    const rect = triggerRef.current.getBoundingClientRect();
    const viewportPadding = 8;
    const menuGap = 6;
    const spaceBelow = window.innerHeight - rect.bottom - viewportPadding;
    const spaceAbove = rect.top - viewportPadding;
    const openUpward = spaceBelow < 220 && spaceAbove > spaceBelow;
    const availableHeight = Math.max(120, (openUpward ? spaceAbove : spaceBelow) - menuGap);
    const width = Math.min(rect.width, window.innerWidth - (viewportPadding * 2));
    const left = Math.min(Math.max(viewportPadding, rect.left), window.innerWidth - viewportPadding - width);

    setMenuPosition({
      left,
      width,
      maxHeight: Math.min(260, availableHeight),
      ...(openUpward
        ? { bottom: window.innerHeight - rect.top + menuGap }
        : { top: rect.bottom + menuGap }),
    });
  }, [isOpen, options.length]);

  useEffect(() => {
    if (!isOpen) return undefined;

    const handleOutsideClick = event => {
      if (!rootRef.current?.contains(event.target) && !menuRef.current?.contains(event.target)) {
        setIsOpen(false);
      }
    };
    const closeMenu = () => setIsOpen(false);
    const handleScroll = event => {
      if (menuRef.current?.contains(event.target)) return;
      closeMenu();
    };

    document.addEventListener("pointerdown", handleOutsideClick);
    window.addEventListener("resize", closeMenu);
    window.addEventListener("scroll", handleScroll, true);

    return () => {
      document.removeEventListener("pointerdown", handleOutsideClick);
      window.removeEventListener("resize", closeMenu);
      window.removeEventListener("scroll", handleScroll, true);
    };
  }, [isOpen]);

  const selectOption = option => {
    onChange(option.value);
    setIsOpen(false);
    triggerRef.current?.focus();
  };

  const handleKeyDown = event => {
    if (event.key === "Escape") {
      setIsOpen(false);
      return;
    }

    if (event.key === "Enter" || event.key === " " || event.key === "ArrowDown") {
      event.preventDefault();
      setIsOpen(current => !current);
    }
  };

  return (
    <div ref={rootRef} className={`responsive-select ${className}`.trim()}>
      <button
        ref={triggerRef}
        id={id}
        type="button"
        className={`responsive-select-trigger ${triggerClassName}`.trim()}
        aria-label={ariaLabel}
        aria-haspopup="listbox"
        aria-expanded={isOpen}
        disabled={disabled}
        onClick={() => setIsOpen(current => !current)}
        onKeyDown={handleKeyDown}
      >
        <span>{selectedOption?.label ?? ""}</span>
        <FaChevronDown aria-hidden="true" />
      </button>

      {isOpen && menuPosition && createPortal(
        <div ref={menuRef} className="responsive-select-menu" role="listbox" aria-label={ariaLabel} style={menuPosition}>
          {options.map(option => {
            const isSelected = String(option.value) === String(value);
            return (
              <button
                key={String(option.value)}
                type="button"
                role="option"
                aria-selected={isSelected}
                className={isSelected ? "selected" : ""}
                onClick={() => selectOption(option)}
              >
                <span>{option.label}</span>
                {isSelected && <FaCheck aria-hidden="true" />}
              </button>
            );
          })}
        </div>,
        document.body
      )}
    </div>
  );
}

export default ResponsiveSelect;
