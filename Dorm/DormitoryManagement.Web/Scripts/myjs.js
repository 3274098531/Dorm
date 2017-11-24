//操作成功则刷新页面，失败提示失败原因
function callbackHandle(data) {
    if (data.IsSuccess) {
        $.pjax.reload("#main_content", { fragment: '#main_content', timeout: 10000 });
        $.sticky(data.Message, { autoclose: 5000, position: "top-right", type: "st-success" });
    } else {
        $.sticky(data.Message, { autoclose: 5000, position: "top-right", type: "st-error" });
    }
}

//打开页面，统一加上样式
$(function() {
    $("form").addClass("form-horizontal");
    $("form input[type='text']").addClass("form-control");
    $("form select").addClass("form-control");
    $("form textarea").addClass("form-control");
});

function SelectClassFormAcademy() {
    $('select#Academy').change(function() {
        $('select#Class').empty().append("<option value=" + "" + ">" + "---请选择---" + "</option>");;
        var academyName = $(this).find('option:selected').text();
        $.ajax({
            type: "get",
            url: "/Account/FindClassesByAcademy",
            data: "academyName=" + academyName,
            async: false, //取消异步  
            success: function(data) {
                var item = JSON.parse(data);
                for (var i = 0; i < item.length; i++) {
                    console.log(item[i][0] + "   " + item[i][1]);
                    $('select#Class').append("<option value=" + item[i][0] + ">" + item[i][1] + "</option>");
                }
            }
        });
    });
}

function SelectRoomFormDorm() {
    $('select#Dorm').change(function() {

        $('select#Room').empty().append("<option value=" + "" + ">" + "---请选择---" + "</option>");;
        var dormName = $(this).find('option:selected').text();
        $.ajax({
            type: "get",
            url: "/Account/FindRoomsByDorm",
            data: "dormName=" + dormName,
            async: false, //取消异步  
            success: function(data) {
                var item = JSON.parse(data);
                for (var j = 0; j < item.length; j++) {
                    $('select#Room').append("<option value=" + item[j][0] + ">" + item[j][1] + "</option>");
                }
            }
        });

    });
}

function SelectTeacherFormClass() {
    $('select#Class').change(function() {
        $('select#Teacher').empty().append("<option value=" + "" + ">" + "---请选择---" + "</option>");;
        var classname = $(this).find('option:selected').text();
        $.ajax({
            type: "get",
            url: "/Account/FindTeachersByClass",
            data: "classname=" + classname,
            async: false, //取消异步  
            success: function(data) {
                var item = JSON.parse(data);
                for (var k = 0; k < item.length; k++) {
                    $('select#Teacher').append("<option value=" + item[k].TeacherId + ">" + item[k].TeacherName + "</option>");
                }
            }
        });
    });
}

function GetAllCheckByName() {
    $('select#Name').change(function() {
        var name = $(this).find('option:selected').text();
        $.ajax({
            type: "get",
            url: "/CheckDorm/Index",
            data: "name=" + name,
            async: false, //取消异步   
        });
    });

}

function getStudentByCode() {
    $('#Code').change(function() {
        var code = $('#Code').val();
        $.ajax({
            type: "get",
            url: "/Help/GetStudentNameByCode",
            data: "code=" + code,
            async: false, //取消异步  
            success: function(data) {
                $('#Name').val(data);
            }
        });
    });
}

function CheckEmail(email) {
    var emailtest = /^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/;
    return emailtest.test(email);
}


function displaynavbar(obj) {
    if ($(obj).hasClass("open")) {
        $(obj).removeClass("open");
        $('#menu').css('display', 'block');
        $(".dislpayArrow").css("left", "190px");
    } else {
        $(obj).addClass("open");
        $(".dislpayArrow").css("left", "0px");

        $('#menu').css("display", "none");
    }
}

function loding() {
    var loding = document.getElementById("loding");
    var covershow = document.getElementById("lodingShow");
    loding.style.display = 'block';
    lodingShow.style.display = 'block';

}

function cancelloding() {
    var loding = document.getElementById("loding");
    var covershow = document.getElementById("lodingShow");
    loding.style.display = 'none';
    lodingShow.style.display = 'none';
}