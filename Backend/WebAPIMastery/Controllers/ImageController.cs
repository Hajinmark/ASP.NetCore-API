using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMastery.Models.Domain;
using WebAPIMastery.Models.DTO;
using WebAPIMastery.Repositories;

namespace WebAPIMastery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository; 
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm]ImageDTO imageRequest)
        {
            ValidateFileUpload(imageRequest);

            if(ModelState.IsValid)
            {
                var imageModel = new Image()
                {
                   File = imageRequest.File,
                   FileExtension = Path.GetExtension(imageRequest.File.FileName),
                   FileSizeInBytes = imageRequest.File.Length,
                   FileName = imageRequest.FileName,
                   FileDescription = imageRequest.FileDescription
                };

                var image = await imageRepository.Upload(imageModel);
                return Ok(image);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageDTO imageRequest)
        {

            var allowedFileExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if(allowedFileExtension.Contains(Path.GetExtension(imageRequest.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(imageRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10 mb, please upload smaller size file.");
            }


        }

    }
}
