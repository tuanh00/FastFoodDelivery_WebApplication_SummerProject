using Data_Layer.ResourceModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IUserReportService
    {
        public Task<APIGenericReposneModel<int>> GetTotalActiveUser();
    }
}
