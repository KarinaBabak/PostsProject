using Posts.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Posts.Model
{
    [Serializable]
    public class Comment : IText
    {
        public int Id { get; set; }

        [Required]
        public string UserLogin { get; set; }

        [Required]
        public int PostId { get; set; }

        [MinLength(2)]
        public string Content { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
