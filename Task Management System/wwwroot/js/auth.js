const KEY = "currentUser";

/* cookie helpers */
function setCookie(name, val) {
    const exp = new Date(Date.now() + 2 * 864e5).toUTCString();   // 2 days
    document.cookie = `${name}=${encodeURIComponent(val)}; expires=${exp}; path=/; SameSite=Lax`;
}
function clearCookie(name) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
}

function getUser() {
    return localStorage.getItem(KEY);
}

/* login / logout */
function login(u) { localStorage.setItem(KEY, u); setCookie(KEY, u); location.reload(); }
function logout() { localStorage.removeItem(KEY); clearCookie(KEY); location.reload(); }

/* navbar UI + badge */
function renderAuthUI() {
    const btn = document.getElementById("userBtn");
    const badge = document.getElementById("notifBadge");
    const user = getUser();

    if (!btn) return;
    if (user) {
        btn.textContent = `Logout (${user})`;
        btn.onclick = logout;
        btn.classList.remove("dropdown-toggle");
        btn.removeAttribute("data-bs-toggle");
    } else {
        btn.textContent = "Login";
        btn.classList.add("dropdown-toggle");
        btn.setAttribute("data-bs-toggle", "dropdown");
        btn.onclick = null;
    }

    // Load pending‑count from localStorage (set by SignalR or page render)
    badge.textContent = localStorage.getItem("pendingCount") || "";
    badge.classList.toggle("d-none", badge.textContent === "");
}

document.addEventListener("DOMContentLoaded", () => {
    renderAuthUI();

    // Fill hidden CurrentUser field on every form
    const u = getUser() ?? "";
    document.querySelectorAll('input[name="CurrentUser"]').forEach(i => i.value = u);
});
