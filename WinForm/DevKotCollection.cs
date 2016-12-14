using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
   public class DevKotCollection<T> : BindingList<T>, IList<T>
    {
        private readonly IList<T> _list = new List<T>();
        public T item { get; set; }
        public event EventHandler<DevKotCollectionEventArgs> AddItemEvent;
        public event EventHandler<DevKotCollectionEventArgs> ChangeItemEvent;

       
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
              
       
        public void Add(T item)
        {
            DevKotCollectionEventArgs devEventArgs = new DevKotCollectionEventArgs();
           _list.Add(item);
            devEventArgs.SetEventArgs<T>(item, "Rene");
            OnAddItemEvent(devEventArgs);
        }
        protected virtual void OnAddItemEvent(DevKotCollectionEventArgs e)
        {
            EventHandler<DevKotCollectionEventArgs> handler = AddItemEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

       

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

       
    }
    public class DevKotCollectionEventArgs : EventArgs
    {
        public Object itemcontent { get; set; }
        public String username { get; set; }

        public void SetEventArgs<T>(T item, String usernmae)
        {
            this.itemcontent = item;
            this.username = usernmae;
        } 


    }
}
