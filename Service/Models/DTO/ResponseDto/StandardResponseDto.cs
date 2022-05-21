using Newtonsoft.Json;

namespace Service.Models.DTO.ResponseDto
{
    public class StandardResponseDto<T>
    {
        public bool? Success { get; set; } = false;
        public T? Result { get; set; }
        public string? Message { get; set; }
        public string ConvertToJson()
        {
            string result = (this.Result != null) ? JsonConvert.SerializeObject(this.Result) : "" ;
            return result;
        }
    }
}
