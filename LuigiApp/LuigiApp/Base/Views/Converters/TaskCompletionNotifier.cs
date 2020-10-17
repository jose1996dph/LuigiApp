﻿using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace LuigiApp.Base.Views.Converters
{
    // Watches a task and raises property-changed notifications when the task completes.
    public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged
    {
        public TaskCompletionNotifier(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var scheduler = (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                task.ContinueWith(t =>
                {
                    var propertyChanged = PropertyChanged;
                    if (propertyChanged != null)
                    {
                        propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
                        if (t.IsCanceled)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
                        }
                        else if (t.IsFaulted)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
                            //propertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
                        }
                        else
                        {
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
                            propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
                        }
                    }
                },
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                scheduler);
            }
        }

        // Gets the task being watched. This property never changes and is never <c>null</c>.
        public Task<TResult> Task { get; private set; }

        /*
        Task ITaskCompletionNotifier.Task
        {
            get { return Task; }
        }
        */

        // Gets the result of the task. Returns the default value of TResult if the task has not completed successfully.
        public TResult Result
        {
            get { return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult); }
        }

        // Gets whether the task has completed.
        public bool IsCompleted => Task.IsCompleted;

        // Gets whether the task has completed successfully.
        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        // Gets whether the task has been canceled.
        public bool IsCanceled => Task.IsCanceled;

        // Gets whether the task has faulted.
        public bool IsFaulted => Task.IsFaulted;

        // Gets the error message for the original faulting exception for the task. Returns <c>null</c> if the task is not faulted.
        /*public string ErrorMessage
        { 
            get { return (InnerException == null) ? null : InnerException.Message; } 
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
