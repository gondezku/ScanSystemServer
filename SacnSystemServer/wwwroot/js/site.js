// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
_wait = false;
function LoadFrame(_frame, _label) {
    var myIcons = $('#' + _frame)
    var _myLabel = myIcons.children().find("p").text()

    clearTimeout(_loadInterval)
    clearTimeout(_wait)
    $('#myContent').removeClass("animate__animated animate__fadeOutLeft")
    $('.main-panel').scrollTop(0)
    $('#myContent').html('').load("/home/" + _frame, function (response, status, xhr) {
        if (status == "error") {
            //location.reload();
            console.log(xhr)
        } else {
            ActivateScroll();
            var myIcons = $('#' + _past)
            // console.log(_label)
            if (_label != null) {
                myIcons.children().find("p").text(_label).removeClass('animate__flash animate__infinite').addClass('animate__flipInX')
                _loadInterval = setTimeout(() => {
                    clearTimeout(_loadInterval)
                    myIcons.children().find("p").removeClass('animate__animated animate__flipInX')
                }, 250);
            }
            if (_myLabel == "Loading") {
                location.reload();
            }
            $('#myContent').fadeIn("slow");
        }
    })
}
function ActivateScroll() {
    $('.table-responsive').each(function () {
        // console.log($(this)[0])
        var ps = new PerfectScrollbar($(this)[0]);
    });
}