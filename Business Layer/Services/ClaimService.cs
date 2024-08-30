using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly uint _id;
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            _id = string.IsNullOrEmpty(Id) ? 0 : uint.Parse(Id);
        }

        public uint GetCurrentUserId => _id;
    }
}
