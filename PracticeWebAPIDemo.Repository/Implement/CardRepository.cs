using Dapper;
using PracticeWebAPIDemo.Repository.Interface;
using PracticeWebAPIDemo.Repository.Helpers;
using PracticeWebAPIDemo.Repository.Entities.Condition;
using PracticeWebAPIDemo.Repository.Entities.DataModel;

namespace PracticeWebAPIDemo.Repository.Implement
{
    public class CardRepository: ICardRepository
    {
        private readonly IDatabaseHelper _databaseHelper;


        public CardRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CardDataModel>> GetList(CardSearchCondition condition)
        {
            var sql = @"SELECT  [Id], 
                                [Name],
                                [Description],
                                [Attack],
                                [Health],
                                [Cost] 
                        FROM Card
                        WHERE 1=1
                              AND (@Name is null OR Name LIKE @Name)
                              AND (@MinCost is null OR Cost >= @MinCost)
                              AND (@MaxCost is null OR Cost <= @MaxCost)
                              AND (@MinAttack is null OR Attack >= @MinAttack)
                              AND (@MaxAttack is null OR Attack <= @MaxAttack)
                              AND (@MinHealth is null OR Health >= @MinHealth)
                              AND (@MaxHealth is null OR Health <= @MaxHealth)
                        ";

            var parameter = new DynamicParameters();
            parameter.Add("Name", string.IsNullOrWhiteSpace(condition.Name) == false ? $"%{condition.Name}%" : null);
            parameter.Add("MinCost", condition.MinCost.HasValue ? condition.MinCost : null);
            parameter.Add("MaxCost", condition.MaxCost.HasValue ? condition.MaxCost : null);
            parameter.Add("MinAttack", condition.MinAttack.HasValue ? condition.MinAttack : null);
            parameter.Add("MaxAttack", condition.MaxAttack.HasValue ? condition.MaxAttack : null);
            parameter.Add("MinHealth", condition.MinHealth.HasValue ? condition.MinHealth : null);
            parameter.Add("MaxHealth", condition.MaxHealth.HasValue ? condition.MaxHealth : null);


            using (var conn = this._databaseHelper.GetConnection())
            {
                var result = await conn.QueryAsync<CardDataModel>(sql, parameter);
                return result;
            }
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <returns></returns>
        public async Task<CardDataModel> Get(int id)
        {
            var sql =
            @"
                SELECT  [Id], 
                        [Name],
                        [Description],
                        [Attack],
                        [Health],
                        [Cost] 
                FROM Card 
                Where Id = @id
            ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = this._databaseHelper.GetConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<CardDataModel>(sql, parameters);
                return result;
            }
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public async Task<bool> Insert(CardCondition parameter)
        {
            var sql =
            @"
                INSERT INTO Card 
                (
                   [Name]
                  ,[Description]
                  ,[Attack]
                  ,[Health]
                  ,[Cost]
                ) 
                VALUES 
                (
                     @Name
                    ,@Description
                    ,@Attack
                    ,@Health
                    ,@Cost
                );
            
                SELECT @@IDENTITY;
            ";

            using (var conn = this._databaseHelper.GetConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<int>(sql, parameter);
                return result > 0;
            }
        }

        /// <summary>
        /// 修改卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public async Task<bool> Update(int id, CardCondition parameter)
        {
            var sql =
            @"
            UPDATE Card
            SET 
                 [Name] = @Name
                ,[Description] = @Description
                ,[Attack] = @Attack
                ,[Health] = @Health
                ,[Cost] = @Cost
            WHERE 
                Id = @id
        ";

            var parameters = new DynamicParameters(parameter);
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = this._databaseHelper.GetConnection())
            {
                var result = await conn.ExecuteAsync(sql, parameters);
                return result > 0;
            }
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            var sql =
            @"
            DELETE FROM Card
            WHERE Id = @Id
        ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = this._databaseHelper.GetConnection())
            {
                var result = await conn.ExecuteAsync(sql, parameters);
                return result > 0;
            }
        }
    }
}
