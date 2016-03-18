$(document).ready(function () {
    var url = window.location;
    $('.menu').find('.active').removeClass('active');
    $('.menu li a').each(function () {
        if (this.href == url) {
            $(this).addClass('active');
        }
    });
});