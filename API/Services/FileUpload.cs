 using System;

namespace API.Services;

public class FileUpload
{
  public static async Task<string> Upload(IFormFile file)
  {
    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

    if (!Directory.Exists(uploadFolder))
    {
      Directory.CreateDirectory(uploadFolder);
    }

    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

    var filepath = Path.Combine(uploadFolder, fileName);

    await using var stream = new FileStream(filepath, FileMode.Create);
    await file.CopyToAsync(stream);
    return fileName;
  }
}
