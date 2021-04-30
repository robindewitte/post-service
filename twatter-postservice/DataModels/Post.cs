using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace twatter_postservice.DataModels
{
    public class Post
    {
        #region properties
        [Key]
        public Guid Id { get; set; }
        public string PostMessage { get; set; }
        public string UserName { get; set; }
        public string HashTag { get; set; }
        #endregion

        #region constructors
        public Post()
        {

        }
        public Post(string postMessage, string userName, string hashTag)
        {
            PostMessage = postMessage;
            UserName = userName;
            HashTag = hashTag;
        }
        #endregion
    }
}
