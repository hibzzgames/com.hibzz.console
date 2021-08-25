/* Description: A cyclic queue with a configured fixed size
 * Author:		Hibnu Hishath (sliptrixx)
 * Data:		18 June, 2021
 */

using System.Collections;
using System.Collections.Generic;

namespace Hibzz.Console
{
	public class CyclicQueue<T> : Queue<T>
	{
		protected int max_size = 0;

		/// <summary>
		/// Gets the last element added to the queue
		/// </summary>
		public T Last { get; protected set; }

		public CyclicQueue(int size)
		{
			max_size = size;
		}

		/// <summary>
		/// Adds the element to the end of the cyclic queue
		/// </summary>
		/// <param name="item"> The item to add </param>
		public new void Enqueue(T item)
		{
			// if the cyclic has reached maximum size, remove the first element
			// and add the item as a new element
			if(this.Count == max_size) { this.Dequeue(); }
			base.Enqueue(item);

			// store the given item as the last item
			Last = item;
		}

		/// <summary>
		/// Change the max size of the cyclic queue at runtime
		/// </summary>
		/// <param name="size"></param>
		public void ChangeSize(int size)
		{
			max_size = size;

			// if there are less items than the new fixed size, then we are clear
			if(this.Count <= max_size) { return; }

			// if not, we must dequeue unused content
			int countToRemove = this.Count - max_size;
			for(int i = 0; i < countToRemove; ++i)
			{
				this.Dequeue();
			}

		}
	}
}