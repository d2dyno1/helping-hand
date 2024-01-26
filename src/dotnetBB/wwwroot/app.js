document.querySelectorAll(".js-no-href").forEach(a => {
    a.addEventListener("click", e => {
        e.preventDefault();
        return false;
    });
});