

//省级下拉
function TermProvince(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/ProvinceList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtProvince").html(html);
            }
        }
    });
}

//市级下拉
function TermCity(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/CityList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtCity").html(html);
            }
        }
    });
}

//县级下拉
function TermCounty(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/CountyList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtCounty").html(html);
            }
        }
    });
}

//农场下拉
function TermFarm(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/FarmList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtFarm").html(html);
            }
        }
    });
}

//大棚下拉
function TermGreenhouse(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/GreenhouseList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtGreenhouse").html(html);
            }
        }
    });
}

//气象站下拉
function TermWeather(obj) {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/WeatherList?id=" + obj,
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtWST_Id").html(html);
            }
        }
    });
}

//气象站类型下拉
function TermWeatherType() {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/WeatherTypeList",
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtST_Id").html(html);
            }
        }
    });
}

//远程控制类型下拉
function TermRemoteType() {
    $.ajax({
        async: false,
        dataType: "xml",
        type: "post",
        url: "/DropDownList/RemoteTypeList",
        success: function (data) {
            if (data != null) {
                var html = $(data).find("html").text();
                $("#txtST_Id").html(html);
            }
        }
    });
}

