
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class AuthenticationService:IAuthenticationService,IDisposable
    {
        private readonly IUserServiceApi _userServiceApi;
        private readonly IMapper _mapper;
        private SourceList<SocialNetwork> _sourceList = new SourceList<SocialNetwork>();
        public SourceList<SocialNetwork> SocialNetworks => _sourceList;

        public AuthenticationService(IUserServiceApi userServiceApi,IMapper mapper)
        {
            _userServiceApi = userServiceApi;
            _mapper = mapper;
        }
        public IObservable<IEnumerable<SocialNetwork>> GetSocialNetworks()
        {
            SocialNetworks.Clear();
            return Observable.Return(new List<SocialNetwork>()
            {
                new SocialNetwork()
                {
                    Name = "Facebook",
                    Icon = "FbIcon",
                    Id = 1,
                    Code = "FB",
                    Color = "#4267B2"
                },
                new SocialNetwork()
                {
                    Name = "Google",
                    Icon = "GBIcon",
                    Id = 2,
                    Code = "GB",
                    Color = "#DB4437"
                },
                new SocialNetwork()
                {
                    Name = "NotiPed",
                    Icon = "NT",
                    Id = 3,
                    Code = "NT",
                    Color = "#00999b"
                }
            }).Do(_sourceList.AddRange);
        }
        public IObservable<string> Authentication(IAuthenticationRequestViewModel viewModel)
        {
            return _userServiceApi.LogIn(_mapper.Map<RequestAuthenticationDto>(viewModel))
                .Select(e=>e.Token);

        }

        public IObservable<string> SignUp(IRegisterRequestViewModel register)
        {
            var user = new User(0,register.PersonalDocument.DocumentId,register.Name,DateTime.Now, 
                register.LastName,DateTime.Today, register.Phone,register.Address1,register.Address2,
                register.City,register.Province,register.PersonalDocument.DocumentType);
            
            var userRole = new UserRole(DateTime.Now, DateTime.Now, true, register.Username, register.Password,
                register.Email, 0, register.BusinessId, 1, 0, user);

             return  _userServiceApi.SingUp(_mapper.Map<UserRoleDto>(userRole))
                 .Select(x=>x.Jwt);
        }

        public void Dispose()
        {
            _sourceList?.Dispose();
        }
    }
}