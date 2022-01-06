function additionalInfo() {

    //It prevents the checkbox to click by the user
    $("#github_check").click(function () { return false; });
    $("#facebook_check").click(function () { return false; });
    $("#linkedin_check").click(function () { return false; });
    $("#website_check").click(function () { return false; });
    $("#job_description_check").click(function () { return false; });

    //Everytime the user types in, these functions checks if the field is populated or not by the indication of the checkbox
    $("#github").keyup(function () {
        if ($(this).val().length != 0) {
            $("#github_check").prop("checked", true);
            $("#github_check").val(true);
        }
        else {
            $("#github_check").prop("checked", false);
            $("#github_check").val(false);
        }
    });

    $("#facebook").keyup(function () {
        if ($(this).val().length != 0) {
            $("#facebook_check").prop("checked", true);
            $("#facebook_check").val(true);
        }
        else {
            $("#facebook_check").prop("checked", false);
            $("#facebook_check").val(false);
        }
    });

    $("#linkedin").keyup(function () {
        if ($(this).val().length != 0) {
            $("#linkedin_check").prop("checked", true);
            $("#linkedin_check").val(true);
        }
        else {
            $("#linkedin_check").prop("checked", false);
            $("#linkedin_check").val(false);
        }
    });

    $("#website").keyup(function () {
        if ($(this).val().length != 0) {
            $("#website_check").prop("checked", true);
            $("#website_check").val(true);
        }
        else {
            $("#website_check").prop("checked", false);
            $("#website_check").val(false);
        }
    });

    //This function checks if the user has uploaded a file or more by the indication of the checkbox
    $("#fileInput").on("change", function () {
        if ($(this).val() != "") {
            $("#job_description_check").prop("checked", true);
            $("#job_description_check").val(true);
        }
        if ($("input:checked").length == $("input:checkbox").length) {
            $("#submit").show();
        }
        else {
            $("#submit").hide();
        }
    });

    //This function checks if the user has removed all the files by unchecking the checkbox
    $("#removeFile").click(function () {
        if ($(".drop-zone-button").length == 1) {
            $("#job_description_check").prop("checked", false);
            $("#job_description_check").val(false);
        }
        if ($("input:checked").length == $("input:checkbox").length) {
            $("#submit").show();
        }
        else {
            $("#submit").hide();
        }
    });

    //This function here checks if all the checkboxes are checked, if they are then the NEXT Button appears
    $("input:text").keyup(function () {
        if ($("input:checked").length == $("input:checkbox").length) {
            $("#submit").show();
        }
        else {
            $("#submit").hide();
        }
    });
}