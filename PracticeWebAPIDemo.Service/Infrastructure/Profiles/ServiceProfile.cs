using AutoMapper;
using PracticeWebAPIDemo.Repository.Entities.Condition;
using PracticeWebAPIDemo.Repository.Entities.DataModel;
using PracticeWebAPIDemo.Service.Dtos.Info;
using PracticeWebAPIDemo.Service.Dtos.ResultModel;

namespace PracticeWebAPIDemo.Service.Infrastructure.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            // Info -> Condition
            CreateMap<CardInfo, CardCondition>();
            CreateMap<CardSearchInfo, CardSearchCondition>();

            // DataModel -> ResultModel
            CreateMap<CardDataModel, CardResultModel>();
        }
    }
}
