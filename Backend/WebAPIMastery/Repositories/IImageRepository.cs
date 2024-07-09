using System.Net;
using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
