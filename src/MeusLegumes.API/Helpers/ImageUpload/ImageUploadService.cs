namespace MeusLegumes.API.Helpers.ImageUpload;

public class ImageUploadService : IImageUploadService
{
    private readonly IWebHostEnvironment _env;

    public ImageUploadService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public void DeleteImage(string fileName, string folderPath)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), folderPath + fileName);

        if (File.Exists(path)) File.Delete(path);
    }

    public List<string> UploadMultipleImages(List<IFormFile> files, string folderPath)
    {
        List<string> uploadedFileNames = new List<string>();
        var directoryPath = Path.Combine(_env.ContentRootPath + folderPath);

        foreach (var file in files)
        {
            string fileName = Guid.NewGuid() + "_" + file.FileName;
            string filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            uploadedFileNames.Add(fileName);
        }

        return uploadedFileNames;
    }

    public string UploadImage(IFormFile file, string folderPath)
    {
        var directoryPath = Path.Combine(_env.ContentRootPath + folderPath);

        string fileName = Guid.NewGuid() + "_" + file.FileName;
        string filePath = Path.Combine(directoryPath, fileName);
        
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyToAsync(stream);
        }

        return fileName;
    }

}
