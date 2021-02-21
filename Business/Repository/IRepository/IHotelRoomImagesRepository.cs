// Business.Repository.IRepository.IHotelRoomImageRepository.cs
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomImageRepository
    {
        // just return status, not the image itself
        public Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO);
        public Task<int> DeleteHotelRoomImageByImageId(int imageId);
        public Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl);
        public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
        public Task<IEnumerable<HotelRoomImageDTO>> GetAllHotelRoomImages(int roomId);
    }
}
