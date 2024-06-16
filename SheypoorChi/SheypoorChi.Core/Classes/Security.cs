using System.Security.Cryptography;
using System.Text;

namespace SheypoorChi.Core.Classes;

public class Security
{
    public async Task<string> GetHash(string str)
    {
        MD5 md5 = MD5.Create();
        byte[] baseInput = ASCIIEncoding.Default.GetBytes(str);
        byte[] hashInput = md5.ComputeHash(baseInput);

        var hashStr = Convert.ToBase64String(hashInput);

        return await Task.FromResult(hashStr);
    }

}
