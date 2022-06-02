using System.Reactive.Concurrency;

namespace NotiPetApp.Services
{
    public interface ISchedulerProvider
    {
        IScheduler MainThread { get; }
        IScheduler CurrentThread { get; } 
    }
    public  class  SchedulerProvider:ISchedulerProvider
    {
        public SchedulerProvider(IScheduler mainThread,IScheduler currentThread)
        {
            MainThread = mainThread;
            CurrentThread = currentThread;
        }
        public IScheduler MainThread { get; }
        public IScheduler CurrentThread { get; }
    }
}