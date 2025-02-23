// $(document).ready(function () {
//     //$('#site').toggle();
//     //$('#aku').toggle();
//     var current_path = window.location.pathname.split('/').pop();
//     ('#myContent').load("/home/" + current_path)
// })
$(function () {
    let BissName = $('#BUName').text();
    $("a").on("click", function () {
        var icons = $(this).attr('data-icon');
        if (icons == 'siteAdmin') {
            var BName = "IAQ-FAN";
            icons = icons + "?name=" + BissName;
        }
        $('#myContent').fadeOut("fast").html('').toggle();
        $('#myContent').load("/home/" + icons, function () {
            start_loader()
            $('.table-responsive').each(function () {
                var ps3 = new PerfectScrollbar($(this)[0]);
            });
            $('#myContent').fadeIn("fast");
            end_loader()
            Segment1Script(icons, BissName);
        });
    });

});

function Segment1Script(icons,BissName) {
    $('#tableBU tr').click(function () {
        var BName = this.cells[0].innerHTML;
        var BName2 = this.cells[1].innerHTML;
        if (icons == 'siteSU') {
            var cekTbl0 = $("#tableSection > tbody > tr").length;
            if (cekTbl0 != 0) {
                $('#tableSection tr').remove();
            }
            start_loader()
            $('#segment2').fadeOut("fast");
            $('#segment1').fadeOut("fast", function () {
                var segmentName1 = $('#segment1').attr('data-segment')
                $('#segment1').load("/home/" + segmentName1 + "?name=" + BName, function () {
                    $(this).addClass("col-lg-4");
                    $('.table-responsive').each(function () {
                        var ps3 = new PerfectScrollbar($(this)[0]);
                    })
                    $('#sectionTitle').text(BName2);
                    $(this).fadeIn("fast", function () {
                        var cekTbl = $("#tableSection > tbody > tr").length;
                        if (cekTbl == 0) {
                            $('#tableSection').append("<span>No results</span>")
                        }
                    });
                    end_loader();
                    Segment2Script();
                })
            })
        }
        else {
            start_loader()
            $('#segment1').fadeOut("fast", function () {
                var segmentName1 = $('#segment1').attr('data-segment')
                
                $('#segment1').load("/home/" + segmentName1 + "?SectionBU=" + BName2, function () {
                    $(this).addClass("col-lg-6");
                    $('.table-responsive').each(function () {
                        var ps3 = new PerfectScrollbar($(this)[0]);
                    })
                    $('#BUTittle').text(BissName);
                    $('#siteTitle').text(BName);
                    $(this).fadeIn("fast", function () {
                        var cekTbl2 = $("#tableSite > tbody > tr").length;
                        if (cekTbl2 == 0) {
                            $('#tableSite').append("<span>No results</span>")
                        }
                    })
                    end_loader()
                    CreateNewSite(BissName, BName);
                })
            })
        }
    })
    $('#create_new_section').click(function () {
        uni_modal("Add/Edit Section", "/home/AddSection?BU=" + BissName + "&&AttId=New")
    })

}

function Segment2Script() {
    $('#tableSection tr').click(function () {
        var BUName = $('#sectionTitle').text();
        var SectionName = this.cells[1].innerHTML;
        var SectionName1 = this.cells[0].innerHTML;

        start_loader()
        $('#segment2').fadeOut("fast", function () {
            var segmentName2 = $('#segment2').attr('data-segment')
            $('#segment2').load("/home/" + segmentName2 + "?SectionBU=" + SectionName, function () {
                $(this).addClass("col-lg-4");
                $('.table-responsive').each(function () {
                    var ps3 = new PerfectScrollbar($(this)[0]);
                })
                $('#BUTittle').text(BUName);
                $('#siteTitle').text(SectionName1);
                $(this).fadeIn("fast", function () {
                    var cekTbl2 = $("#tableSite > tbody > tr").length;
                    if (cekTbl2 == 0) {
                        $('#tableSite').append("<span>No results</span>")
                    }
                });
                end_loader()
                CreateNewSite(BUName, SectionName1);
            })
        })

    })

    $('#create_new_section').click(function () {
        var BUName = $('#sectionTitle').text();
        uni_modal("Add/Edit Section", "/home/AddSection?BU=" + BUName + "&&AttId=New")
    })

}

function CreateNewSite(BUName, SecName) {
    $('#create_new_site').click(function () {
        uni_modal("Add/Edit Site", "/home/AddSite?SecName=" + SecName + "&&BUName=" + BUName + "&&AttId=New")
    })

    $('#tableSite tr').dblclick(function () {
        var SectionName1 = this.cells[0].innerHTML;
        uni_modal("Add/Edit Site", "/home/AddSite?SecName=" + SecName + "&&BUName=" + BUName + "&&AttId=" + SectionName1)
    })
}

function GetToken() {
    return document.querySelector("[name='__RequestVerificationToken']").value;
}
