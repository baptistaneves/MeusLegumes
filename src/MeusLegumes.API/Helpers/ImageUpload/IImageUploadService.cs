namespace MeusLegumes.API.Helpers.ImageUpload;

public interface IImageUploadService
{
    List<string> UploadMultipleImages(List<IFormFile> files, string folderPath);
    string UploadImage(IFormFile file, string folderPath);
    void DeleteImage(string fileName, string folderPath);
}
