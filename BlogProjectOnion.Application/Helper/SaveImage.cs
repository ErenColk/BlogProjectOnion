using BlogProjectOnion.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Helper
{
    public static class SaveImage
    {
        public static void ImagePath(string uploadPath, IUploadImagePath uploadImage)
        {

            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                uploadImage.UploadPath.CopyTo(stream);
            }

        }


    }
}
