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
using TinyERP4Fun.ModelServiceInterfaces;

namespace TinyERP4Fun.ModelServises
{
    public class GeneralService: IGeneralService
    {
        private readonly DefaultContext _context;
        public GeneralService(DefaultContext context)
        {
            _context = context;
        }

        public async Task<T> GetObject<T>(long? id) where T : class, IHaveLongId
        {
            if (id == null) return null;

            T resultObject = await _context.Set<T>().SingleOrDefaultAsync(m => m.Id == id);

            if (resultObject == null) return null;

            return resultObject;
        }
        public void AddImage<T>(ref T entity, IList<IFormFile> files) where T:IHaveImage
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null)
            {
                entity.Image = null;
                entity.ContentType = null;
            }
            else if (uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {

                MemoryStream ms = new MemoryStream();
                uploadedImage.OpenReadStream().CopyTo(ms);
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                {
                    if (image.Width > Constants.maxImageSize || image.Height > Constants.maxImageSize)
                        throw new BadImageFormatException("Max image size is " + Constants.maxImageSize.ToString() + "x" + Constants.maxImageSize.ToString()+"!");
                    entity.Image = ms.ToArray();
                    entity.ContentType = uploadedImage.ContentType;
                }
            }
        }
        public FileStreamResult GetImage<T>(long? id) where T : class, IHaveLongId, IHaveImage
        {
            var entityTask = GetObject<T>(id);
            Task.WaitAll(entityTask);
            if (entityTask.Result == null || entityTask.Result.Image == null) return null;
            MemoryStream ms = new MemoryStream(entityTask.Result.Image);
            return new FileStreamResult(ms, entityTask.Result.ContentType);
        }

    }
}
