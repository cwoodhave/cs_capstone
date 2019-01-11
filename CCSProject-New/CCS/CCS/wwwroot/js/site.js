window.onload = finalize();

function finalize()
{
    checkSize();
    window.onresize = checkSize;
}

function checkSize()
{
    var ww = $(window).width();
    if (ww < 768)
    {
        console.log("SM");
        var cols = document.getElementsByClassName("main-col");
        for (var i = 0; i < cols.length; i++) {
            if (!cols[i].classList.contains("main-col-stacked")) {
                cols[i].classList.add("main-col-stacked");
            }
            if (cols[i].classList.contains("main-col-sided")) {
                cols[i].classList.remove("main-col-sided");
            }
        }
        var conts = document.getElementsByClassName("container");
        for (var i = 0; i < conts.length; i++) {
            if (conts[i].classList.contains("container-full")) {
                conts[i].classList.remove("container-full");
            }
        }
    }
    else if (ww >= 768 && ww < 992)
    {
        console.log("MD");
        var cols = document.getElementsByClassName("main-col");
        for (var i = 0; i < cols.length; i++) {
            if (!cols[i].classList.contains("main-col-stacked")) {
                cols[i].classList.add("main-col-stacked");
            }
            if (cols[i].classList.contains("main-col-sided")) {
                cols[i].classList.remove("main-col-sided");
            }
        }
        var conts = document.getElementsByClassName("container");
        for (var i = 0; i < conts.length; i++) {
            if (conts[i].classList.contains("container-full")) {
                conts[i].classList.remove("container-full");
            }
        }
    }
    else if (ww >= 992)
    {
        console.log("LG");
        var cols = document.getElementsByClassName("main-col");
        for (var i = 0; i < cols.length; i++) {
            if (cols[i].classList.contains("main-col-stacked")) {
                cols[i].classList.remove("main-col-stacked");
            }
            if (!cols[i].classList.contains("main-col-sided")) {
                cols[i].classList.add("main-col-sided");
            }
        }
        var conts = document.getElementsByClassName("container");
        for (var i = 0; i < conts.length; i++) {
            if (conts[i].classList.contains("container-full")) {
                conts[i].classList.remove("container-full");
            }
        }
    }
    else if (ww >= 1200) {
        console.log("XL");
        var conts = document.getElementsByClassName("container");
        for (var i = 0; i < conts.length; i++) {
            if (!conts[i].classList.contains("container-full")) {
                conts[i].classList.add("container-full");
            }
        }
    }
}