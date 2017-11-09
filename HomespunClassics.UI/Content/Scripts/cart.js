var items = [
    //In the array objects are enclosed with {} and comma-separated
    {
        id: 1,
        title: "Bow Headband",
        author: "Additional interchangeable snap-on bows available",
        price: 8.00
    },
    {
        id: 2,
        title: "Door Jammer",
        author: "Keep your little one from slamming the door",
        price: 6.00
    },
    {
        id: 3,
        title:"Cloud Flannel Blanket",
        author: "Coordinating accessories and custom embroidery available",
        price: 25.00
    },
    
];//End object initialization

//Array to store user cart info
var cart = [];

//Add items to cart -- wired to <a> and <button> tags
function addToCart(id) {
    //If they have NOT added any of the titles yet, set a qty property to 1,
    //and add the book to our array
    var itemObj = items[id - 1];
    if (typeof itemObj.qty === 'undefined') {
        itemObj.qty = 1;
        cart.push(itemObj);
    }
    else {
        itemObj.qty = itemObj.qty + 1;
    }

    //for testing purposes
    console.clear();
    for (var i = 0; i < cart.length; i++) {
        console.log(cart[i]);
    }

    //sum the total number of books in the cart
    var cartQty = 0;
    for (var i = 0; i < cart.length; i++) {
        cartQty += cart[i].qty;
    }
    //update cart notification with running total
    document.getElementById('cart-notification').style.display = 'block';
    document.getElementById('cart-notification').innerHTML = cartQty;

    document.getElementById('cart-contents').innerHTML = getCartContents();
}

//Functions to retrieve items in the cart and display in the dialog
function getCartContents() {
    var cartContents = "";
    var cartTotal = 0;

    for (var i = 0; i < cart.length; i++) {
        cartContents += cart[i].title + "<br />by " + cart[i].author
        + "<br />Qty: " + cart[i].qty + " at $" + cart[i].price.toFixed(2) + "<br/ ><br />";
        //Add total price of all books
        cartTotal += cart[i].qty * cart[i].price;
    }

    cartContents += "Cart total: $" + cartTotal.toFixed(2);
    return cartContents;
}
 