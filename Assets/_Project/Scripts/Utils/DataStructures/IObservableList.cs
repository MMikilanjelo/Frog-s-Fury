using System;
using System.Collections.Generic;
using System.Collections;

namespace Game.Utils {
	/// <summary>
	/// Initializes a new instance of the ObservableList class that is empty
	/// or contains elements copied from the specified list_.
	/// </summary>
	/// <param name="initialList"> The list_ to copy elements from. </param>    
	public interface IObservableList<T> {
		/// <summary>
		/// Adds an item to the ObservableList.
		/// </summary>
		/// <param name="item">
		/// The item to add.
		/// </param>        
		void Add(T item);

		/// <summary>
		/// Inserts an item into the ObservableList at the specified index.
		/// </summary>
		/// <param name="index"> The zero-based index at which the item should be inserted. </param>
		/// <param name="item"> The item to insert into the ObservableList. </param>        
		void Insert(int index, T item);

		/// <summary>
		/// Removes all items from the ObservableList.
		/// </summary>        
		void Clear();

		/// <summary>
		/// Determines whether the ObservableList contains a specific item.
		/// </summary>
		/// <param name="item"> The item to locate. </param>        
		bool Contains(T item);

		/// <summary>
		/// Determines the index of a specific item in the IonObservableList.
		/// </summary>
		/// <param name="item"> The item to locate in the IonObservableList. </param>
		int IndexOf(T item);

		/// <summary>
		/// Copies the elements of the ObservableList to an array, starting at a particular index.
		/// </summary>
		/// <param name="array"> The one dimensional array that is the destination of the elements
		/// copied from the ObservableList. </param>
		/// <param name="arrayIndex"> The zero-based index in the array at which copying begins. </param>        
		void CopyTo(T[] array, int arrayIndex);

		/// <summary>
		/// Removes the first occurrence of a specific item from the ObservableList.
		/// </summary>
		/// <param name="item"> The item to remove from the ObservableList. </param>        
		bool Remove(T item);

		/// <summary>
		/// Returns a generic enumerator that iterates through the ObservableList.
		/// </summary>
		/// <returns>A generic enumerator that can be used to iterate through the collection.</returns>        
		IEnumerator<T> GetEnumerator();

		/// <summary>
		/// Removes the item at the specified index of the ObservableList.
		/// </summary>
		/// <param name="index"> The zero-based index of the item to remove. </param>        
		void RemoveAt(int index);
	}

	[Serializable]
	public class ObservableList<T> : IList<T>, IObservableList<T> {
		private readonly IList<T> list_;
		public event Action<IList<T>> AnyValueChanged;

		public ObservableList(IList<T> initialList = null) {
			list_ = initialList ?? new List<T>();
		}

		public T this[int index] {
			get => list_[index];
			set {
				list_[index] = value;
				Invoke();
			}
		}

		public void Invoke() => AnyValueChanged?.Invoke(list_);

		public int Count => list_.Count;

		public bool IsReadOnly => list_.IsReadOnly;

		public void Add(T item) {
			list_.Add(item);
			Invoke();
		}

		public void Clear() {
			list_.Clear();
			Invoke();
		}

		public bool Contains(T item) => list_.Contains(item);

		public void CopyTo(T[] array, int arrayIndex) => list_.CopyTo(array, arrayIndex);

		public bool Remove(T item) {
			var result = list_.Remove(item);
			if (result) {
				Invoke();
			}

			return result;
		}

		public IEnumerator<T> GetEnumerator() => list_.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => list_.GetEnumerator();

		public int IndexOf(T item) => list_.IndexOf(item);

		public void Insert(int index, T item) {
			list_.Insert(index, item);
			Invoke();
		}

		public void RemoveAt(int index) {
			T item = list_[index];
			list_.RemoveAt(index);
			Invoke();
		}
	}
}
