using Dapper;
using Microsoft.Extensions.Configuration;
using MMTracker.Abstract;
using MMTracker.Dto;
using MMTracker.Helper;
using MMTracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Service
{
    public class CategoryService : BaseRepository, ICategory
    {
        IConfiguration _configuration;

        public CategoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteCategoryByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_categoryid", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteCategory", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<CategoryDto>> GetAllCategories(int id = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_categoryid", id, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<CategoryDto>("GetCategory", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<CategoryDto> GetCategoryByKey(int id)
        {
            var dataList = await GetAllCategories(id);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateCategory(CategoryDto model)
        {
                using (connection = Get_Connection(_configuration))
                {
                    var param = new DynamicParameters();
                    param.Add("v_categoryId", model.CategoryId, DbType.Int64, ParameterDirection.Input);
                    param.Add("v_name", model.Name, DbType.String, ParameterDirection.Input);
                    param.Add("v_description", model.Description, DbType.String, ParameterDirection.Input);
                    var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateCategory", param, commandType: CommandType.StoredProcedure);
                    return lastInsertedId.FirstOrDefault();
                }
        }
    }
}
