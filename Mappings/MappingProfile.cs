using AutoMapper;
using ToursAPI.DTOs;
using ToursAPI.Models;

namespace ToursAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Company -> CompanyDto (з індустрією та кількістю турів)
        CreateMap<Company, CompanyDto>()
            .ForMember(
                dest => dest.IndustryName,
                opt => opt.MapFrom(src => src.Industry != null ? src.Industry.Name : null)
            )
            .ForMember(
                dest => dest.Tours,
                opt => opt.MapFrom(src => src.Tours)
            );

        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();

        // Industry -> IndustryDto (з компаніями, якщо потрібно)
        CreateMap<Industry, IndustryDto>()
            .ForMember(
                dest => dest.Companies,
                opt => opt.MapFrom(src => src.Companies)
            );

        CreateMap<IndustryCreateDto, Industry>();
        CreateMap<IndustryUpdateDto, Industry>();

        // Tour -> TourDto (з компанією)
        CreateMap<Tour, TourDto>()
            .ForMember(
                dest => dest.CompanyName,
                opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : null)
            );

        CreateMap<TourCreateDto, Tour>();
        CreateMap<TourUpdateDto, Tour>();
        
        // Feedback -> FeedbackDto
        CreateMap<Feedback, FeedbackDto>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.User.FullName)
            )
            .ForMember(
                dest => dest.TourTitle,
                opt => opt.MapFrom(src => src.Tour.Title)
            );
        
        CreateMap<FeedbackCreateDto, Feedback>();
        CreateMap<FeedbackUpdateDto, Feedback>();
        
        // TourVisit ->TourVisitDto
        CreateMap<TourVisit, TourVisitDto>()
            .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.User.FullName)
            )
            .ForMember(
                dest => dest.TourTitle,
                opt => opt.MapFrom(src => src.Tour.Title)
            );
        
        CreateMap<TourVisitCreateDto, TourVisit>();
        CreateMap<TourVisitUpdateDto, TourVisit>();
        
        // User ->UserDto
        CreateMap<User, UserDto>();
    }
}