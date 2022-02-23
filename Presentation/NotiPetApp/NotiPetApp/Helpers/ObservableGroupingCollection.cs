using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace NotiPetApp.Helpers
{
    public class ObservableGroupingCollection<TGrouped,TObject,TKey>:ObservableCollectionExtended<TObject>,IDisposable
    {
        public TGrouped Key { get; set; }
        private readonly IDisposable _disposable;
        public ObservableGroupingCollection(IGroup<TObject,TKey,TGrouped> group,IObservable<Func<TObject, bool>> filter,IObservable<IComparer<TObject>> compare)
        {
            this.Key = group.Key;
            _disposable = group.Cache.Connect()
                .Filter(filter)
                .Sort(compare)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(this)
                .Subscribe();
        }
        public ObservableGroupingCollection(IGroup<TObject,TKey,TGrouped> group)
        {
            this.Key = group.Key;
            _disposable = group.Cache.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(this)
                .Subscribe();
        }
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}