using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    //new to declare a partial class of what we are validating
    [MetadataType(typeof(PostValidation))]
    public partial class Post
    {
 
    }

    public class PostValidation
    {
        [Required, DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(150)]
        public string ImageURL { get; set; }
    }
}