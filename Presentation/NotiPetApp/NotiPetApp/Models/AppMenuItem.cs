using System.Reactive;
using ReactiveUI;

namespace NotiPetApp.Models
{
    public class AppMenuItem:ReactiveObject
    {
        public AppMenuItem(string name, string image, int id, ReactiveCommand<Unit, Unit> reactiveCommand,SizeItem sizeItem)
        {
            Name = name;
            Image = image;
            Id = id;
            ReactiveCommand = reactiveCommand;
            SizeItem = sizeItem;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public ReactiveCommand<Unit,Unit> ReactiveCommand { get; set; }
        public SizeItem SizeItem { get; set; }

    }

    public enum SizeItem
    {
        Small,
        Medium,
        Large
    }
}