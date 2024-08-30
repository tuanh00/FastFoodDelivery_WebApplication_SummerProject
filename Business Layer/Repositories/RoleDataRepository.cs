using Data_Layer.ResourceModel.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class RoleDataRepository : IDataService
    {
        private readonly IServiceProvider _app;

        public RoleDataRepository(IServiceProvider app)
        {
            _app = app;
        }

        public async Task AddData()
        {
            var scope = _app.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var listRoles = UserRole.Roles;

            foreach (var role in listRoles)
            {
                if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }
}
