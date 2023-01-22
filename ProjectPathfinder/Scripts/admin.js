$(document).ready(function () {

    $(".test-element").bind('click', function () { return false; });
    $(".test-element").keypress(function (e) { return false; });
    $('select.test-element').on('mousedown', function (e) {
        e.preventDefault();
        this.blur();
        window.focus();
    });

    $(function () {
        $('.btn-group label.btn input[type=radio]')
          .hide()
          .filter(':checked').parent('.btn').addClass('active');
    });

    $('[data-toggle="tooltip"]').tooltip();
});