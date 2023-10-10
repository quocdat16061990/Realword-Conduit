
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatDotnetConduit.Application.Users.Commands;
using DatDotnetConduit.Application.Users.Queries;
using DatDotnetConduit.Application.Articles.Queries;
using DatDotnetConduit.Application.Common.Articles.Commands;
using DatDotnetConduit.Application.Common.Articles.Queries;
using DatDotnetConduit.Application.Articles.Commands;

namespace DatDotnetConduit.API.Controllers
{

    public class ArticleController : BaseController
    {
        public ArticleController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalArticle([FromQuery] ArticleGlobalQuery query, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetAnArticle(string slug, CancellationToken cancellationToken)
        {
            var query = new GetAnArticleQuery { Slug = slug };
            var data = await _mediator.Send(query, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> AddArticle(AddArticleCommand comamnd, CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(comamnd, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpPut("{slug}")]
        public async Task<IActionResult> UpdateArticle(string slug, UpdateArticleRequest body , CancellationToken cancellationToken)
        {
           
            var data = await _mediator.Send(new UpdateArticleCommand { Slug = slug , Body = body}, cancellationToken);
            return Ok(data);
        }

        [Authorize]
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeleteArticle(string slug  , CancellationToken cancellationToken)
        {
            var command = new DeleteArticleCommand { Slug = slug };
            var data = await _mediator.Send(command, cancellationToken);
            return Ok(data);

        }

        [Authorize]
        [HttpPost("{slug}/comments")]
        public async Task<IActionResult> AddComment (string slug, AddCommentRequest body , CancellationToken cancellationToken)
        {
            var data = await _mediator.Send(new AddCommentCommand { Slug = slug , Comment = body}, cancellationToken);
            return Ok(data);
        }
        [HttpGet("{slug}/comments")]
        public async Task<IActionResult> GetCommentQuery (string slug , CancellationToken cancellationToken)
        {
            var data =await _mediator.Send(new GetCommentQuery { Slug = slug }, cancellationToken);
            return Ok(data);
        }
        [Authorize]
        [HttpDelete("{slug}/comments/{id}")]
        public async Task<IActionResult> DeleteComment(string slug, Guid id , CancellationToken cancellation)
        {
            var data = await _mediator.Send(new DeleteCommentCommand { Slug = slug, Id = id }, cancellation);
            return Ok(data);
        }
        [Authorize]
        [HttpPost("{slug}/favourite")]
        public async Task<IActionResult> FavouriteArticle(string slug,  CancellationToken cancellation)
        {
            var data = await _mediator.Send(new AddArticleFavouriteCommand { Slug = slug }, cancellation);
            return Ok(data);
        }
        [Authorize]
        [HttpDelete("{slug}/favourite")]
        public async Task<IActionResult> UnFavouriteArticle(string slug, CancellationToken cancellation)
        {
            var data = await _mediator.Send(new DeleteArticleFavouriteCommand { Slug = slug }, cancellation);
            return Ok(data);
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags( CancellationToken cancellationToken)
        {
            
            var data = await _mediator.Send(new GetTagsQueries(), cancellationToken);
            return Ok(data.Tags);
        }
    }
}
