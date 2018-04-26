using Posts.Application.Interface;
using Posts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Posts.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _postService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _postService.GetAllPosts();
            return Ok(result);
        }

        /// <summary>
        /// Saves the new post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SavePost")]
        public async Task<IHttpActionResult> Create([FromBody] Post model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            var newPostId = await _postService.Create(model);
            return CreatedAtRoute("Get", new { Controller = "Post", id = newPostId }, model);
        }

        /// <summary>
        /// Updates the Post.
        /// </summary>
        /// <param name="id">id of entity to be updated</param>
        /// <param name="formData"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody]Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsPostExist(post.Id).Result)
            {
                return NotFound();
            }

            post.LastUpdatedDate = DateTime.Now;
            await _postService.Update(post);

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePost([FromBody]Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsPostExist(post.Id).Result)
            {
                return NotFound();
            }

            await _postService.Delete(post);
            return Ok();
        }

        private async Task<bool> IsPostExist(int id)
        {
            return await _postService.GetById(id) == null ? false : true;
        }

    }
}
