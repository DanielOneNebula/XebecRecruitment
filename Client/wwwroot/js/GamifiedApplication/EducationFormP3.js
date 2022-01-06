function educationalForm() {

    //It prevents the checkbox to click by the user
    $("#institution_check").click(function () { return false; });
    $("#qualification_check").click(function () { return false; });
    $("#eduStartDate_check").click(function () { return false; });
    $("#eduEndDate_check").click(function () { return false; });

    //The date checkboxes are already checked at the start of the page
    $("#eduStartDate_check").prop("checked", true);
    $("#eduStartDate_check").val(true);
    $("#eduEndDate_check").prop("checked", true);
    $("#eduEndDate_check").val(true);

    //Everytime the user types in, these functions checks if the field is populated or not by the indication of the checkbox
    $("#institution").keyup(function () {
        if ($(this).val().length != 0) {
            $("#institution_check").prop("checked", true);
            $("#institution_check").val(true);
        }
        else {
            $("#institution_check").prop("checked", false);
            $("#institution_check").val(false);
        }
    });

    $("#qualification").keyup(function () {
        if ($(this).val().length != 0) {
            $("#qualification_check").prop("checked", true);
            $("#qualification_check").val(true);
        }
        else {
            $("#qualification_check").prop("checked", false);
            $("#qualification_check").val(false);
        }
    });

    //This function here checks if the dates are not overlapping
    $("#dateStart").change(function () {
        if ($(this).val() > $("#dateEnd").val()) {
            $(this).val($("#dateEnd").val());
        }
    });

    $("#dateEnd").change(function () {
        if ($(this).val() < $("#dateStart").val()) {
            $(this).val($("#dateStart").val());
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