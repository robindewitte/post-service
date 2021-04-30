using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twatter_postservice.DTO
{
    public class PostDTO
    {
        #region fields
        private string username;
        private string postMessage;
        private string hashTag;
        #endregion

        #region constructors
        //empty constructor for JSON
        public PostDTO()
        {

        }

        public PostDTO(string username, string postMessage, string hashtag)
        {
            this.Username = username;
            this.PostMessage = postMessage;
        }
        #endregion

        #region properties

        public string Username { get => username; set => username = value; }
        public string PostMessage { get => postMessage; set => postMessage = value; }
        public string HashTag { get => hashTag; set => hashTag = value; }
        #endregion
    }
}
