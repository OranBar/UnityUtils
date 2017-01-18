using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Maybe I should have extended List<T>
namespace OranDataStructures
{
	public class PriorityQueue<T> {

		protected List<PriorityQueueNode<T>> heap;
		public int Size{
			get{ return _size;	}
			private set{ _size = value;}
		}
		protected int _size;

		public delegate bool PriorityComparator(float firstPriority, float secondPriority);

		private PriorityComparator comparator;

		public PriorityQueue(PriorityComparator comparator){
			heap = new List<PriorityQueueNode<T>>();
			heap.Capacity=36;
			Size = 0;
			this.comparator = comparator;
		}

		public T Get(int index){
			return heap[index].Item;
		}

		public virtual PriorityQueueNode<T> GetNode(int index){
			return heap[index];
		}

		public virtual PriorityQueueNode<T> SearchNode(T item){
			PriorityQueueNode<T> searchResult = null;
			foreach(PriorityQueueNode<T> node in heap){
				if(node.Item.Equals(item)){
					searchResult = node;
					break;
				}
			}
			return searchResult;
		}

		public float PeekPriority(){
			return heap[0].Priority;
		}

		public virtual void Add(T item, float priority){
			Size++;
			PriorityQueueNode<T> newNode = new PriorityQueueNode<T>(item, priority, Size-1);
			heap.Add(newNode);
			newNode.Index = heap.IndexOf(newNode);
			if(Size>=2){
				Upheap();
			}
		}

		public T Pop(){
			PriorityQueueNode<T> highestPriorityElement = heap[0];
			if(Size>=2){
				heap[0] = heap[Size-1];
				heap[0].Index = 0;
				heap.RemoveAt(Size-1);
			} else {
				heap.RemoveAt(0);
			}
			Size--;
			if(Size>=2){
				DownHeap ();
			}
			return highestPriorityElement.Item;
		}

		public void Remove(T item){

		}

		public virtual bool Contains(T item){
			bool isInHeap=false;
			foreach(PriorityQueueNode<T> node in heap){
				if(node.Item.Equals(item)){
					isInHeap=true;
					break;
				}
			}
			return isInHeap;
		}

		protected void DownHeap(){
			int i=0;
			while (i<=(Size-1)/2){
				PriorityQueueNode<T> node = heap[i];
			
				PriorityQueueNode<T> biggerChild = GetBiggerChild(GetChildren(i));
				if(biggerChild==null){
					break;
				}

				if(comparator(biggerChild.Priority, node.Priority)){
					Swap(biggerChild.Index, node.Index);
					i = node.Index;
				} else {
					break;
				}
			}
		}

		protected void Upheap(){
			int i=Size-1;

			while(i>=0){
				PriorityQueueNode<T> node = heap[i];
				PriorityQueueNode<T> father = GetFather(node.Index);


				if(comparator(node.Priority, father.Priority)){
					Swap(node.Index, father.Index);
					i = node.Index;

				} else {
					break;
				}
			}
		}


		protected void BottomUp(){
			for(int i=Size/2 -1; i>=0; i++){
				PriorityQueueNode<T> father = heap[i];

				List<PriorityQueueNode<T>> children = GetChildren(i);
				PriorityQueueNode<T> biggerChild = GetBiggerChild(children);


				if(comparator( biggerChild.Priority, father.Priority )){
					Swap(biggerChild.Index, father.Index);
				}
			}
		}
		
		protected void Swap(int firstElementIntex, int secondElementIndex){
			if(firstElementIntex<0 || firstElementIntex>=Size){
				Debug.Log("firstElementIntex: "+firstElementIntex+" out of range. Size is: "+Size);
			}
			if(secondElementIndex<0 || secondElementIndex>=Size){
				Debug.Log("secondElementIndex: "+secondElementIndex+" out of range");
			}

			PriorityQueueNode<T> firstElement = heap[firstElementIntex];
			PriorityQueueNode<T> secondElement = heap[secondElementIndex];

			heap[firstElementIntex] = secondElement;
			heap[secondElementIndex] = firstElement;

			firstElement.Index = secondElementIndex;
			secondElement.Index = firstElementIntex;
		}

		protected List<PriorityQueueNode<T>> GetChildren(int fatherIndex){
			List<PriorityQueueNode<T>> children = new List<PriorityQueueNode<T>>();
			if(fatherIndex*2 +1<=Size-1){
				children.Add(heap[fatherIndex*2 +1]);
			}
			if(fatherIndex*2 +2<=Size-1){
				children.Add(heap[fatherIndex*2 +2]);
			}
			
			return children;
		}

		protected PriorityQueueNode<T> GetBiggerChild(List<PriorityQueueNode<T>> children){
			PriorityQueueNode<T> biggerChild = null;
			if(children.Count==1){
				biggerChild = children[0];
			}
			if(children.Count==2){
				biggerChild = ( comparator(children[0].Priority, children[1].Priority) )? children[0] : children[1];
			}
			return biggerChild;
		}

		protected PriorityQueueNode<T> GetFather(int i){
			return heap[(i-1)/2];
		}


		public void Clear(){
			heap = new List<PriorityQueueNode<T>>();
		}

		public override string ToString (){
			String heapString = "";
			for(int i=0; i<Size; i++){
				heapString += heap[i].Item+" ";
			}
			return heapString;
		}

		public void Reverse(){
			for(int i=0; i<Size; i++){
				if(i>=Size-1-i){
					break;
				}
				PriorityQueueNode<T> temp = heap[i];
				heap[i] = heap[Size-1-i];
				heap[Size-1-i] = temp;
			}
		}

		public List<T> ConvertToList(){
			List<T> list = new List<T>();

			for(int i=0; i<Size; i++){
				list[i] = Get(i);
			}
			return list;
		}
	}
}

