using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using twatter_postservice.DTO;
using twatter_postservice.Repositories;
using twatter_postservice.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace twatter_postservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController: Controller
    {
        private readonly PostContext _context;

        private IConfiguration _config;

        public PostController(PostContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("postmessage")]
        public async Task<ActionResult<string>> PostMessage(PostDTO postDTO)
        {
            if (!ValidateMessageLength(postDTO.PostMessage))
            {
                return "FOUT! Dit bericht is te lang.";
            }
            if (!ValidateMessageCompliance(postDTO.PostMessage))
            {
                return "FOUT! Taalgebruik!";
            }
            if(postDTO.HashTag.Length < 3)
            {
                return "FOUT! De hashtag is onwerkbaar klein.";
            }
            Post toAdd = new Post(postDTO.PostMessage, postDTO.Username, postDTO.HashTag);
            _context.Add(toAdd);
            await _context.SaveChangesAsync();
            return "geslaagd";
        }

        [HttpGet]
        [Route("getMessages/{searchterm}")]
        public async Task<ActionResult<List<PostDTO>>> FindMessages(string searchterm)
        {
            List<Post> posts = await _context.Posts.Where(b => b.UserName == searchterm || b.HashTag == searchterm).ToListAsync();
            List<PostDTO> returnPosts = convertToDTO(posts);
            return Ok(returnPosts);
        }

        [HttpGet]
        [Route("getUserMessages/{username}")]
        public async Task<ActionResult<List<PostDTO>>> FindUserMessages(string username)
        {
            List<Post> posts = await _context.Posts.Where(b => b.UserName == username).ToListAsync();
            List<PostDTO> returnPosts = convertToDTO(posts);
            return  Ok(returnPosts);
        }


        public static bool ValidateMessageLength(string message)
        {
            if(message.Length > 300)
            {
                return false;
            }
            return true;
        }

        public static bool ValidateMessageCompliance(string message)
        {
            if(message.Contains("fuck") || message.Contains("Hitler") || message.Contains("Order 66"))
            {
                return false;
            }
            return true;
        }

        public static List<PostDTO> convertToDTO(ICollection<Post> posts)
        {
            List<PostDTO> toReturn = new List<PostDTO>();
            foreach (Post post in posts)
            {
                PostDTO convert = new PostDTO(post.UserName, post.PostMessage, post.HashTag);
                toReturn.Add(convert);
            }
            return toReturn;
        }
    }
}
