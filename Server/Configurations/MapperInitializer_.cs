using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using XebecPortal.Server.DTOs;
using XebecPortal.Shared;
using XebecPortal.Shared.Security;

namespace Server.Configurations
{
    public class MapperInitializer_ : Profile
    {
        public MapperInitializer_()
        {
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<AdditionalInformation, AdditionalInformationDTO>().ReverseMap();
            CreateMap<Application, ApplicationDTO>().ReverseMap();
            CreateMap<ApplicationPhase, ApplicationPhaseDTO>().ReverseMap();
            CreateMap<ApplicationPhaseHelper, ApplicationPhaseHelperDTO>().ReverseMap();
            CreateMap<Document, DocumentsDTO>().ReverseMap();
            CreateMap<Education, EducationDTO>().ReverseMap();
            CreateMap<JobPlatform, JobPlatformDTO>().ReverseMap();
            CreateMap<JobPlatformHelper, JobPlatformHelperDTO>().ReverseMap();
            CreateMap<JobType, JobTypeDTO>().ReverseMap();
            CreateMap<JobTypeHelper, JobTypeHelperDTO>().ReverseMap();
            CreateMap<LoginHelper, LoginHelperDTO>().ReverseMap();
            CreateMap<PersonalInformation, PersonalInformationDTO>().ReverseMap();
            CreateMap<ProfilePortfolioLink, ProfilePortfolioLinkDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<WorkHistory, WorkHistoryDTO>().ReverseMap();          
        }

    }
}
