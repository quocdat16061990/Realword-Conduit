using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Common
{
    public class PagingParamsDTO
    {
        public int Limit { get; init; } = 20;
        public int Offset { get; init; } = 0;
    }
}