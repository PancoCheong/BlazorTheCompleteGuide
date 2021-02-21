// Business.Repository.IHotelRoomRepository.cs
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        //using dependency injection to extract the DbContext
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;             
        }
        
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
                hotelRoom.CreatedDate = DateTime.Now;
                //TODO: assign username when we got the login
                hotelRoom.CreatedBy = "";
                var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom); //how to convert DTO to model object, use Auto-mapper
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity); // .Entity to ensure it get HotelRoom Object, then covert back to DTO.
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId) 
        {
            try
            {
                var roomDetails = await _db.HotelRooms.FindAsync(roomId);
                if (roomDetails != null)
                {
                    //delete all images
                    var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                    foreach(var image in allImages)
                    {
                        if(File.Exists(image.RoomImageUrl))
                        {
                            File.Delete(image.RoomImageUrl);
                        }
                    }
                    _db.HotelRoomImages.RemoveRange(allImages);

                    _db.HotelRooms.Remove(roomDetails);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = 
                    _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                    (_db.HotelRooms.Include(x => x.HotelRoomImages)); //Eagar loading - .Include(x=>x.HotelRoomImages)
                return hotelRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
        {
            try
            {   //.Include(x=>x.HotelRoomImages) - Eagar loading all children entities
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x=>x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
                    //await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId));
                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // if unique, returns null, else returns the room object (means duplicated room)
        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
               if (roomId == 0)
               {
                    // create a brand new record (ie. Create)
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower())); //return null if not found
                    return hotelRoom;
                }
                else
                {
                    // updating record is existed in DB, check if the Room ID is the same
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                        await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id !=roomId)); //hotelRoomDTO.Id is not equal roomId, means duplicated room
                    return hotelRoom;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    // have to explicitly define object type for automapper to work correctly
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = ""; //TODO: assign username once we got the login
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room); //object type is Entity
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
