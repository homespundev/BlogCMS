using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomespunClassics.DATA
{
    class PostMetadata
    {
        //public int PostId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(250, ErrorMessage = "* Cannot exceed 250 characters")]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(1000, ErrorMessage = "* Cannot exceed 1000 characters")]
        [UIHint("MultilineText")]
        [Display(Name = "Description")]
        public string PostDescription { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "* Required")]
        [UIHint("MultilineText")]
        [Display(Name = "Body")]
        public string PostBody { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Author ID")]
        public int PostAuthorID { get; set; }

        [Display(Name = "Category ID")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public Nullable<int> CategoryID { get; set; }
        //public bool Published { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0: dddd, MMMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateCreated { get; set; }
    }
    [MetadataType(typeof(PostMetadata))]
    public partial class Post { }
}
