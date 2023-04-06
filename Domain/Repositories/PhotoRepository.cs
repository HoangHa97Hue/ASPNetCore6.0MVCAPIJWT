//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Service.Contexts;
//using Service.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace Service.Repositories
//{
//    public class PhotoService : IPhotoService
//    {
//        private readonly MyDbContext _dbContext;

//        public PhotoService(MyDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public Photo CreatePhoto(Photo photo)
//        {
//            try
//            {
//                _dbContext.Photos.Add(photo);

//                var result = _dbContext.SaveChanges();

//                if (result > 0)
//                {
//                    return photo;
//                }
//                return new Photo();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new Photo();
//            }
//        }

//        public void Delete(Photo photo)
//        {
//            try
//            {
//                _dbContext.Photos.Remove(photo);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        public void DeletePhoto(string id)
//        {
//            var photo = GetPhoto(id);
//            try
//            {
//                _dbContext.Photos.Remove(photo);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        public void EditPhoto(Photo photo)
//        {
//            _dbContext.Photos.Update(photo);

//            var result = _dbContext.SaveChanges();
//        }

//        public Photo GetPhoto(string id)
//        {
//            try
//            {
//                return _dbContext.Photos.Single(i => i.ID == id);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new Photo();
//            }
//        }

//    }
//}
