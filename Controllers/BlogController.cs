using API_Assignment.BAL.IServices;
using API_Assignment.DAL.DBContext;
using API_Assignment.DAL.Entity;
using API_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        BlogContext _bContext;
        IPostService _postService;
        IUserService _userService;
        public BlogController(IUserService us, IPostService ps, BlogContext bContext)
        {
            // _eContext= ec;
            _userService = us;
            _postService = ps;
            _bContext = bContext;
        }


        //Endpoint to create a new user.
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                await _userService.CreateUser(user); // Assuming _userService handles user creation logic
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                // Handle potential exceptions during user creation
                return StatusCode(500, ex.Message);
            }
        }



        //Endpoint to create a new post
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(PostModel post)
        {
            if (post == null)
            {
                return BadRequest("Invalid post data.");
            }

            try
            {
                await _postService.CreatePost(post); // Assuming _postService handles post creation logic
                return Ok("Post created successfully.");
            }
            catch (Exception ex)
            {
                // Handle potential exceptions during post creation
                return StatusCode(500, ex.Message);
            }
        }


        //Endpoint to get all users.
        [HttpGet("GetUserDetails")]
        public List<User> GetUserDetails()
        {
            List<User> users = _bContext.Users.ToList();
            return users;
        }



        //Endpoint to get all published posts.
        [HttpGet("GetPostDetails")]
        public List<Post> GetPostDetails()
        {
            List<Post> posts = _bContext.Posts.Where(p => p.IsPublished == true).ToList();
            return posts;
        }



        //Endpoint to get a published post by ID
        [HttpGet("GetPostDetailsById/{id}")]
        public IActionResult GetPost(int id)
        {
            Post post = _bContext.Posts
                .Where(p => p.Id == id && p.IsPublished == true)
                .SingleOrDefault();

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            return Ok(post);
        }




        //Endpoint to get a published post of an active user by ID.
        [HttpGet("GetActivePostDetailsById/{id}")]
        public Post GetActivePost(int id)
        {
            var post = (from p in _bContext.Posts
                        join u in _bContext.Users on p.CreatedBy equals u.Id
                        where p.Id == id && u.IsActive == true
                        select p).SingleOrDefault();

            return post;

        }

        

        //Endpoint to get published posts by category.
        [HttpGet("GetPostsByCategory/{Category}")]
        public List<Post> GetPostsByCategory(int Category)
        {
            List<Post> posts = _bContext.Posts
                .Where(e => e.Category == Category)
                .ToList();
            return posts;
        }



        // Endpoint to update user details

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid user data.");
            }

            var user = _bContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = model.name;
            user.Password = model.password;

            _bContext.SaveChanges();

            return Ok("User updated successfully.");
        }



       // Endpoint to update post details
       [HttpPut("UpdatePost/{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostModel post)
        {
            if (post == null)
            {
                return BadRequest("Invalid post data.");
            }

            var existingPost = await _bContext.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (existingPost == null)
            {
                return NotFound("Post not found.");
            }

            // Update properties (consider using AutoMapper for mapping)
            existingPost.Title = post.title;
            existingPost.Description = post.description;

            // ... Update other properties as needed

            await _bContext.SaveChangesAsync();

            return Ok("Post updated successfully.");
        }


        //Endpoint to delete a user (soft delete by setting ‘is_active’ as false).
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _bContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.IsActive = false; // Soft delete by setting IsActive to false

            await _bContext.SaveChangesAsync();

            return Ok("User deleted successfully.");
        }



        //Endpoint to delete a post (soft delete by setting ‘is_published’ as false).
        [HttpDelete("DeletePost/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _bContext.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            post.IsPublished = false; // Soft delete by setting IsPublished to false

            await _bContext.SaveChangesAsync();

            return Ok("Post deleted successfully.");
        }











    }
}

