using Dapper;
using PracticeWebAPIDemo.Repository.Entities.Condition;
using PracticeWebAPIDemo.Repository.Entities.DataModel;

namespace PracticeWebAPIDemo.Repository.Interface
{
    public interface ICardRepository
    {
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CardDataModel>> GetList(CardSearchCondition info);

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>   
        Task<CardDataModel> Get(int id);

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        Task<bool> Insert(CardCondition info);

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        Task<bool> Update(int id, CardCondition info);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}
