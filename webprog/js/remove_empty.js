$(document).ready(function () {
    $("div").filter(function () {
        return $.trim(this.innerHTML) === ""
    }).remove();
});