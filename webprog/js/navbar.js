$(document).ready(function () {
    var url = window.location.href;
    $('.menu').find('.active').removeClass('active');
    $('.menu li a').each(function () {
        if (this.href == url.split(/[?#]/)[0]) {
            $(this).addClass('active');
        } else {
            console.log(url.split(/[?#]/)[0]);
        }
    });
});