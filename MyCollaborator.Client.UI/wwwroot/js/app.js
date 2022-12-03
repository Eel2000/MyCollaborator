$(function () {
    $(".btn-toggle-menu").click(function () {
        $("#wrapper").toggleClass("toggled");
    });
})

function SaveToLocalStorage(key, data) {
    let jsonData = JSON.stringify(data)
    window.localStorage.setItem(key, jsonData)
    return true;
}

function clearLocalStorage() {
    window.localStorage.clear();
}

function getSavedItem(key) {
    return window.localStorage.getItem(key);
}