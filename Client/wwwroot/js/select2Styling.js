function jobElement() {
    var items1 = [[]];
    var items2 = [[]];

    $(".jobTypeSelect2").select2({
        placeholder: "--Select Job Type--"
    });

    $(".jobPlatformSelect2").select2({
        placeholder: "--Select Any Social Media--"
    });

    $("#jobType").on("select2:select select2:unselect", function () {
        var textString = [];
        items1[0] = $(this).val();
        $("#jobType option:selected").each(function (index, content) {
            textString.push(content.textContent);
        });
        items1[1] = textString;
        DotNet.invokeMethodAsync('XebecPortal.Client', 'jobTypeModel', items1.toString());
    })

    $("#jobPlatform").on("select2:select select2:unselect", function () {
        var textString = [];
        items2[0] = $(this).val();
        $("#jobPlatform option:selected").each(function (index, content) {
            textString.push(content.textContent);
        });
        items2[1] = textString;
        DotNet.invokeMethodAsync('XebecPortal.Client', 'jobPlatformModel', items2.toString());
    })
}