using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Posts.Application.Interface;
using Posts.Model;
using System.Web.Http.Description;
using System.Web;

namespace Posts.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        /// <summary>
        /// Gets comments of the post asynchronously.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/post/{id}/comments")]
        [ResponseType(typeof(IEnumerable<Comment>))]
        public async Task<IHttpActionResult> GetPostComments(int postId)
        {
            var result = await _commentService.GetByPostId(postId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates comment to post asynchronously.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/post/{id}/comment")]
        public async Task<IHttpActionResult> CreateCommentAsync(int postId, [FromBody]Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.LastUpdatedDate = DateTime.Now;
            comment.PostId = postId;
            await _commentService.Create(comment);
            return Ok();
        }

        /// <summary>
        /// Updates the comment asynchronously.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns> The result of update operation. </returns>
        [HttpPut]
        [Route("api/post/{id}/comment/{id}")]
        public async Task<IHttpActionResult> UpdateCommentAsync([FromBody]Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            comment.LastUpdatedDate = DateTime.Now;
            await _commentService.Update(comment);

            return Ok();
        }

        [HttpDelete]
        [Route("api/post/{id}/comment")]
        public async Task<IHttpActionResult> DeleteComment([FromBody]Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _commentService.Delete(comment);
            return Ok();
        }
    }
}
