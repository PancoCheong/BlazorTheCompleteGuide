// Business
// Repository\IRepository\IHotelRoomRepository.cs
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId);
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms();
        public Task<HotelRoomDTO> IsSameNameRoomAlreadyPresent(string name);
        //public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
    }
}
