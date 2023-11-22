export function zoomin() {
    var myImg = document.getElementById("Product.Image");
    var currWidth = myImg.clientWidth;
    if (currWidth >= 1000) {
        alert("You’re fully zoomed in!");
    } else {
        myImg.style.width = (currWidth + 100) + "px";
    }
}

export function zoomout() {
    var myImg = document.getElementById("Product.Image");
    var currWidth = myImg.clientWidth;
    if (currWidth <= 50) {
        alert("That’s as small as it gets.");
    } else {
        myImg.style.width = (currWidth - 100) + "px";
    }
}