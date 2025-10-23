namespace PropertyManagement.Application.DTOs;

public class ImageDto
{
    public string IdPropertyImage { get; set; }
    public string File { get; set; }

    public ImageDto(string idPropertyImage, string file)
    {
        IdPropertyImage = idPropertyImage;
        File = file;
    }
}