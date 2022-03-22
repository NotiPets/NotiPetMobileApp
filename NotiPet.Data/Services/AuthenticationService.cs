
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
                .Select(e=>e?.Token);

        }

        public IObservable<string> SignUp(IRegisterRequestViewModel register)
        {
            var user = new User(Guid.NewGuid().ToString(),1,register.BusinessId,null,register.Username,register.Password,register.Email,register.DocumentType,register.Document,register.Name,register.LastName,register.Phone,register.Address1,register.Address2,register.City,register.Province,null,true,DateTime.Today,DateTime.Today);

             return  _userServiceApi.SingUp(_mapper.Map<UserDto>(user))
                 .Select(x=>x.Jwt);
        }

        public IObservable<List<PersonalDocument>> GetDocumentTypes()
        {
            return Observable.Return(new List<PersonalDocument>()
            {
                new PersonalDocument(1,"Id Card"),
                new PersonalDocument(2,"Passport"),
            });
        }
        public void Dispose()
        {
            _sourceList?.Dispose();
        }
    }
}