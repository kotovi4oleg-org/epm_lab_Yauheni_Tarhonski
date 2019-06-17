using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;

namespace TinyERP4Fun.ModelServises
{
    public static class ServicesCommonFunctions
    {
        public static void AddImage<T>(ref T entity, IList<IFormFile> files) where T : IHaveImage
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null)
            {
                entity.Image = null;
                entity.ContentType = null;
            }
            else if (uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var ors = uploadedImage.OpenReadStream())
                        ors.CopyTo(ms);

                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                    {
                        if (image.Width > Constants.maxImageSize || image.Height > Constants.maxImageSize)
                            throw new BadImageFormatException("Max image size is " + Constants.maxImageSize.ToString() + "x" + Constants.maxImageSize.ToString() + "!");
                        entity.Image = ms.ToArray();
                        entity.ContentType = uploadedImage.ContentType;
                    }
                }
            }
        }
        public static FileStreamResult GetImage<T>(T entity) where T : class, IHaveLongId, IHaveImage
        {
            if (entity == null || entity.Image == null) return null;
            MemoryStream ms = new MemoryStream(entity.Image);
            return new FileStreamResult(ms, entity.ContentType); 
        }
        public static async Task<T> GetObject<T>(long? id, DefaultContext _context, bool tracking = false) where T : class, IHaveLongId
        {
            if (id == null) return null;
            T resultObject;
            if (tracking)
                resultObject = await _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            if (resultObject == null) return null;
            return resultObject;
        }
        public static async Task AddObject<T>(T entity, DefaultContext _context) where T : class, IHaveLongId
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<bool> UpdateObject<T>(T entity, DefaultContext _context) where T : class, IHaveLongId
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists<T>(entity.Id, _context)) return false;
                throw;
            }
        }
        public static async Task DeleteObject<T>(long id, DefaultContext _context) where T : class, IHaveLongId
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        private static bool EntityExists<T>(long id, DefaultContext _context) where T : class, IHaveLongId
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
    }
}
