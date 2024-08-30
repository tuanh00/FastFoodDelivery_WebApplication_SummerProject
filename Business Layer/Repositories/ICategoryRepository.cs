using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoryAll();
        Task<int> GetTotalCategories();
    }
}
