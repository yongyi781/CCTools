using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CCTools
{
	[Serializable]
	public sealed class TileLocationCollection : IList<TileLocation>, IBindingList, ICloneable
	{
		private readonly IList<TileLocation> _items = new List<TileLocation>();
		private readonly int _maxItems;
		private bool _updating;

		public TileLocationCollection() { _maxItems = -1; }
		public TileLocationCollection(int maxItems) { _maxItems = maxItems; }

		#region Events

		#region IBindingList Members

		public event ListChangedEventHandler ListChanged;
		internal void OnListChanged(ListChangedEventArgs e)
		{
			ListChanged?.Invoke(this, e);
		}

		#endregion

		#endregion

		#region Properties

		#region IList<TileLocation> Members

		public TileLocation this[int index]
		{
			get { return index < 0 || index >= _items.Count ? TileLocation.Invalid : _items[index]; }
			set
			{
				var oldValue = _items[index];
				if (oldValue != value)
				{
					_items[index] = value;
					if (!_updating)
						OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
				}
			}
		}

		#endregion

		#region ICollection<TileLocation> Members

		public int Count
		{
			get { return _items.Count; }
		}

		bool ICollection<TileLocation>.IsReadOnly
		{
			get { return false; }
		}

		#endregion

		#region IBindingList Members

		bool IBindingList.AllowEdit
		{
			get { return true; }
		}

		bool IBindingList.AllowNew
		{
			get { return true; }
		}

		bool IBindingList.AllowRemove
		{
			get { return true; }
		}

		bool IBindingList.IsSorted
		{
			get { throw new NotSupportedException(); }
		}

		ListSortDirection IBindingList.SortDirection
		{
			get { throw new NotSupportedException(); }
		}

		PropertyDescriptor IBindingList.SortProperty
		{
			get { throw new NotSupportedException(); }
		}

		bool IBindingList.SupportsChangeNotification
		{
			get { return true; }
		}

		bool IBindingList.SupportsSearching
		{
			get { return false; }
		}

		bool IBindingList.SupportsSorting
		{
			get { return false; }
		}

		#endregion

		#region IList Members

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (TileLocation)value; }
		}

		#endregion

		#region ICollection Members

		bool ICollection.IsSynchronized
		{
			get { return true; }
		}

		object ICollection.SyncRoot
		{
			get { return this; }
		}

		#endregion

		#endregion

		#region Methods

		public void AddRange(IEnumerable<TileLocation> collection)
		{
			foreach (var item in collection)
			{
				if (_maxItems > 0 && _items.Count >= _maxItems)
					break;
				_items.Add(item);
			}
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public void Move(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || oldIndex >= _items.Count)
				return;
			if (newIndex < 0 || newIndex >= _items.Count)
				return;
			var item = _items[oldIndex];
			_updating = true;
			RemoveAt(oldIndex);
			Insert(newIndex, item);
			_updating = false;
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
		}

		#region IList<TileLocation> Members

		public int IndexOf(TileLocation item)
		{
			return _items.IndexOf(item);
		}

		public void Insert(int index, TileLocation item)
		{
			if (_maxItems > 0 && _items.Count >= _maxItems)
				return;
			_items.Insert(index, item);
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				throw new ArgumentOutOfRangeException("index");
			_items.RemoveAt(index);
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		#endregion

		#region ICollection<TileLocation> Members

		public void Add(TileLocation item)
		{
			if (_maxItems > 0 && _items.Count >= _maxItems)
				return;
			_items.Add(item);
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Count - 1));
		}

		public void Clear()
		{
			_items.Clear();
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public bool Contains(TileLocation item)
		{
			return _items.Contains(item);
		}

		public void CopyTo(TileLocation[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public bool Remove(TileLocation item)
		{
			var index = IndexOf(item);
			if (index >= 0)
			{
				_items.RemoveAt(index);
				if (!_updating)
					OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
				return true;
			}
			return false;
		}

		#endregion

		#region IEnumerable<TileLocation> Members

		public IEnumerator<TileLocation> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		#endregion

		#region IBindingList Members

		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		public object AddNew()
		{
			var result = new TileLocation();
			Add(result);
			return result;
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw new NotSupportedException();
		}

		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		#endregion

		#region ICloneable Members

		public TileLocationCollection Clone()
		{
			var result = new TileLocationCollection(_maxItems);
			foreach (var item in _items)
				result._items.Add(item);
			return result;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		#region IList Members

		int IList.Add(object value)
		{
			Add((TileLocation)value);
			return Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((TileLocation)value);
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((TileLocation)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (TileLocation)value);
		}

		void IList.Remove(object value)
		{
			Remove((TileLocation)value);
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(Array array, int index)
		{
			CopyTo((TileLocation[])array, index);
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		#endregion

		#endregion
	}
}
