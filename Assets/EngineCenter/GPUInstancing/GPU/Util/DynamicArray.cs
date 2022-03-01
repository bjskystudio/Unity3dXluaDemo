using System;
using UnityEngine;
using System.Collections;

namespace YoukiaEngine
{
	public class DynamicArray<T>
	{
		private const int DefaultCapacity = 4;

		internal T[] _array;
		private int _size;

		public DynamicArray()
		{
			_array = new T[DefaultCapacity];
			_size = 0;
			Init();
		}

		public DynamicArray(int capacity)
		{
			_array = new T[capacity];
			_size = 0;
			Init();
		}

		~DynamicArray()
		{
			Clear();
		}

		private void Init()
		{
			for (int i = 0; i < _size; i++)
				_array[i] = default(T);
		}

		public int Count
		{
			get => _size; 
		}

		public T this[int index]
		{
			get => _array[index];
			set { _array[index] = value; }
		}

		/// <summary>
		/// add item
		/// </summary>
		/// <param name="item"></param>
		public void Add(T item)
		{
			int size = _size + 1;

			int length = 0;
			if (_array != null)
				length = _array.Length;

			if (size > length)
			{
				if (length == 0)
				{
					length = DefaultCapacity;
					_array = null;
					_array = new T[length];
				}
				else
				{
					length *= 2;
					T[] arr = new T[length];
					CopyTo(arr);
					_array = null;
					_array = arr;
				}
			}

			_array[_size] = item;
			_size = size;
		}

		/// <summary>
		/// add range
		/// </summary>
		/// <param name="items"></param>
		public void AddRange(T[] items)
		{
			int size = _size + items.Length;

			int length = 0;
			if (_array != null)
				length = _array.Length;

			if (size > length)
			{
				if (length == 0)
				{
					length = size;
					_array = null;
					_array = new T[length];
				}
				else
				{
					length = Math.Max(length * 2, size);
					T[] arr = new T[length];
					CopyTo(arr);
					_array = null;
					_array = arr;
				}
			}

			items.CopyTo(_array, _size);
			_size = size;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			for (int i = 0; i < _size; i++)
			{
				if (_array[i].Equals(item))
					return true;
			}

			return false;
		}

		public int IndexOf(T item)
		{
			int index = -1;

			for (int i = 0; i < _size; i++)
			{
				if (_array[i].Equals(item))
				{
					index = i;
					break;
				}
			}

			return index;
		}

		/// <summary>
		/// remove first item equals the param
		/// </summary>
		/// <param name="item"></param>
		public void Remove(T item)
		{
			int arrayIndex = IndexOf(item);

			RemoveAt(arrayIndex);
		}

		/// <summary>
		/// remove item at the arrayIndex
		/// </summary>
		/// <param name="arrayIndex"></param>
		public void RemoveAt(int arrayIndex)
		{
			if (arrayIndex >= _size || arrayIndex < 0)
			{
				Debug.LogError("arrayIndex out of range.");
				return;
			}

			if (arrayIndex == _size - 1)
				_array[arrayIndex] = default(T);
			else
			{
				for (int i = arrayIndex; i < _size; i++)
					_array[i] = _array[i + 1];
			}
			_size--;
		}

		/// <summary>
		/// quick remove the item
		/// switch with the last item
		/// </summary>
		/// <param name="item"></param>
		public void QuickRemove(T item)
		{
			if (item == null || _size == 0)
				return;
			
			int arrayIndex = IndexOf(item);

			QuickRemoveAt(arrayIndex);
		}

		/// <summary>
		/// quick remove the item at arrayIndex
		/// switch with the last item
		/// </summary>
		/// <param name="arrayIndex"></param>
		public void QuickRemoveAt(int arrayIndex)
		{
			if (arrayIndex >= _size || arrayIndex < 0)
			{
				Debug.LogError("arrayIndex out of range.");
				return;
			}

			if (arrayIndex == _size - 1)
				_array[arrayIndex] = default(T);
			else
			{
				_array[arrayIndex] = _array[_size - 1];
				_array[_size - 1] = default(T);
			}
			_size--;
		}

		/// <summary>
		/// copy all items to another dynamic array
		/// </summary>
		/// <param name="dynamicArray"></param>
		public void CopyTo(DynamicArray<T> dynamicArray)
		{
			T[] arr = dynamicArray._array;

			CopyTo(arr);
		}

		/// <summary>
		/// copy all items to another array
		/// </summary>
		/// <param name="array"></param>
		public void CopyTo(T[] array)
		{
			if (array.Length < _array.Length)
			{
				Debug.LogError("arrayIndex out of range.");
				return;
			}

			for (int i = 0; i < _size; i++)
				array[i] = _array[i];
		}

		/// <summary>
		/// copy items to another dynamic array
		/// start from arrayIndex 
		/// </summary>
		/// <param name="dynamicArray"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(DynamicArray<T> dynamicArray, int arrayIndex)
		{
			T[] arr = dynamicArray._array;

			CopyTo(arr, arrayIndex);
		}

		/// <summary>
		/// copy items to another array
		/// start from arrayIndex
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array.Length < _array.Length - arrayIndex || arrayIndex >= _size || arrayIndex < 0)
			{
				Debug.LogError("arrayIndex out of range.");
				return;
			}

			for (int i = 0; i < _size - arrayIndex; i++)
				array[i] = _array[i + arrayIndex];
		}

		/// <summary>
		/// clear
		/// </summary>
		public void Clear()
		{
			_array = null;
			_size = 0;
		}
	}
}

