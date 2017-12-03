using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HomespunClassics.DATA
{
    class TagMetadata
    {
        //public int TagID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(250, ErrorMessage = "* Cannot exceed 50 characters")]
        [Display(Name = "Name")]
        public string TagName { get; set; }
    }

    [MetadataType(typeof(TagMetadata))]
    public partial class Tag { }

}
