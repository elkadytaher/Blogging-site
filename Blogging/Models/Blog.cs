//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blogging.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Blog
    {
        public int blog1 { get; set; }

        public Nullable<int> blogerid { get; set; }
        public Nullable<System.DateTime> blog_date { get; set; }
        [Display(Name = "Blog")]

        public string blog_content { get; set; }
        [Display(Name = "Image")]

        public string blog_image { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is requierd")]

        public string blog_title { get; set; }

        public virtual bloger bloger { get; set; }
    }
}
