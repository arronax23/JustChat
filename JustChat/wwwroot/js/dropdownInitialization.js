let dropdownInitialization = function() {
    document.addEventListener('DOMContentLoaded', function () {
        let elems = document.querySelectorAll('.dropdown-trigger');
        let options = {
            coverTrigger: false,
            hover: true
        };
        M.Dropdown.init(elems, options);
    });
}