using Posts.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Posts.Model
{
    [Serializable]
    public class Post : IText
    {
        public int Id { get; set; }

        [Required]
        public string UserLogin { get; set; }

        [MinLength(10)]
        public string Content { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
