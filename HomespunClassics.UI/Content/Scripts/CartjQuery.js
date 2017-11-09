$(function () {
    $("#dialog").dialog({
        autoOpen: false,//disables popup from opening on page load
        show: {//options used when dialog box is opened
            effect: "blind",
            duration: 1000
        },
        hide: {//options used when box is closed
            effect: "puff",
            duration: 1000
        }
    });
    //other effects: blind, bounce, clip, drop, fold, puff, pulsate
    //scale, shake, slide
    $("#cart-button").on("click", function () {
        $("#dialog").dialog("open");
    });
});