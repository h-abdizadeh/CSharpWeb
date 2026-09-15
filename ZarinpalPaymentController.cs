using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourApp.Controllers
{
    public class ZarinpalSandboxController : Controller
    {
        private const string SandboxBaseUrl = "https://sandbox.zarinpal.com/pg/rest/WebGate/";

        // در عمل بهتر است از IConfiguration برای خواندن مقدار استفاده کنید
        private const string MerchantId = "00000000-0000-0000-0000-000000000000"; // UUID تستی دلخواه

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentRequestModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            // آدرس بازگشت (Callback)
            var callbackUrl = Url.Action(
                nameof(Verify),
                "ZarinpalSandbox",
                new { amount = model.Amount },
                Request.Scheme);

            using var http = new HttpClient();

            var requestBody = new
            {
                merchant_id = MerchantId,
                amount = model.Amount,
                description = model.Description,
                callback_url = callbackUrl,
                email = model.Email,
                mobile = model.Mobile
            };

            var response = await http.PostAsJsonAsync(
                SandboxBaseUrl + "PaymentRequest.json",
                requestBody);

            if (!response.IsSuccessStatusCode)
                return BadRequest("خطا در ارتباط با زرین‌پال (سندباکس).");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var status = root.GetProperty("Status").GetInt32();
            var authority = root.GetProperty("Authority").GetString();

            // طبق مستندات، وضعیت 100 یعنی موفق 
            if (status == 100 && !string.IsNullOrEmpty(authority))
            {
                // هدایت کاربر به صفحه پرداخت سندباکس
                var paymentUrl = $"https://sandbox.zarinpal.com/pg/StartPay/{authority}";
                return Redirect(paymentUrl);
            }

            return BadRequest($"درخواست پرداخت ناموفق بود. Status = {status}");
        }

        [HttpGet]
        public async Task<IActionResult> Verify(int amount, string Authority, string Status)
        {
            // اگر کاربر پرداخت را لغو کرده باشد
            if (Status == "NOK")
                return View("Error");

            using var http = new HttpClient();

            var requestBody = new
            {
                merchant_id = MerchantId,
                amount = amount,
                authority = Authority
            };

            var response = await http.PostAsJsonAsync(
                SandboxBaseUrl + "PaymentVerification.json",
                requestBody);

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var status = root.GetProperty("Status").GetInt32();
            var refId = root.GetProperty("RefID").GetInt64();

            if (status != 100)
                return View("Error");

            // اینجا می‌توانید refId را در دیتابیس ذخیره کنید و به کاربر نمایش دهید
            ViewBag.RefId = refId;
            ViewBag.Amount = amount;

            return View("Success");
        }
    }
}
