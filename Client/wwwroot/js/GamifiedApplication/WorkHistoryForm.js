function workHistoryForm() {

    //It prevents the checkbox to click by the user
    $("#company_name_check").click(function () { return false; });
    $("#job_title_check").click(function () { return false; });
    $("#start_date_check").click(function () { return false; });
    $("#end_date_check").click(function () { return false; });
    $("#job_description_check").click(function () { return false; });

    //The date checkboxes are already checked at the start of the page
    $("#start_date_check").prop("checked", true);
    $("#start_date_check").val(true);
    $("#end_date_check").prop("checked", true);
    $("#end_date_check").val(true);

    //Everytime the user types in, these functions checks if the field is populated or not by the indication of the checkbox
    $("#companyname").keyup(function () {
        if ($(this).val().length != 0) {
            $("#company_name_check").prop("checked", true);
            $("#company_name_check").val(true);
        }
        else {
            $("#company_name_check").prop("checked", false);
            $("#company_name_check").val(false);
        }
    });

    $("#jobtitle").keyup(function () {
        if ($(this).val().length != 0) {
            $("#job_title_check").prop("checked", true);
            $("#job_title_check").val(true);
        }
        else {
            $("#job_title_check").prop("checked", false);
            $("#job_title_check").val(false);
        }
    });

    $("#textarea").keyup(function () {
        if ($(this).val().length != 0) {
            $("#job_description_check").prop("checked", true);
            $("#job_description_check").val(true);
        }
        else {
            $("#job_description_check").prop("checked", false);
            $("#job_description_check").val(false);
        }

        if ($("input:checked").length == $("input:checkbox").length) {
            $("#next").show();
        }
        else {
            $("#next").hide();
        }
    });

    //This function here checks if the dates are not overlapping
    $("#startdate").on("change", function () {
        if ($(this).val() > $("#enddate").val()) {
            $(this).val($("#enddate").val());
        }
    });

    $("#enddate").on("change", function () {
        if ($(this).val() < $("#startdate").val()) {
            $(this).val($("#startdate").val());
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