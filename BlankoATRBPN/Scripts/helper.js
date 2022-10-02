const pathMenu = bashPath + "Menu/GetMenuJson";

function writeSession(name, value) {
    if (typeof (Storage) !== "undefined")
        sessionStorage.setItem(name, value)
    else
        console.log("Sorry your browser does not support Web Storage")
}

function readSession(name) {
    if (typeof (Storage) !== "undefined")
        return sessionStorage.getItem(name);
    else
        console.log("Sorry your browser does not support Web Storage")

}


function convertToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    return pad(dt.getDate().toString(), 2) + "/" + pad( dt.getMonth()+1 , 2) + "/" + dt.getFullYear();
}
//-----------------------------add format 00 in number---------------------------//
function pad(n, len) {
    let l = Math.floor(len);
    let sn = '' + n;
    let snl = sn.length;
    if (snl >= l) return sn;
    return '0'.repeat(l - snl) + sn;
}
//-----------------------------for add commas in Rp ------------------------------//
function numberWithCommas(x) {
    return "Rp " + x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + ",00";
}

//-----------------------------Replace Space to unders score----------------------//
function replcaeSpaceToUndersCore(value) {
    return value.toString().replace(/ /g, "_");
}

jQuery.loadScript = function (url) {
    jQuery.ajax({ url: url, dataType: 'script', async: false })
}
