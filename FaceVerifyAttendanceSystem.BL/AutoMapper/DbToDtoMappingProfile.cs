using AutoMapper;
using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;

namespace FaceVerifyAttendanceSystem.BL.AutoMapper
{
    public class DbToDtoMappingProfile: Profile
    {
        public DbToDtoMappingProfile() 
        {
            CreateMap<User, RegisterDTO>().ReverseMap()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.EducationalInstitution, opt => opt.MapFrom(src => src.EducationalInstitution))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<User, ExternalLoginDTO>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EducationalInstitution, opt => opt.MapFrom(src => src.EducationalInstitution))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}