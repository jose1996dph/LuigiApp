using LuigiApp.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LuigiApp.Base.Interactors
{
    public abstract class BaseInteractor<T> where T : new()
    {
        protected IDataStore DataStore => DependencyService.Get<IDataStore>();
        public abstract Task<bool> Save(T obj);
        public virtual Task<List<T>> All() => DataStore.Get<T>();
        public virtual Task<T> Get(int id) => DataStore.Get<T>(id);
        public virtual Task<bool> Delete(T obj) => DataStore.Delete<T>(obj);
    }
}
