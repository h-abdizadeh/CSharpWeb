using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SheypoorChi.Core.Interface;
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

    IShopping _shopping;
    public PaymentController(IShopping shopping)
    {
        _shopping = shopping;
    }

    public async Task<IActionResult> RequestPaymentGate(int id)//factorId
    {
        var factor = await _shopping.GetFactor(id);
        if (factor is null) return RedirectToAction("index", "home");

        //create request data
        var requestData = new Dictionary<string, string>()
        {
            {"MerchantID",merchantId },
            {"Amount",factor.TotalPrice.ToString() },
            {"CallbackURL",$"{mainUrl}payment/VerifyPaymentGate?factorId={factor.Id}"},
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


    public async Task<IActionResult> VerifyPaymentGate(int factorId, string status, string authority)
    {
        if (status.ToLower() is "ok")
        {
            var factor = await _shopping.GetFactor(factorId);
            if (factor is null) return View("ShoppingCart", "home");

            var verifyData = new Dictionary<string, string>()
            {
                {"MerchantID",merchantId},
                {"Authority",authority},
                {"Amount",factor.TotalPrice.ToString()},
            };

            var verifyJson = JsonConvert.SerializeObject(verifyData);
            var verify =
                new StringContent(verifyJson, Encoding.UTF8, "application/json");

            var verifyResponse = await client.PostAsync(verifyUrl, verify);
            var response =await verifyResponse.Content.ReadAsStringAsync();

            ZarinPalVerify result = 
                JsonConvert.DeserializeObject<ZarinPalVerify>(response);

            if (result.Status is 100)
            {
                var id = 
                    await _shopping.SetFactor(factor.Id,refId:result.RefID,isPay:true);

                return RedirectToAction("PaymentResult", new { factorId = id });
            }

            return View();
        }

        return View();
    }


    public async Task<IActionResult> PaymentResult(int factorId)
    {
        var factor=await _shopping.GetFactor(factorId);

        return View(factor);
    }
}
