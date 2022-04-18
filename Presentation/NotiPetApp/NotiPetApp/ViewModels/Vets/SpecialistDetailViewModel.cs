using System.ComponentModel;
using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class SpecialistDetailViewModel  : BaseViewModel , INotifyPropertyChanged
    {

        public string Image { get; set; }
        public string ImageMap { get; set; }

        public SpecialistDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            Image =
                "https://i0.wp.com/revista.weepec.com/wp-content/uploads/2021/02/vet-and-pet-EESKSLX.jpg?fit=1200%2C800&ssl=1";
            ImageMap =
                "https://androidbip.com/wp-content/uploads/2021/04/Como-ver-el-trafico-en-tiempo-real-Google-Maps-Android.jpg";

        }
    }
}