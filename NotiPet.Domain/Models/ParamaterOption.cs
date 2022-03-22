using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DynamicData.Binding;

namespace NotiPet.Domain.Models
{
    public class ParameterOption : INotifyPropertyChanged
    {
        public string Title { get; }
        public bool IsActive { get; private set; }
        public bool IsSort { get; }
        public int Id { get; }
        public string Key { get; }
        
        public ParameterOption(string title, bool isActive, bool isSort, int id, string key)
        {
            Title = title;
            IsActive = isActive;
            IsSort = isSort;
            Id = id;
            Key = key;

        }

        private ParameterOption(bool isActive)
        {
            IsActive = isActive;
        }
        public static readonly ParameterOption Default = new ParameterOption(false); 

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetActive(bool active)
        {
            IsActive = active;
            OnPropertyChanged(nameof(IsActive));
        }

        public ParameterOption SetFilterExpression<T>(Func<T, bool> func)
        {
            FilterExpressions =  func;
       
            return this;
        }
 
        private object FilterExpressions { get; set; }
        private object SortExpressions { get; set; }

        public ParameterOption SetSortExpression<T>(SortExpressionComparer<T>  func)
        {
           SortExpressions =  func;
            return this;
        }
        public T GetFilterExpression<T>() 

        {
            return (T)FilterExpressions ;
        }

        public T GetSortExpressions<T>()
        {
            return (T)SortExpressions ;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}