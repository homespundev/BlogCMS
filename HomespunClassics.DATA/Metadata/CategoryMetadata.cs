using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomespunClassics.DATA
{
    class CategoryMetadata
    {
        //public int CategoryID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* Max Length 50 characters")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
}
