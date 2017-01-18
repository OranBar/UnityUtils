using System;

namespace OranDataStructures
{
	public class PriorityQueueNode<T>{

		public T Item{get;private set;}
		public float Priority{get;set;}
		public int Index{
			get{
				return _index;
			}
			set{
				_index=value;
			}
		}
		private int _index;
	
		public PriorityQueueNode (T item, float priority)	{
			this.Item = item;
			this.Priority = priority;
		}

		public PriorityQueueNode (T item, float priority, int index)	{
			this.Item = item;
			this.Priority = priority;
			this.Index = index;
		}
	}
}
