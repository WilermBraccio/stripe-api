using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SafetyPayTest.Models;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SafetyPayTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            StripeConfiguration.ApiKey = "sk_test_51JW1GNDBPIrSzoZVcjI8Xhvp0H6srnGaUtL3Bs3E0IosCjeoLN4e9l455lNwcq4YNlNy9CINtjw2n8eNeCnTm9qn002EbtAy1E";
        }
        public async Task<JsonResult> OnGetList()
        {
            var url = "https://api.stripe.com/v1/payment_intents?limit=50";
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer "+ StripeConfiguration.ApiKey);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Stripe-Account","acct_1JW1GNDBPIrSzoZV");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return new JsonResult(result);
            }
            return new JsonResult("");
        }
        public JsonResult  OnGetPaymentIntent(long? amount)
        {
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            });

            return new JsonResult(paymentIntent.ClientSecret);
        }

    }
}
