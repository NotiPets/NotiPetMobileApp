using System.Reflection;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using Prism.Ioc;

namespace NotiPet.Domain
{
    public class DomainModule:Module
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.Register<AuthenticationValidator>();
            containerRegistry.Register<RegisterValidator>();
            containerRegistry.Register<CreatePetModelValidate>();
            containerRegistry.Register<CreateAppointmentValidate>();

        }
    }
}