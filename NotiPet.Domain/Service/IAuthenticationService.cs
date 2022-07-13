using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IAuthenticationService
    {
        public SourceList<SocialNetwork> SocialNetworks { get; }

        public IObservable<IEnumerable<SocialNetwork>> GetSocialNetworks();

        public IObservable<Authentication> Authentication(IAuthenticationRequestViewModel viewModel);
        public IObservable<Authentication> SignUp(IRegisterRequestViewModel viewModel);
        public IObservable<bool> ForgotPassword(string email);
        IObservable<User> ValidateCode(int code);

        IObservable<bool> UpdatePassword(string userId, string newPassword);

        public IObservable<List<PersonalDocument>>  GetDocumentTypes();
    }
}