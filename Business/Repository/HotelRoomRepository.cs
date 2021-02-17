// Business
// Repository\IHotelRoomRepository.cs
using Business.Repository.IRepository;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        //using dependency injection to extract the DbContext
        private readonly ApplicationDbContext _db;

        public HotelRoomRepository(ApplicationDbContext db)
        {
            _db = db;             
        }
        
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var addedHotelRoom = _db.HotelRooms.Add(hotelRoomDTO); //how to convert DTO to model object, use Auto-mapper

        }

        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            throw new NotImplementedException();
        }

        public Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<HotelRoomDTO> IsSameNameRoomAlreadyPresent(string name)
        {
            throw new NotImplementedException();
        }

        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            throw new NotImplementedException();
        }
    }
}
