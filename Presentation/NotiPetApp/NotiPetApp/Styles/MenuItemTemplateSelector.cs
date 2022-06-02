using System;
using NotiPetApp.Models;
using Xamarin.Forms;

namespace NotiPetApp.Styles
{
    public class MenuItemTemplateSelector:DataTemplateSelector
    {
        public DataTemplate LargeItemDataTemplate { get; set; }
        
        public DataTemplate MediumItemDataTemplate { get; set; }
        
        public DataTemplate SmallItemDataTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var appMenuItem = item as AppMenuItem;
            return appMenuItem?.SizeItem switch
            {
                SizeItem.Medium => MediumItemDataTemplate,
                SizeItem.Large => LargeItemDataTemplate,
                _ => SmallItemDataTemplate
            };
        }
    }
}