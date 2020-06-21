namespace NorusContract.Api.Models
{
  public class IFileUploadResponse
  {
    public string FileName { get; set; }
    public long Size { get; set; }
    public string Url { get; set; }
  }
}