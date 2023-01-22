$(document).ready(function () {

    $("[data-autofill]").each(function () {
        var $this = $(this);
        var $sendContentFrom = $($this.data("autofill"));

        $sendContentFrom.keyup(function () {
            var content = $sendContentFrom.val();

            $this.val(content);
        });
    });

    $(function () {
        $('.btn-group label.btn input[type=radio]')
          .hide()
          .filter(':checked').parent('.btn').addClass('active');
    });

    $('[data-toggle="tooltip"]').tooltip();

    $("#Interest_1").change(function () {
        $("#Interest_2").children('option').show();
        $("#Interest_2").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#Interest_2").change(function () {
        $("#Interest_1").children('option').show();
        $("#Interest_1").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#Ability_1").change(function () {
        $("#Ability_2").children('option').show();
        $("#Ability_2").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#Ability_2").change(function () {
        $("#Ability_1").children('option').show();
        $("#Ability_1").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#CareerValue_1").change(function () {
        $("#CareerValue_2").children('option').show();
        $("#CareerValue_2").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#CareerValue_2").change(function () {
        $("#CareerValue_1").children('option').show();
        $("#CareerValue_1").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#CareerGroup_1").change(function () {
        $("#CareerGroup_2").children('option').show();
        $("#CareerGroup_2").children("option[value^=" + $(this).val() + "]").hide();
    });

    $("#CareerGroup_2").change(function () {
        $("#CareerGroup_1").children('option').show();
        $("#CareerGroup_1").children("option[value^=" + $(this).val() + "]").hide();
    });

});

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//var confirmOnPageExit = function (e) {
//    // If we haven't been passed the event get the window.event
//    e = e || window.event;

//    var message = 'Any text will block the navigation and display a prompt';

//    // For IE6-8 and Firefox prior to version 4
//    if (e) {
//        e.returnValue = message;
//    }

//    // For Chrome, Safari, IE8+ and Opera 12+
//    return message;
//};
//window.onbeforeunload = confirmOnPageExit;

//function onSubmitForm() {
//    window.onbeforeunload = null;
//}