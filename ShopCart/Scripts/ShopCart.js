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

function RemoveFromCart(id, price) {
    RemoveSlideUpAnimation(id);
    console.log("RemoveFromCart id =", id);
    var obj = { id: id };
    $.ajax({
        url: "/Home/RemoveFromCart",
        data: JSON.stringify(obj),
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            console.log("RemoveFromCart success =", data);
            var removePrice = $("#sptotal").html();
            var totalPrice = parseInt(removePrice) - price;
            $("#sptotal").html(totalPrice);
            SetCartItemCount(data.cartItemsCount);
            //NotifyProductRemoved();
        },
        error: function (data) {
            console.log("AddToCart error =", data);
        }
    });
}
function decreaseQtyCount(qty, price, id) {
    var obj = {
        Qty: qty,
        Id: id,
        Price: price
        
    }
    if (qty > 1) {
        $.ajax({
            url: "/Home/DecreaseQty",
            data: JSON.stringify(obj),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {

                $("#qtyid_" + id).val(data.updateQty);
                var idtotoalprice = "#idtotalprice_" + id;
                //var minbtnid = "#minbtn_" + id;
                var total = data.updateQty * price;
                $(idtotoalprice).html('Total Price(' + price + 'X' + data.updateQty + '):' + total);

                console.log("Decrease Amount", data.grandAmount);
                $("#sptotal").html(data.grandAmount);



                console.log("DecreaseQty Data", data.grandAmount);
            },
            error: function (data) {
                console.log("DecreaseQty Data", data);
            },
            complete: function (xhr) {
                var data = $.parseJSON(xhr.responseText);
                EnableDisableButton(data, id);
                var string = "#qtyid_" + id;
                $(string).val(data.updateQty);

            }
        });
    }
    
}
function increaseQtyCount(qty, price, id) {
    var obj = {
        Qty: qty,
        Id: id,
        Price: price
    }
    if (qty < 10) {

        $.ajax({
            url: "/Home/IncreaseQty",
            data: JSON.stringify(obj),
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {

            },
            error: function (data) {
                console.log("IncreaseQty Data", string);
            },
            complete: function (xhr) {
                var data = $.parseJSON(xhr.responseText);
                console.log("increaseQtyCount data =", data);
                $("#qtyid_" + id).val(data.updateQty);
                $("#idtotalprice_" + id).html('Total Price(' + price + 'X' + data.updateQty + '):' + data.updateQty * price);
                $("#sptotal").html(data.grandAmount);
                EnableDisableButton(data, id);
            }
        });
    }
    
}



function RemoveSlideUpAnimation(id) {
    console.log("RemoveSlideUpAnimation id =", id);
    var string = "#cartitembox_" + id;
    $(string).fadeOut(1000);
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
    if (count == 0) {
        $("#btnplaceorder").hide();
        $("#idtotalprice").hide();
        $("#idtotalamount").hide();
        $("#idcartempty").show();
        
            $("#btnaddtocart").show();
       

    }
    console.log("SetCartItemCount count =", count);
    $("#cartItemCount").html('');
    $("#cartItemCount").append(count);
}

function EnableDisableButton(data,id) {
    console.log("EnableDisableButton data = ", data, id)

    if (data.updateQty = 10) {

        $("#maxbtn_" + id).attr("disabled", "disabled");
        //$("#maxbtn_"+id).removeAttr("disabled");
    }
    else {
        $("#maxbtn_"+id).attr("disabled", "disabled");
    }

    if (data.updateQty <= 1) {
        
        $("#minbtn_"+id).attr("disabled", "disabled");
    }
    else {      
        $("#minbtn_"+id).removeAttr("disabled");      
    }

}

