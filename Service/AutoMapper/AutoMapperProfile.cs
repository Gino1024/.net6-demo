using AutoMapper;
using Database.Models.DTO.ParameterDto;
using Database.Models.DTO.ResultDto;
using Database.Models.SQLModels;
using Service.Models.DTO.RequestDto;
using Service.Models.DTO.ResponseDto;

namespace Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TUser, AccountResponseDto>();
            #region 查詢使用者列表
            CreateMap<GetListAccountRequestDto, GetListAccountParameterDto>();
            CreateMap<GetListAccountResultDto, GetListAccountResponseDto>();
            CreateMap<AccountResultDto, AccountResponseDto>();
            #endregion
            CreateMap<PostLoginAccountRequestDto, TUser>()
                .ForMember(
                t=>t.Account,
                s=>s.MapFrom(m=>m.Email));
            CreateMap<PutLoginAccountRequestDto, TUser>();


        }
    }
}
