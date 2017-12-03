using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomespunClassics.DATA
{
    public class CommentMetadata
    {
        //public int CommentID { get; set; }

        //public int UserID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "* Required")]
        [UIHint("MultilineText")]
        [StringLength(1000, ErrorMessage = "* Cannot exceed 1000 characters")]
        public string CommentBody { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "* Cannot exceed 100 characters")]
        public string CommentTitle { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Post ID" )]
        public int PostID { get; set; }
    }
    //[MetadataType(typeof(CommentMetadata))]
    //public partial class Comment { }
}
