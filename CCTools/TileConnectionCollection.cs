using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace CCTools
{
	[Serializable]
	public sealed class TileConnectionCollection : IList<TileConnection>, IBindingList, ICloneable
	{
		private readonly IList<TileConnection> _items = new List<TileConnection>();
		private bool _updating;
		private readonly int _maxItems;

		public TileConnectionCollection() { }
		public TileConnectionCollection(int maxItems) { _maxItems = maxItems; }

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

		public TileLocationCollection Destinations
		{
			get
			{
				var result = new TileLocationCollection();
				foreach (var connection in _items)
					result.Add(connection.Destination);
				return result;
			}
		}

		public TileLocationCollection Sources
		{
			get
			{
				var result = new TileLocationCollection();
				foreach (var connection in _items)
					result.Add(connection.Source);
				return result;
			}
		}

		#region IList<TileConnection> Members

		public TileConnection this[int index]
		{
			get { return _items[index]; }
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

		#region ICollection<TileConnection> Members

		public int Count
		{
			get { return _items.Count; }
		}

		bool ICollection<TileConnection>.IsReadOnly
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
			set { this[index] = (TileConnection)value; }
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

		public void AddRange(IEnumerable<TileConnection> collection)
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

		public TileLocationCollection GetDestinationsFromSource(TileLocation source)
		{
			var result = new TileLocationCollection();
			foreach (var connection in _items)
				if (connection.Source == source)
					result.Add(connection.Destination);
			return result;
		}

		public TileLocationCollection GetSourcesFromDestination(TileLocation destination)
		{
			var result = new TileLocationCollection();
			foreach (var connection in _items)
				if (connection.Destination == destination && !result.Contains(connection.Source))
					result.Add(connection.Source);
			return result;
		}

		public void Move(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || oldIndex >= _items.Count)
				return;
			if (newIndex < 0 || newIndex >= _items.Count)
				return;
			var connection = _items[oldIndex];
			_updating = true;
			RemoveAt(oldIndex);
			Insert(newIndex, connection);
			_updating = false;
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
		}

		#region IList<TileConnection> Members

		public int IndexOf(TileConnection item)
		{
			return _items.IndexOf(item);
		}

		public void Insert(int index, TileConnection item)
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

		#region ICollection<TileConnection> Members

		public void Add(TileConnection item)
		{
			if (_maxItems > 0 && _items.Count >= _maxItems)
				return;
			_items.Add(item);
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Count - 1));
		}

		public void Add(TileLocation source, TileLocation destination)
		{
			Add(new TileConnection(source, destination));
		}

		public void Add(int sourceX, int sourceY, int destinationX, int destinationY)
		{
			Add(new TileConnection(sourceX, sourceY, destinationX, destinationY));
		}

		public void Clear()
		{
			_items.Clear();
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public bool Contains(TileConnection item)
		{
			return _items.Contains(item);
		}

		public bool Contains(TileLocation source, TileLocation destination)
		{
			return _items.Contains(new TileConnection(source, destination));
		}

		public bool Contains(int sourceX, int sourceY, int destinationX, int destinationY)
		{
			return _items.Contains(new TileConnection(new TileLocation(sourceX, sourceY), new TileLocation(destinationX, destinationY)));
		}

		public void CopyTo(TileConnection[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public bool Remove(TileConnection item)
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

		public void Remove(TileLocation source, TileLocation destination)
		{
			Remove(new TileConnection(source, destination));
		}

		public void Remove(int sourceX, int sourceY, int destinationX, int destinationY)
		{
			Remove(new TileConnection(sourceX, sourceY, destinationX, destinationY));
		}

		#endregion

		#region IEnumerable<TileConnection> Members

		public IEnumerator<TileConnection> GetEnumerator()
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
			var result = new TileConnection();
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

		public TileConnectionCollection Clone()
		{
			var result = new TileConnectionCollection(_maxItems);
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
			Add((TileConnection)value);
			return Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((TileConnection)value);
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((TileConnection)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (TileConnection)value);
		}

		void IList.Remove(object value)
		{
			Remove((TileConnection)value);
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(Array array, int index)
		{
			CopyTo((TileConnection[])array, index);
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
