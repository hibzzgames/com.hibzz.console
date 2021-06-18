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

		public CyclicQueue(int size)
		{
			max_size = size;
		}

		public new void Enqueue(T item)
		{
			if(this.Count == max_size) { this.Dequeue(); }
			base.Enqueue(item);
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