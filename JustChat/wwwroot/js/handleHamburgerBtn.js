let handleHamburgerBtn = function () {
    let hamburgerBtn = document.querySelector(".hamburger");
    let navItems = document.querySelectorAll(".nav-item");
    let nav = document.querySelector(".nav");
    hamburgerBtn.addEventListener('click', function () {
        nav.classList.toggle('nav-small');
        navItems.forEach(function (item) {
            item.classList.toggle('collapse');
        })
    });
}