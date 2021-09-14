$("#reset").on("click", function () {
    $(".result-message").hide();
})
// A reference to Stripe.js initialized with your real test publishable API key.
var stripe = Stripe("pk_test_51JW1GNDBPIrSzoZVcLlFmxpp7UfPclVSWsX3UKac0ecRJQTYViaYnbgPuEUdql1G1fK8r0PsMWQXqhOHLMcxRt4Q00JRYPLzUY");

// The items the customer wants to buy
var purchase = {
    items: [{ id: "xl-tshirt" }]
};
var elements = stripe.elements();

var style = {
    base: {
        color: "#32325d",
        fontFamily: 'Arial, sans-serif',
        fontSmoothing: "antialiased",
        fontSize: "16px",
        "::placeholder": {
            color: "#32325d"
        }
    },
    invalid: {
        fontFamily: 'Arial, sans-serif',
        color: "#fa755a",
        iconColor: "#fa755a"
    }
};

var card = elements.create("card", { style: style });
// Stripe injects an iframe into the DOM
card.mount("#card-element");

card.on("change", function (event) {
    // Disable the Pay button if there are no card details in the Element
    $("button").attr("disabled", false);
    $("#card-error").html(event.error ? event.error.message : "");
});

//var form = document.getElementById("payment-form");
$("#payment-form").on("submit", function (event) {
    event.preventDefault();
    $.ajax({
        url: '/Index?handler=PaymentIntent&amount=' + ($("#amount").val() * 100),
        contentType: "application/json",
        dataType: "json",
        type: "GET", //send it through get method
        success: function (data) {
            // Complete payment when the submit button is clicked
            payWithCard(stripe, card, data);
        },
        error: function (xhr) {
            alert("Ocurrio un error al crear intento de pago")
            //Do Something to handle error
        }
    });

});
//});
var payWithCard = function (stripe, card, clientSecret) {
    loading(true);
    stripe
        .confirmCardPayment(clientSecret, {
            payment_method: {
                card: card
            }
        })
        .then(function (result) {
            if (result.error) {
                // Show error to your customer
                showError(result.error.message);
            } else {
                // The payment succeeded!
                orderComplete(result.paymentIntent.id);
            }
            loadTable();
            loadTable2();
        });
};
// Shows a success message when the payment is complete
var orderComplete = function (paymentIntentId) {
    loading(false);
    $(".result-message a").attr("href", "https://dashboard.stripe.com/test/payments/" + paymentIntentId);
    $(".result-message").show();
    setTimeout(function () {
        $(".result-message").hide();
    }, 4000);
    $("button").attr("disabled", true);
};

// Show the customer the error from Stripe if their card fails to charge
var showError = function (errorMsgText) {
    loading(false);
    $("#card-error").html(errorMsgText);
    setTimeout(function () {
        $("#card-error").html("")
    }, 4000);
};

// Show a spinner on payment submission
var loading = function (isLoading) {
    if (isLoading) {
        // Disable the button and show a spinner
        $("button").attr("disabled", true)
        $("#spinner").show();
    } else {
        $("button").attr("disabled", false)
        $("#spinner").hide();
    }
};
function loadTable() {
    $.ajax({
        contentType: "application/json",
        dataType: "json",
        url: 'https://api.stripe.com/v1/payment_intents?limit=50',
        headers: {
            'Authorization': 'Bearer sk_test_51JW1GNDBPIrSzoZVcjI8Xhvp0H6srnGaUtL3Bs3E0IosCjeoLN4e9l455lNwcq4YNlNy9CINtjw2n8eNeCnTm9qn002EbtAy1E'
        },
        type: "get", //send it through get method
        success: function (data) {
            //Do Something
            $("#trasactions-table tbody").find("tr").remove();
            if (data && data.data && data.data.length) {
                $.each(data.data, function (index, value) {
                    var html = rowFormat(value);
                    $("#trasactions-table tbody").append(html);
                })
            } else {
                $("#trasactions-table tbody").append(`<tr class="text-center">
                                <td colspan="5">No hay datos disponibles</td>
                            </tr>`);
            }

        },
        error: function (xhr) {
            alert("Ocurrio un error con el Obtener intentos (js)")
        }
    });
}
function loadTable2() {
    $.ajax({
        url: '/Index?handler=List',
        contentType: "application/json",
        dataType: "json",
        type: "get", //send it through get method
        success: function (result) {
            //Do Something
            var data = JSON.parse(result);
            $("#trasactions-table-2 tbody").find("tr").remove();
            if (data && data.data && data.data.length) {
                $.each(data.data, function (index, value) {
                    var html = rowFormat(value);
                    $("#trasactions-table-2 tbody").append(html);
                })
            } else {
                $("#trasactions-table-2 tbody").append(`<tr class="text-center">
                                <td colspan="5">No hay datos disponibles</td>
                            </tr>`);
            }
        },
        error: function (xhr) {
            alert("Ocurrio un error con el obtener intento de pago (.net)")
        }
    });
}
function rowFormat(value) {
    var date = new Date(value.created * 1000).toLocaleDateString();

    var html = `<tr>
                    <td class="text-right">${value.amount ? (value.amount / 100).toFixed(2) : ""}&emsp;</th>
                    <td class="text-left">&emsp;${value.status}</th>
                    <td class="text-right">&emsp;${date}</th>
                </tr>`;
    return html;
}


loadTable();
loadTable2();