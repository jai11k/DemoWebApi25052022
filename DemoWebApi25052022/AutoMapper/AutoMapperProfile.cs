using Demo.Models;
using AutoMapper;
using Demo.ViewModels;

namespace DemoWebApi25052022.AutoMapper
{
        public class AppProfile : Profile
        {
            public AppProfile()
            {
            CreateMap<IssueViewModel, Issue>().ReverseMap();

            }
        }
    
}
