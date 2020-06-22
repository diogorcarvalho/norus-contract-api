using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorusContract.Api.Models;

namespace NorusContract.Api.Controller
{
  [ApiController]
  public class FileUploadController : ControllerBase
  {
    [HttpPost, Authorize, Route("v1/fileupload/pdf")]
    public async Task<ActionResult<IFileUploadResponse>> PostFile(IFormFile file)
    {
      IFileUploadResponse response;

      var fileName = Guid.NewGuid().ToString() + GetExtension(file.ContentType);

      var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs", fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      response = new IFileUploadResponse
      {
        FileName = file.FileName,
        Size = file.Length,
        Url = $"http://localhost:5000/pdfs/{fileName}"
      };
      
      return Ok(response);
    }

    private string GetExtension(string value)
    {
      if (value == "application/pdf") return ".pdf";
      if (value == "image/png") return ".png";
      if (value == "image/jpg") return ".jpg";
      if (value == "image/jpeg") return ".jpeg";
      return ".tmp";
    }
  }
}