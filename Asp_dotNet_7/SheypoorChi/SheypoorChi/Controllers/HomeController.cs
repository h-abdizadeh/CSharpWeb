using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SheypoorChi.Core.Interface;
using SheypoorChi.Core.ViewModels;
using SheypoorChi.DataLayer.Migrations;
using SheypoorChi.DataLayer.Models;
using System.Xml;

namespace ShepoorChi.Controllers;

public class HomeController : Controller
{
    IAdmin _admin;
    IShopping _shopping;

    public HomeController(IAdmin admin, IShopping shopping)
    {
        _admin = admin;
        _shopping = shopping;
    }

    public async Task<IActionResult> Index()
    {
        //get all groups
        var groups = await _admin.GetGroups();
        //filter groups (notShow false)
        //ViewBag.Groups = groups.Where(g => g.NotShow == false);
        //ViewBag.Groups = groups.Where(g => g.NotShow is false);
        ViewBag.Groups = groups.Where(g => !g.NotShow);
        var products = await _admin.GetProducts(notShow: false);

        return View(products);
    }

    public async Task<IActionResult> ProductInfo(int id)//id -> productId
    {
        var product = await _admin.GetProduct(id);

        //redirect to index if not found product
        if (product is null) return RedirectToAction(nameof(Index));

        ViewBag.Related =
            await _admin.GetProducts(groupId: product.GroupId, productId: product.Id);

        ViewBag.InCart = false;
        //add shopping viewmodel
        var addShopping = new AddShoppingVM();
        if (User.Identity.IsAuthenticated)
        {
            var userId = (await _admin.GetUser(User.Identity.Name)).Id;

            //check product in user cart
            var userFactor = await _shopping.GetFactor(userId);
            if (userFactor is not null)
            {
                ViewBag.InCart =
                    userFactor.Details.Select(f => f.ProductId).Contains(product.Id);
            }


            addShopping.UserId = userId;
            addShopping.ProductId = product.Id;
            addShopping.ShoppingCount = 1;
        }

        //two model (product , addShopping) in tuple
        var productShopping = (product, addShopping);

        //return View(product);
        return View(productShopping);
    }

    public async Task<IActionResult> Products(int id)//id -> groupId
    {
        var products = await _admin.GetProducts(id);

        if (products is null) return RedirectToAction(nameof(Index));

        ViewBag.groupName = (await _admin.GetGroup(id)).GroupName;
        return View(products);

    }


    public async Task<IActionResult> Search(string search)
    {
        ViewBag.Search = search;

        if (string.IsNullOrEmpty(search))
            return RedirectToAction(nameof(Index));

        var products = await _admin.GetProducts(search);


        //return View(products);
        //don't creat view -> return data to another View(viewName,data)
        return View("products", products);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToShoppingCart(AddShoppingVM shopping)
    {
        var factor = await _shopping.AddFactor(shopping);

        if (factor is null) return RedirectToAction(nameof(Index));

        return RedirectToAction(nameof(ShoppingCart), new { userId = factor.UserId });
    }


    public async Task<IActionResult> ShoppingCart(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            var user = User.Identity.IsAuthenticated;
            if (user)
            {
                var id = (await _admin.GetUser(User.Identity.Name)).Id;
                var cart = await _shopping.GetFactor(id);

                return View(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        var shoppingCart = await _shopping.GetFactor(userId);

        return View(shoppingCart);
    }

    public async Task<IActionResult> PreShoppingPay(int factorId)
    {
        var iran = new XmlDocument();
        iran.Load("wwwroot/files/irancities.xml");

        var cities = iran.SelectNodes("/iran/city");

        var cityList = new List<CitiesVM>();
        foreach (XmlNode item in cities)
        {
            var city = new CitiesVM()
            {
                Province = item["province_name"].InnerXml,
                City = item["city_name"].InnerXml
            };
            cityList.Add(city);
        }

        ViewBag.CityList = cityList;
        var user = await _admin.GetUser(User.Identity.Name);

        if (user.UserInfo is not null)
            return View(user.UserInfo);//userDetail

        var tmpUser = new UserInfo()
        {
            UserId = user.Id,
        };
        return View(tmpUser);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> PreShoppingPay(UserInfo userDetail)
    {
        var result = await _admin.SetUserDetail(userDetail);

        if (result)
        {
            //set factor price 
            var factorId = await _shopping.SetFactor(userDetail.UserId);
            if (factorId is 0) return RedirectToAction(nameof(Index));

            //redirect to payment method
            return RedirectToAction("RequestPaymentGate",
                                    "payment",
                                    new { id = factorId });
        }

        return View(userDetail);
    }

}
