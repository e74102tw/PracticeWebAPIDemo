using AutoMapper;
using CoreProfiler;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPIDemo.Infrastructure.ActionFilters;
using PracticeWebAPIDemo.Infrastructure.Validators;
using PracticeWebAPIDemo.Models.InputParameters;
using PracticeWebAPIDemo.Models.OutputModels;
using PracticeWebAPIDemo.Service.Dtos.Info;
using PracticeWebAPIDemo.Service.Dtos.ResultModel;
using PracticeWebAPIDemo.Service.Interface;
using PracticeWebAPIDemo.WebApi.Infrastructure.ActionFilters;
using PracticeWebAPIDemo.WebApi.Infrastructure.Models;

namespace PracticeWebAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper mapper ) 
        {
            _cardService = cardService;
            _mapper= mapper;
        }
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        [CoreProfiling]
        [HttpGet("GetList")]
        [Produces("application/json")]
        [CustomValidator(typeof(CardSearchParameterValidator))]
        public async Task<IEnumerable<CardOutputModel>> GetList([FromQuery]CardSearchParameter parameter)
        {
            var info = this._mapper.Map<
                CardSearchParameter,
              CardSearchInfo>(parameter);

            var cards = await this._cardService.GetList(info);

            var result = this._mapper.Map<
                IEnumerable<CardResultModel>,
              IEnumerable<CardOutputModel>>(cards);

            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <remarks>我是附加說明</remarks>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        /// <response code="200">回傳對應的卡片</response>
        [CoreProfiling]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResultOutputModel<CardOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorInfoModel), StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var card = await this._cardService.Get(id);

            if (card is null)
            {
                ErrorDetail Detail = new ErrorDetail()
                {
                    ErrorCode = StatusCodes.Status404NotFound.ToString(),
                    Message = $"查無此筆資料 Id:{id}",
                    Description = $"查無此筆資料 Id:{id}"
                };
                return NotFound(Detail);
            }

            var result = this._mapper.Map<
                CardResultModel,
              CardOutputModel>(card);

            return Ok(result);
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [CoreProfiling]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CardParameter parameter)
        {
            var info = this._mapper.Map<
                CardParameter,
              CardInfo>(parameter);

            var isInsertSuccess = await this._cardService.Insert(info);
            if (isInsertSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [CoreProfiling]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorInfoModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CardParameter parameter)
        {
            var targetCard = await this._cardService.Get(id);
            if (targetCard is null)
            {
                ErrorDetail Detail = new ErrorDetail()
                {
                    ErrorCode = StatusCodes.Status404NotFound.ToString(),
                    Message = $"查無此筆資料 Id:{id}",
                    Description = $"查無此筆資料 Id:{id}"
                };
                return NotFound(Detail);
            }

            var info = this._mapper.Map<
                CardParameter,
              CardInfo>(parameter);

            var isUpdateSuccess = await this._cardService.Update(id, info);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [CoreProfiling]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorInfoModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var targetCard = await this._cardService.Get(id);
            if (targetCard is null)
            {
                ErrorDetail Detail = new ErrorDetail()
                {
                    ErrorCode = StatusCodes.Status404NotFound.ToString(),
                    Message = $"查無此筆資料 Id:{id}",
                    Description = $"查無此筆資料 Id:{id}"
                };
                return NotFound(Detail);
            }

            var isDeleteSuccess = await this._cardService.Delete(id);
            if (isDeleteSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
