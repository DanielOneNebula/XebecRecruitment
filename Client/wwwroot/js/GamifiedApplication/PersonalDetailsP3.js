function personalDetails() {

   
    //It prevents the checkbox to click by the user
    $("#first_name_check").click(function () { return false; });
    $("#last_name_check").click(function () { return false; });
    $("#phone_number_check").click(function () { return false; });
    $("#email_check").click(function () { return false; });
    $("#id_number_check").click(function () { return false; });
    $("#disability_check").click(function () { return false; });
    $("#gender_check").click(function () { return false; });
    $("#ethnicity_check").click(function () { return false; });
    $("#address_check").click(function () { return false; });
    

    //The disability checkbox is already checked at the start of the page
    $("#disability_check").prop("checked", true);
    $("#disability_check").val(true);

    //Everytime the user types in, these functions checks if the field is populated or not by the indication of the checkbox
    $("#firstname").keyup(function () {
        if ($(this).val().length != 0) {
            $("#first_name_check").prop("checked", true);
            $("#first_name_check").val(true);
        }
        else {
            $("#first_name_check").prop("checked", false);
            $("#first_name_check").val(false);
        }
    });

    $("#lastName").keyup(function () {
        if ($(this).val().length != 0) {
            $("#last_name_check").prop("checked", true);
            $("#last_name_check").val(true);
        }
        else {
            $("#last_name_check").prop("checked", false);
            $("#last_name_check").val(false);
        }
    });

    $("#phonenumber").keyup(function () {
        if ($(this).val().length != 0 && $(this).val().length == 10) {
            $("#phone_number_check").prop("checked", true);
            $("#phone_number_check").val(true);
            alert($.type(phoneNo));
        }
        else {
            $("#phone_number_check").prop("checked", false);
            $("#phone_number_check").val(false);
        }
    });

    $("#email").keyup(function () {
        if ($(this).val().length != 0) {
            $("#email_check").prop("checked", true);
            $("#email_check").val(true);
        }
        else {
            $("#email_check").prop("checked", false);
            $("#email_check").val(false);
        }
    });

    $("#idnumber").keyup(function () {
        if ($(this).val().length != 0 && $(this).val().length == 13) {
            $("#id_number_check").prop("checked", true);
            $("#id_number_check").val(true);
        }
        else {
            $("#id_number_check").prop("checked", false);
            $("#id_number_check").val(false);
        }
    });

    $("#gender").on("change", function () {
        if ($(this).val() != "empty") {
            $("#gender_check").prop("checked", true);
            $("#gender_check").val(true);
        }
        else {
            $("#gender_check").prop("checked", false);
            $("#gender_check").val(false);
        }

        if ($("input:checked").length == $("input:checkbox").length) {
            $("#next").show();
        }
        else {
            $("#next").hide();
        }
    });

    $("#ethnicity").keyup(function () {
        if ($(this).val().length != 0) {
            $("#ethnicity_check").prop("checked", true);
            $("#ethnicity_check").val(true);
        }
        else {
            $("#ethnicity_check").prop("checked", false);
            $("#ethnicity_check").val(false);
        }
    });

    $("#address").keyup(function () {
        if ($(this).val().length != 0) {
            $("#address_check").prop("checked", true);
            $("#address_check").val(true);
        }
        else {
            $("#address_check").prop("checked", false);
            $("#address_check").val(false);
        }

        if ($("input:checked").length == $("input:checkbox").length) {
            $("#next").show();
        }
        else {
            $("#next").hide();
        }
    });

    //This function here checks if all the checkboxes are checked, if they are then the NEXT Button appears
    $("input:text").keyup(function () {
        if ($("input:checked").length == $("input:checkbox").length) {
            $("#next").show();
        }
        else {
            $("#next").hide();
        } 
    });
}