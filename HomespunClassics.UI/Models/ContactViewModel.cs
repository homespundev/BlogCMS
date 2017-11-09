using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomespunClassics.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
            ErrorMessage = "* Please enter a valid email address")]
        [Required(ErrorMessage = "* Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Subject { get; set; }
        [UIHint("MultilineText")]//This only works with @Html.EditorFor fields
        [Required(ErrorMessage = "* Required")]
        public string Message { get; set; }
    }
}