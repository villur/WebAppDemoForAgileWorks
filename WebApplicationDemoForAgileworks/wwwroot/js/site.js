// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function invalidAlert() {
    var description = document.querySelector('[name=description]');
    if (description.value == '') {
        description.className += ' is-invalid';
    }
    else {
        document.querySelector('[name=ticketForm]').submit();
    }
    
}
function clearAlert() {
    var description = document.querySelector('[name=description]');
    description.classList.remove('is-invalid');
}