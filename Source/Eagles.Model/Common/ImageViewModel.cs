using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Eagle.Model
{
    public class ImageViewModel
    {
        [Required]
        public string Title { get; set; }

        public string AltText { get; set; }

        [DataType(DataType.Html)]
        public string Caption { get; set; }

        //[DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }

    public class ImageCreateViewModel
    {
        public ImageModel Image { get; set; }

        [Required]
        public string Title { get; set; }

        public string AltText { get; set; }

        [DataType(DataType.Html)]
        public string Caption { get; set; }

        //[DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }

    public class ImageListViewModel
    {
        public List<ImageModel> Images { get; set; }
    }
}
