let autocompleteInitialization = function (options) {
    document.addEventListener('DOMContentLoaded', function () {
        let elems = document.querySelectorAll('.autocomplete');
        M.Autocomplete.init(elems, options);
    });
}