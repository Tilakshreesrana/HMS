$(".btn-accomodationType").click(function () {
    var accomodationTypeId = $(this).attr("data-id");
    $(".accomodationType-row").hide();
    $("div.accomodationType-row[data-id="+accomodationTypeId + "]").show();

});