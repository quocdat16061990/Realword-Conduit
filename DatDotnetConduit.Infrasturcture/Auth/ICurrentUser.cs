using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Infrasturcture.Auth
{
     public interface ICurrentUser
    {
        public Guid? Id { get; }
        public string? Username { get; }
    }
}
