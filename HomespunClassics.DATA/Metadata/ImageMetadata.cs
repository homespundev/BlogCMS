using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomespunClassics.DATA.Metadata
{
    class ImgMetadata
    {
        //public int ImageID { get; set; }
        [StringLength(100, ErrorMessage = "* Not to exceed 100 characters")]
        [Display(Name ="Description")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string ImageDescription { get; set; }
        [Required (ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage ="* Not to exceed 100 characters")]
        [Display(Name = "File Name")]
        public string ImageFileName { get; set; }
    }
    [MetadataType(typeof(ImgMetadata))]
    public partial class Image { }
}
