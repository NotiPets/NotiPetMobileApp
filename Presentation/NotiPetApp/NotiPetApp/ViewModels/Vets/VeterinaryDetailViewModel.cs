using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class VeterinaryDetailViewModel : BaseViewModel
    {
        public string Image { get; set; }
        public string ImageMap { get; set; }
        public VeterinaryDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            Image =
                "https://cdn.techtitute.com/techtitute-blog/2021/03/Peritaje-veterinario-1-scaled.jpg";
            ImageMap =
                "https://androidbip.com/wp-content/uploads/2021/04/Como-ver-el-trafico-en-tiempo-real-Google-Maps-Android.jpg";

        }
    }
}