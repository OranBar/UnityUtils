using System;

namespace OranDataStructures
{
	public class AStarPriorityNode<T> {

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
		public T ParentPointer{get;set;}

		public AStarPriorityNode(T item, T parent, int index, float priority){
			this.Item = item;
			this.Priority = priority;
			this.Index = index;
			this.ParentPointer = parent;
		}

		/*
		public AStarPriorityNode(T item, T parent, float priority){
			this.Item = item;
			this.Priority = priority;
			this.ParentPointer = parent;
		}
		 */		

	}
}

