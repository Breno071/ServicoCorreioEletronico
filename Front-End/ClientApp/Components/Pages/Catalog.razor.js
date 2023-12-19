$(".product-colors span").click(function () {
    $(".product-colors span").removeClass("active");
    $(this).addClass("active");
    $(".active").css("border-color", $(this).attr("data-color-sec"))
    /*$("#catalog").css("background", $(this).attr("data-color-primary"));*/
    $(".content h2").css("color", $(this).attr("data-color-sec"));
    $(".content h3").css("color", $(this).attr("data-color-sec"));
    $(".container .imgBx").css("background", $(this).attr("data-color-sec"));
    $(".container .details button").css("background", $(this).attr("data-color-sec"));
    $(".imgBx img").attr('src', $(this).attr("data-pic"));
});