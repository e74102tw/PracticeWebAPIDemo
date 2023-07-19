using System;

namespace PracticeWebAPIDemo.WebApi.Infrastructure.Models
{
    public class ErrorInfoModel
    {
        public Guid? Id { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public List<ErrorDetail> Errors { get; set; }
    }

    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
