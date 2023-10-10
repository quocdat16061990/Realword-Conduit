using DatDotnetConduit.Infrasturcture;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Queries
{
    public class GetTagsQueries : IRequest<GetTagsDTO>
    {
       
    }
    public class GetTagsDTO
    {
        public List<string> Tags { get; set; }
    }

    public class GetTagQueryHandler : IRequestHandler<GetTagsQueries, GetTagsDTO>
    {
        private readonly MainDbContext _mainDbContext;
        
        
        public GetTagQueryHandler(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
         
        }
        public async Task<GetTagsDTO> Handle (GetTagsQueries request , CancellationToken cancellationToken)
        {

            var tags = await _mainDbContext.Articles
                    .SelectMany(x => x.Tags)
                    .Distinct()
                    .Take(10)
                    .ToListAsync(cancellationToken);

            if (tags.Count == 0)
            {
                throw new RestException(HttpStatusCode.NotFound,"Tags is Empty");
            }

            var allTags = new GetTagsDTO
            {
                Tags = tags
            };

            return allTags;

        }
    }
}
