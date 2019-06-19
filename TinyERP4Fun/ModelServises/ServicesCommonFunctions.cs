using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinyERP4Fun.Data;
using TinyERP4Fun.Models;

namespace TinyERP4Fun.ModelServises
{
    public static class ServicesCommonFunctions
    {
        internal static async Task<string> SendGetRequestAsync(Uri url)
        {
            string result;
            using (var content = new MemoryStream())
            {
                try //http
                {
                    var webReq = WebRequest.Create(url);
                    Task<WebResponse> responseTask = webReq.GetResponseAsync();
                    using (WebResponse response = await responseTask)
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            await responseStream.CopyToAsync(content);
                        }
                    }
                }
                catch //https
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var webReq = WebRequest.Create(url);
                    Task<WebResponse> responseTask = webReq.GetResponseAsync();
                    using (WebResponse response = await responseTask)
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            await responseStream.CopyToAsync(content);
                        }
                    }
                }
                result = Encoding.ASCII.GetString(content.ToArray());
            }

            return result;
        }
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
        public static async Task<IEnumerable<T>> GetListAsync<T>(DefaultContext _context) where T : class
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public static IQueryable<T> GetIQueryable<T>(DefaultContext _context) where T : class
        {
            return _context.Set<T>().AsNoTracking();
        }

        public static async Task<T> GetObject<T>(long? id, DefaultContext _context, bool tracking = false) where T : class, IHaveLongId
        {
            if (id == null) return null;
            T resultObject;
            if (tracking)
                resultObject = await _context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);
            else
                resultObject = await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
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
        public static bool EntityExists<T>(long id, DefaultContext _context) where T : class, IHaveLongId
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
        internal static SelectList AddFirstItem(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = null, Text = "" });
            return new SelectList(_list, "Value", "Text");
        }
    }
}
