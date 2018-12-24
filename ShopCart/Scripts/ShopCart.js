GetCartItems();

function GetCartItems() {
    $.ajax({
        url: "/Home/GetCartItems",
        data: JSON.stringify("1"),
        type: "POST",
        dataType: "json",
        contentType: "application/json",

        success: function (data) {

            console.log("GetCartItems success =", data);
            SetCartItemCount(data.cartItemsCount);
        },
        error: function (data) {
            console.log("GetCartItems error =", data);
        }
    });
}



function AddToCart(id) {
    //AddedToCartNotification();
    console.log("AddToCart ProductId =", id);
    var obj = { id: id };
    $.ajax({
        url: "/Home/AddToCart",
        data: JSON.stringify(obj),
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            console.log("AddToCart success =", data);
            console.log("AddToCart productExistenceStatus =", data.productStatus);
            SetCartItemCount(data.totalCount);

            //if (data.productStatus == true) {
            //    NotifyProductAlreadyExists();
            //}
            //else {
            //    NotifyProductAdded();
            //}
        },
        error: function (data) {
            console.log("AddToCart error =", data);
        }
    });
}

function SetCartItemCount(count) {
    console.log("SetCartItemCount count =", count);
    $("#cartItemCount").html('');
    $("#cartItemCount").append(count);
}



//This is for shipping details
//var shiparr = [];
//$("#btnsubmit").click(function () {
//    console.log('click');
//    var cname = $("#idcname").val();

//    var address = $("#idaddress").val();
//    var mobile = $("#idmobile").val();
//    var obj = {

//        Cname: cname,
//        Address: address,
//        Mobile: mobile,

//    }
//    $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'POST',
//        url: "/Home/Shipping",
//        data: obj,
//        success: function (result) {
//            alert(result.msg);

//        },
//        error: function () {
//            alert("Error!")
//        }
//    });
//});

//console.log("Data", data);

