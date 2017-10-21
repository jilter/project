$(function () {

    $(window).resize(function () {
        $('.am_listimg_info .am_imglist_time').css('display', $('.am_list_block').width() <= 170 ? 'none' : 'block');
    });


    //@首页 底部下载按钮

    // function mouse_over_out(obj, style, overcss, outcss) {
    //      obj.bind('mouseover', function() {
    //          $(this).css(style, overcss);
    //      });
    //      obj.bind('mouseout', function() {
    //          $(this).css(style, outcss);
    //      });
    //  }



    //@首页 图片列表长宽相等

    $(window).resize(function () {
        $('.am_img_bg').height($('.am_img_bg').width());
    });

    var page = 1;
    var count = 0;
    var rows = 40;
    var pages = 0;
    var category = "";
    var order = "";
    var query = "";

    var getQueryString=function(url, name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = url.substr(url.indexOf('?') + 1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }

    var regPage = function () {
        $(".am-pager").empty();
        if ($(window).width() < 700) {
            $(".am-pager").page({
                pages: pages,
                curr:page,
                first: "首页", //设置false则不显示，默认为false  
                last: "尾页", //设置false则不显示，默认为false      
                prev: false, //若不显示，设置false即可，默认为上一页
                next: false, //若不显示，设置false即可，默认为下一页
                groups: 4, //连续显示分页数
                jump: function (context, first) {
                    pageJump(context, first);
                }
            });
        } else {
            $(".am-pager").page({
                pages: pages,
                curr: page,
                first: "首页", //设置false则不显示，默认为false  
                last: "尾页", //设置false则不显示，默认为false      
                prev: '上一页', //若不显示，设置false即可，默认为上一页
                next: '下一页', //若不显示，设置false即可，默认为下一页
                groups: 10, //连续显示分页数
                jump: function (context, first) {
                    pageJump(context, first);
                }
            });
        }
    }

    var getlist = function () {
        var $loading = AMUI.dialog.loading({ title:'努力加载中...'});
        $.post('/default/getlist', { c: category, o: order, p: page,q:query }, function (response) {
            $loading.modal('close');
            if (response == '') {
                AMUI.dialog.alert({
                    title: '错误提示',
                    content: '网络连接失败，稍后再试'
                });
            } else {
                try {
                    var data = eval('(' + response + ')');
                    console.log(data);
                    page = parseInt(data.page);
                    if (category != '') {
                        switch (category) {
                            case 'baby': count = data.categories.baby.count; break;
                            case 'beauty': count = data.categories.beauty.count; break;
                            case 'book': count = data.categories.book.count; break;
                            case 'clothes': count = data.categories.clothes.count; break;
                            case 'elec': count = data.categories.elec.count; break;
                            case 'food': count = data.categories.food.count; break;
                            case 'health': count = data.categories.health.count; break;
                            case 'home': count = data.categories.home.count; break;
                            case 'others': count = data.categories.others.count; break;
                            case 'side': count = data.categories.side.count; break;
                            case 'sports': count = data.categories.sports.count; break;
                            default: count = 40; break;
                        }
                    }
                    else {
                        count = data.count;
                        if (count == 0) {
                            count = data.total;
                        }
                    }
                    rows = data.rows||20;
                    pages = count % rows == 0 ? parseInt(count / rows) : parseInt(count / rows) + 1;
                    pages = pages > 250 ? 250 : pages;
                    console.log(rows,pages);
                    var html = "";
                    var itemId = "";
                    var activityId = "";
                    var url = "";
                    $.each(data.list, function (index, item) {
                        itemId = getQueryString(item.url, "id");
                        activityId = getQueryString(item.coupon_url, "activityId");
                        url = "/default/goto?itemId=" + itemId + "&activityId=" + activityId;
                        html += '<li>' +
                        '       <div class="am-gallery-item am_list_block">' +
                        '     <a target="_blank" href="' + url + '" class="am_img_bg">' +
                        '            <img class="am_img animated" src="/assets-web/img/loading.gif" data-original="' + item.image_path_300 + '" alt="' + item.title + '" />' +
                        '    </a>' +
                        '            <div class="am_imglist_user_font"><a target="_blank" href="' + url + '">' + item.title + '</a></div>' +
                        '            <div class="am_listimg_info"><a target="_blank" href="' + url + '">' +
                        '                <span class="am-icon-coupon">¥' + parseInt(item.coupon_price) + '</span>' +
                        '                <span class="am-icon-price">券后:¥' + item.price + '</span>' +
                        '          </a></div>' +
                        '   <div style="font-size:12px;"><span>销量<span>' + item.history_30d_sales + '</span>&nbsp;&nbsp;&nbsp;&nbsp;优惠剩余<span>' + item.coupon_spare_day + '</span>天</span></div>' +
                        '        </div>' +
                        '    </li>';
                        //html += '<li>' +
                        //'       <div class="am-gallery-item am_list_block">' +
                        //'     <a target="_blank" href="' + url + '" class="am_img_bg">' +
                        //'            <img class="am_img animated" src="/assets-web/img/loading.gif" data-original="' + item.image_path_300 + '" alt="' + item.title + '" />' +
                        //'    </a>' +
                        //'            <div class="am_imglist_user_font"><a target="_blank" href="' + url + '">' + item.title + '</a></div>' +
                        //'            <div class="am_listimg_info"><a target="_blank" href="' + url + '">' +
                        //'                <span class="am-icon-price">优购分享:  内部专享优惠！已抢' + item.history_30d_sales + '件！券后:¥' + item.price + ',' + item.intro + '</span>' +
                        //'          </a></div>' +
                        //'        </div>' +
                        //'    </li>';
                    });
                    $("#product-list").html(html);
                    if (page == 1) {
                        regPage();
                    }
                    //@懒加载
                    $("img.am_img").lazyload({ threshold: 200 });
                    $("a.am_img_bg").lazyload({
                        effect: 'fadeIn',
                        threshold: 500
                    });

                    $('.am_list_block').off();
                    //@首页 图片滑动效果
                    $(".am_list_block").on('mouseover', function () {
                        $('.am_img_bg').removeClass('am_img_bg');
                        $(this).find('.am_img').addClass('bounceIn');
                    });
                    $('.am_img').on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
                        $('.am_img').removeClass('bounceIn');
                    });
                    if ($(window).width() < 700) {
                        $('.am_list_block').off();
                    }
                }
                catch (e) {
                    AMUI.dialog.alert({
                        title: '错误提示',
                        content: '网络连接失败，稍后再试'+e.toString(),
                        onConfirm: function () {
                            console.log('close');
                        }
                    });
                }
            }
            console.log();
        }, "text");
    }
    getlist();

    $('.am-topbar-nav li').bind("click", function () {
        category = $(this).attr("data-category");
        $(".am-topbar-nav .am-active").removeClass("am-active");
        $(this).addClass("am-active");
        page = 1;
        query = "";
        $("#strKey").val("");
        getlist();
    });

    $("#btnSearch").bind("click", function () {
        query = $("#strKey").val();
        page = 1;
        getlist();
    });

    $(".banner_nav a").bind("click", function () {
        var neworder = $(this).attr("data-order");
        if (neworder == "p") {
            $(this).attr("data-order", "d");
        }
        if (neworder == "d") {
            $(this).attr("data-order", "p");
        }
        if (neworder != order) {
            order = neworder;
            $(".banner_nav .banner_hover").removeClass("banner_hover");
            $(this).addClass("banner_hover");
            getlist();
        }
    });

    var pageJump = function (context, first) {
        if (!first && page != context.option.curr) {
            page = context.option.curr;
            $('html, body').animate({
                scrollTop: 0
            }, 0);
            getlist();
        }
    }
});

//动画效果

function OpenDonghua(Chufa, Mubiao, Xiaoguo) {
    Chufa.on('mouseover', function () {
        $(this).find(Mubiao).addClass(Xiaoguo);
    });
}

function CloseDonghua(Chufa, Mubiao, Xiaoguo) {
    Chufa.on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
        Mubiao.removeClass(Xiaoguo);
    });
}