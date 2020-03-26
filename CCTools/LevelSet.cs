using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace CCTools
{
	[Serializable]
	public sealed class LevelSet : IList<Level>, IBindingList, IRaiseItemChangedEvents
	{
		private readonly IList<Level> _items = new List<Level>();
		private bool _updating;

		#region Events

		#region IBindingList Members

		public event ListChangedEventHandler ListChanged;
		private void OnListChanged(ListChangedEventArgs e)
		{
			ListChanged?.Invoke(this, e);
		}

		#endregion

		#endregion

		#region Properties

		#region IList<Level> Members

		public Level this[int index]
		{
			get { return index < 0 || index >= _items.Count ? null : _items[index]; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				var oldValue = _items[index];
				if (oldValue != value)
				{
					_items[index] = value;
					oldValue.parent = null;
					value.parent = this;
					if (!_updating)
						OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
				}
			}
		}

		#endregion

		#region ICollection<Level> Members

		public int Count
		{
			get { return _items.Count; }
		}

		bool ICollection<Level>.IsReadOnly
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

		#region IRaiseItemChangedEvents Members

		public bool RaisesItemChangedEvents
		{
			get { return true; }
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
			set { this[index] = (Level)value; }
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

		public void AddRange(IEnumerable<Level> collection)
		{
			_updating = true;
			foreach (var item in collection)
				Add(item);
			_updating = false;
			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public void Move(int oldIndex, int newIndex)
		{
			if (oldIndex < 0 || oldIndex >= _items.Count)
				return;
			if (newIndex < 0 || newIndex >= _items.Count)
				return;
			var level = _items[oldIndex];
			if (level == null)
				return;
			_updating = true;
			RemoveAt(oldIndex);
			Insert(newIndex, level);
			_updating = false;
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex, oldIndex));
		}

		public void Load(string fileName)
		{
			using (var stream = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.Default))
				Read(stream);
		}

		public void Save(string fileName)
		{
			using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None), Encoding.Default))
				Write(writer);
		}

		#region IList<Level> Members

		public int IndexOf(Level item)
		{
			return _items.IndexOf(item);
		}

		public void Insert(int index, Level item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			_items.Insert(index, item);
			item.parent = this;
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				throw new ArgumentOutOfRangeException("index");
			var item = this[index];
			_items.RemoveAt(index);
			item.parent = null;
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		#endregion

		#region ICollection<Level> Members

		public void Add(Level item)
		{
			if (item == null)
				throw new ArgumentNullException("item");
			_items.Add(item);
			item.parent = this;
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, Count - 1));
		}

		public void Clear()
		{
			foreach (var item in _items)
				item.parent = null;
			_items.Clear();
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public bool Contains(Level item)
		{
			return _items.Contains(item);
		}

		public void CopyTo(Level[] array, int arrayIndex)
		{
			_items.CopyTo(array, arrayIndex);
		}

		public bool Remove(Level item)
		{
			var index = IndexOf(item);
			if (index >= 0)
			{
				_items.RemoveAt(index);
				item.parent = null;
				if (!_updating)
					OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
				return true;
			}
			return false;
		}

		#endregion

		#region IEnumerable<Level> Members

		public IEnumerator<Level> GetEnumerator()
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
			var result = new Level();
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

		#region IList Members

		int IList.Add(object value)
		{
			Add((Level)value);
			return Count - 1;
		}

		bool IList.Contains(object value)
		{
			return Contains((Level)value);
		}

		int IList.IndexOf(object value)
		{
			return IndexOf((Level)value);
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (Level)value);
		}

		void IList.Remove(object value)
		{
			Remove((Level)value);
		}

		#endregion

		#region ICollection Members

		void ICollection.CopyTo(Array array, int index)
		{
			CopyTo((Level[])array, index);
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		#endregion

		internal void OnLevelChanged(Level level)
		{
			if (!_updating)
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, IndexOf(level)));
		}

		private void Read(BinaryReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			Clear();

			var levels = new List<Level>();
			reader.BaseStream.Seek(4, SeekOrigin.Current);
			var levelCount = reader.ReadUInt16();
			for (int i = 0; i < levelCount; i++)
			{
				var level = new Level();
				level.Read(reader);
				levels.Add(level);
			}
			AddRange(levels);
		}

		private void Write(BinaryWriter writer)
		{
			if (writer == null)
				throw new ArgumentNullException("writer");

			writer.Write(0x2AAAC);
			writer.Write((ushort)_items.Count);
			for (int i = 0; i < _items.Count; i++)
				_items[i].Write(writer, i);
		}

		#endregion
	}
}
