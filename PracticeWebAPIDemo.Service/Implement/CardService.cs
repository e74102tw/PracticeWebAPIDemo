using AutoMapper;
using PracticeWebAPIDemo.Repository.Entities.Condition;
using PracticeWebAPIDemo.Repository.Entities.DataModel;
using PracticeWebAPIDemo.Repository.Interface;
using PracticeWebAPIDemo.Service.Dtos.Info;
using PracticeWebAPIDemo.Service.Dtos.ResultModel;
using PracticeWebAPIDemo.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWebAPIDemo.Service.Implement
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;
        public CardService(IMapper mapper, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<CardResultModel>> GetList(CardSearchInfo info)
        {
            var condition = this._mapper.Map<CardSearchInfo, CardSearchCondition>(info);
            var data = await this._cardRepository.GetList(condition);

            var result = this._mapper.Map<
                IEnumerable<CardDataModel>,
                IEnumerable<CardResultModel>>(data);
            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public async Task<CardResultModel> Get(int id)
        {
            var card = await this._cardRepository.Get(id);
            var result = this._mapper.Map<CardDataModel, CardResultModel>(card);
            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<bool> Insert(CardInfo info)
        {
            var condition = this._mapper.Map<CardInfo, CardCondition>(info);
            var result = await this._cardRepository.Insert(condition);
            return result;
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<bool> Update(int id, CardInfo info)
        {
            var condition = this._mapper.Map<CardInfo, CardCondition>(info);
            var result = await this._cardRepository.Update(id, condition);
            return result;
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            var result = await this._cardRepository.Delete(id);
            return result;
        }
    }
}
