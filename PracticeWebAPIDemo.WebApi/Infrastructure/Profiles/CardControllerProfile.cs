using AutoMapper;
using PracticeWebAPIDemo.Models.InputParameters;
using PracticeWebAPIDemo.Models.OutputModels;
using PracticeWebAPIDemo.Service.Dtos.Info;
using PracticeWebAPIDemo.Service.Dtos.ResultModel;

namespace PracticeWebAPIDemo.Infrastructure.Profiles
{
    public class CardControllerProfile : Profile
    {
        public CardControllerProfile()
        {
            // Parameter -> Info
            CreateMap<CardParameter, CardInfo>();
            CreateMap<CardSearchParameter, CardSearchInfo>();

            // ResultModel -> OutputModel
            CreateMap<CardResultModel, CardOutputModel>();
        }
    }
}
