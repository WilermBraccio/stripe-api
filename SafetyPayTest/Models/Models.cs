using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyPayTest.Models
{
    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
    }

    public class BillingDetails
    {
        public Address address { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class FraudDetails
    {
    }

    public class Metadata
    {
    }

    public class Outcome
    {
        public string network_status { get; set; }
        public string reason { get; set; }
        public string risk_level { get; set; }
        public int? risk_score { get; set; }
        public string seller_message { get; set; }
        public string type { get; set; }
    }

    public class Checks
    {
        public string address_line1_check { get; set; }
        public string address_postal_code_check { get; set; }
        public string cvc_check { get; set; }
    }

    public class Card
    {
        public string brand { get; set; }
        public Checks checks { get; set; }
        public string country { get; set; }
        public int? exp_month { get; set; }
        public int? exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public object installments { get; set; }
        public string last4 { get; set; }
        public string network { get; set; }
        public object three_d_secure { get; set; }
        public object wallet { get; set; }
        public string request_three_d_secure { get; set; }
    }

    public class PaymentMethodDetails
    {
        public Card card { get; set; }
        public string type { get; set; }
    }

    public class Refunds
    {
        public string @object { get; set; }
        public List<Datum2> data { get; set; }
        public bool has_more { get; set; }
        public int? total_count { get; set; }
        public string url { get; set; }
    }

    public class Datum2
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int? amount { get; set; }
        public int? amount_captured { get; set; }
        public int? amount_refunded { get; set; }
        public string application { get; set; }
        public decimal? application_fee { get; set; }
        public decimal? application_fee_amount { get; set; }
        public string balance_transaction { get; set; }
        public BillingDetails billing_details { get; set; }
        public string calculated_statement_descriptor { get; set; }
        public bool captured { get; set; }
        public int? created { get; set; }
        public string currency { get; set; }
        public string customer { get; set; }
        public string description { get; set; }
        public string destination { get; set; }
        public string dispute { get; set; }
        public bool disputed { get; set; }
        public string failure_code { get; set; }
        public string failure_message { get; set; }
        public FraudDetails fraud_details { get; set; }
        public string invoice { get; set; }
        public bool livemode { get; set; }
        public Metadata metadata { get; set; }
        public string on_behalf_of { get; set; }
        public string order { get; set; }
        public Outcome outcome { get; set; }
        public bool paid { get; set; }
        public string payment_intent { get; set; }
        public string payment_method { get; set; }
        public PaymentMethodDetails payment_method_details { get; set; }
        public string receipt_email { get; set; }
        public string receipt_number { get; set; }
        public string receipt_url { get; set; }
        public bool refunded { get; set; }
        public Refunds refunds { get; set; }
        public string review { get; set; }
        public string shipping { get; set; }
        public string source { get; set; }
        public string source_transfer { get; set; }
        public string statement_descriptor { get; set; }
        public string statement_descriptor_suffix { get; set; }
        public string status { get; set; }
        public string transfer_data { get; set; }
        public string transfer_group { get; set; }
        public int? amount_capturable { get; set; }
        public int? amount_received { get; set; }
        public string canceled_at { get; set; }
        public string cancellation_reason { get; set; }
        public string capture_method { get; set; }
        public Charges charges { get; set; }
        public string client_secret { get; set; }
        public string confirmation_method { get; set; }
        public string last_payment_error { get; set; }
        public string next_action { get; set; }
        public PaymentMethodOptions payment_method_options { get; set; }
        public List<string> payment_method_types { get; set; }
        public string setup_future_usage { get; set; }
    }

    public class Charges
    {
        public string @object { get; set; }
        public List<Datum2> data { get; set; }
        public bool has_more { get; set; }
        public int? total_count { get; set; }
        public string url { get; set; }
    }

    public class PaymentMethodOptions
    {
        public Card card { get; set; }
    }

    public class RootViewModel
    {
        public string @object { get; set; }
        public List<Datum2> data { get; set; }
        public bool has_more { get; set; }
        public string url { get; set; }
    }
}
