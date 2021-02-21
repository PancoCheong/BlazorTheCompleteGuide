//Business.Repository.HotelRoomImageRepository.cs
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomImageRepository : IHotelRoomImageRepository
    {
        //using dependency injection to extract the DbContext
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            //convert from DTO to data model
            var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            await _db.HotelRoomImages.AddAsync(image);      //add to database
            return await _db.SaveChangesAsync();            // save changes (ie. commit)
        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            // search image from database by Image ID - single record
            var image = await _db.HotelRoomImages.FindAsync(imageId); 
            _db.HotelRoomImages.Remove(image);              // remove from database 
            return await _db.SaveChangesAsync();            // save changes (ie. commit)
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            if (allImages == null) { return 0; }  //if no image record in database (ie. just uploaded, then click delete 1st one, and then update), just skip the removal on DB   
            _db.HotelRoomImages.Remove(allImages);          // remove from database 
            return await _db.SaveChangesAsync();            // save changes (ie. commit)
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            // search image from database by Room ID, mutliple records
            // Where condition in Linq, ToListAsync - EF Core 
            var imageList = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(imageList);     // remove from database 
            return await _db.SaveChangesAsync();            // save changes (ie. commit)
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetAllHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>,IEnumerable<HotelRoomImageDTO>>(
                await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
