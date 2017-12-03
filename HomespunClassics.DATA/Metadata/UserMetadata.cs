﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomespunClassics.DATA
{
    public class UserMetadata
    {
        //public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
            ErrorMessage = "* Please enter a valid email address")]
        [StringLength(256, ErrorMessage = "* Email must not exceed 256 characters")]
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        //public bool LockoutEnabled { get; set; }
        // public int AccessFailedCount { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; }
    }
    [MetadataType(typeof(UserMetadata))]
    public partial class AspNetUser {
        [Display(Name = "Name")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}