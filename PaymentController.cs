using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SheypoorChi.Core.ViewModels;
using System.Text;


namespace SheypoorChi.Controllers;

public class PaymentController : Controller
{
    private static readonly HttpClient client = new HttpClient();

    //private readonly string zarinPalUrl = "https://www.zarinpal.com";
    private static readonly string zarinPalUrl = "https://sandbox.zarinpal.com";

    //private readonly string mainUrl = "https://www.host-domain.com";
    private static readonly string mainUrl = "https://localhost:44394/";

    private readonly string requestUrl =
        $"{zarinPalUrl}/pg/rest/WebGate/PaymentRequest.json";

    private readonly string verifyUrl =
        $"{zarinPalUrl}/pg/rest/WebGate/PaymentVerification.json";

    //private const string merchantId = "zarinpal-user-merchant-id";
    private const string merchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";

    public async Task<IActionResult> RequestPaymentGate()
    {
        //create request data
        var requestData = new Dictionary<string, string>()
        {
            {"MerchantID",merchantId },
            {"Amount","10000" },
            {"CallbackURL",mainUrl},
            {"Mobile","" },
            {"Description","تست درگاه پرداخت زرین پال" }
        };

        //convert request dictionary to request json
        var requestJson = JsonConvert.SerializeObject(requestData);
        var request =
            new StringContent(requestJson, Encoding.UTF8, "application/json");

        //send request to zarinpal
        var requestResponse = await client.PostAsync(requestUrl, request);
        var response = await requestResponse.Content.ReadAsStringAsync();

        ZarinpalResponse zarinpalResponse =
            JsonConvert.DeserializeObject<ZarinpalResponse>(response);

        //check zarinpal status
        if (zarinpalResponse.status is 100)
        {
            //go to payment gate
            return Redirect($"{zarinPalUrl}/pg/StartPay/{zarinpalResponse.authority}");
        }

        return View();
    }
}
