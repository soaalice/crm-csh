// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showLoadingState(buttonId, spinnerId) {
    document.getElementById(spinnerId).style.display = "inline-block";
    document.getElementById(buttonId).disabled = true;
    document.getElementById(buttonId).innerHTML = '<span id="' + spinnerId + '" class="spinner"></span> Chargement...';
}
