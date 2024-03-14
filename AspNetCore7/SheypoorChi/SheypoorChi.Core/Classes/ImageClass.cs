
namespace SheypoorChi.Core.Classes;

public class ImageClass
{
    string groupPath = "wwwroot/images/groups/";
    public async Task<string> CreateSvg(string svgCode, string fileName)
    {
        //string svgFileName = fileName + ".svg";
        string svgFileName = $"{fileName}.svg";
        string savePath = "wwwroot/Images/groups/";

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fileAddress =
            Path.Combine(savePath, svgFileName);

        await File.WriteAllTextAsync(fileAddress, svgCode);

        return svgFileName;
    }

    public async Task<string> GetSvgCode(string fileName)
    {
        string fileAddress = "wwwroot/images/groups/" + fileName;

        if (File.Exists(fileAddress))
            return await File.ReadAllTextAsync(fileAddress);

        return string.Empty;
    }

    public async Task<string> EditSvg(string svgCode,
                                        string filename,
                                          string oldFilename)
    {
        string svgFilename = $"{filename}.svg";

        if (oldFilename != svgFilename &&
            File.Exists(groupPath + oldFilename))
        {
            File.Delete(groupPath + oldFilename);
        }

        string fileAddress =
            Path.Combine(groupPath, svgFilename);

        await File.WriteAllTextAsync(fileAddress, svgCode);

        return svgFilename;

    }


    public void DeleteSvg(string fileName)
    {
        if (File.Exists(groupPath+fileName))
        {
            File.Delete(groupPath + fileName);
        }
    }
}
