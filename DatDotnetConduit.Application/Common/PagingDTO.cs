using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Common
{
    public class PagingDTO<T>
    {
        public PagingDTO(int totalCount, List<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
        public int TotalCount { get; init; }
        public List<T> Items { get; init; }
    }
}