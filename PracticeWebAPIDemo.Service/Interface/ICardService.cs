using PracticeWebAPIDemo.Service.Dtos.Info;
using PracticeWebAPIDemo.Service.Dtos.ResultModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWebAPIDemo.Service.Interface
{
    public interface ICardService
    {
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CardResultModel>> GetList(CardSearchInfo info);

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>   
        Task<CardResultModel> Get(int id);

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        Task<bool> Insert(CardInfo info);

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        Task<bool> Update(int id, CardInfo info);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        Task<bool> Delete(int id);
    }
}
