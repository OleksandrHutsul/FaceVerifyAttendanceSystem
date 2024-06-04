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
                .ForMember(x => x.LastName, y => y.MapFrom(src => src.LastName))
                .ForMember(x => x.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(src => src.MiddleName))
                .ForMember(x => x.EducationalInstitution, y => y.MapFrom(src => src.EducationalInstitution))
                .ForMember(x => x.PasswordHash, y => y.MapFrom(src => src.Password))
                .ForMember(x => x.Email, y => y.MapFrom(src => src.Email));

            CreateMap<User, ExternalLoginDTO>().ReverseMap()
                .ForMember(x => x.Email, y => y.MapFrom(src => src.Email))
                .ForMember(x => x.EducationalInstitution, y => y.MapFrom(src => src.EducationalInstitution))
                .ForMember(x => x.LastName, y => y.MapFrom(src => src.LastName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(src => src.MiddleName))
                .ForMember(x => x.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(x => x.UserName, y => y.MapFrom(src => src.Email));

            CreateMap<User, IndexDTO>().ReverseMap()
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.FirstName, y => y.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(src => src.LastName))
                .ForMember(x => x.MiddleName, y => y.MapFrom(src => src.MiddleName))
                .ForMember(x => x.EducationalInstitution, y => y.MapFrom(src => src.EducationalInstitution))
                .ForMember(x => x.Birthday, y => y.MapFrom(src => src.Birthday))
                .ForMember(x => x.IdentificationNumber, y => y.MapFrom(src => src.IdentificationNumber))
                .ForMember(x => x.Country, y => y.MapFrom(src => src.Country))
                .ForMember(x => x.City, y => y.MapFrom(src => src.City))
                .ForMember(x => x.CourseEducation, y => y.MapFrom(src => src.CourseEducation))
                .ForMember(x => x.ProfilePicture, y => y.MapFrom(src => src.ProfilePicture));

            CreateMap<Application, ApplicationDTO>().ReverseMap();

            CreateMap<ApplicationStatus, ApplicationStatusDTO>().ReverseMap()
                .ForMember(x => x.StatusName, y => y.MapFrom(src => src.StatusName));

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<Lesson, WordsCodeDTO>().ReverseMap();
            CreateMap<Lesson, CourseDetailDTO>().ReverseMap();
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
        }
    }
}