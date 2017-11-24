$(function () {
    $(document).pjax('a[data-pjax]', '#main_content', { fragment: '#main_content', timeout: 10000 });
});

$(function () {
    $(document).pjax('a[data-post-pjax]', '#main_content', { type: 'post', fragment: '#main_content', timeout: 10000 });
});

//pjax方式的提交
$(document).on('submit', 'form[data-pjax]', function (event) {
    event.preventDefault();
    //防止点击两次
    var button = $(event.target).find("[type='submit']");
    if (button.attr('name') != 'Search') {
        button.attr("disabled", "true");
        button.html("正在" + button.html() + "...");
    }
    
    //提交表单
    $.pjax.submit(event, '#main_content', { fragment: '#main_content', "timeout": 10000 });

});

$(document).on('pjax:complete', function (xhr) {
    var button = $(xhr.relatedTarget).find("[type='submit']:not([name=Search])");
    button.removeAttr("disabled");
    button.html('<i class="fa fa-save"></i>保存');
});

//使用pjax加载页面时，统一加上样式
$(document).on('pjax:complete', function () {
    $("form").addClass("form-horizontal");
    $("form input[type='text']").addClass("form-control");
    $("form select").addClass("form-control");
    $("form textarea").addClass("form-control");
});