using System.Security.Cryptography;
using System.Text;

namespace FardaOnlineShop.Classes;

public class AdminClass
{
    string saveAddress = Path.Combine("wwwroot/img/products");
    public async Task<string> SaveProductImg(IFormFile img)
    {
        //image name
        var imgName = Guid.NewGuid() + ".png";

        //directory (create if not exists)
        if (!Directory.Exists(saveAddress))
            Directory.CreateDirectory(saveAddress);

        var saveImg = Path.Combine(saveAddress, imgName);

        //upload image
        using (Stream imgFile = new FileStream(saveImg, FileMode.Create))
        {
            await img.CopyToAsync(imgFile);
        }

        return imgName;
    }

    public void DeleteProductImage(string imgName)
    {
        var imgFile = Path.Combine(saveAddress, imgName);
        if (File.Exists(imgFile))
            File.Delete(imgFile);
    }


    public string HashPassword(string password)
    {
        byte[] passBytes = ASCIIEncoding.Default.GetBytes(password);
        byte[] md5Pass = MD5.Create().ComputeHash(passBytes);

        return BitConverter.ToString(md5Pass);
    }
}
