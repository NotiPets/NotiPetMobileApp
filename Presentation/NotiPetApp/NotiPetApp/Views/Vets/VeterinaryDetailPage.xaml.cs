using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views.Vets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VeterinaryDetailPage 
    {
        public VeterinaryDetailPage()
        {
            InitializeComponent();
        }

        protected override CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            disposables.Add(this.WhenAnyValue(x=>x.ViewModel.Veterinary)
                .Where(e=>e!=null)
                .Select(e=>new Position(e.Latitude,e.Longitude))
                .InvokeCommand(new Command<Position>(DoMapPositionChange)));
            return base.ManageDisposables(disposables);
        }
        private void DoMapPositionChange(Position position)
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(300)));
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (ViewModel != null)
            {
                WhenAppearing
                    .InvokeCommand(ViewModel?.InitializeCommand);
            }
        }
    }
}