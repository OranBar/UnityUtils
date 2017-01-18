using System;
using NUnit.Framework;

namespace OranDataStructures
{
	[TestFixture()]
	public class PriorityQueueTest
	{
		PriorityQueue<String> heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator((first, second) => first > second));
		PriorityQueue<String> tileHeap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator((first, second) => first > second));


		[Test()]
		public void TestAdd (){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first > second) );
			heap.Add("Uno",1);
			Assert.AreEqual(1, heap.Size);
			heap.Add("Due", 1);
			Assert.AreEqual(2, heap.Size);
		}

		[Test()]
		public void TestPop(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first> second) );

			heap.Add("Uno",1);
			String pop = heap.Pop();

			Assert.AreEqual("Uno", pop );
			Assert.AreEqual(0, heap.Size);
		}
	
		[Test()]
		public void TestPriorityPop(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first > second) );
			
			heap.Add("Uno",1);
			heap.Add("Due",2);

			String pop = heap.Pop();
			Assert.AreEqual("Due", pop);

			pop = heap.Pop();
			Assert.AreEqual("Uno", pop);
		}

		
		[Test()]
		public void TestContains(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first > second) );
			
			heap.Add("Uno",1);
			heap.Add("Due",2);
			heap.Add("Tre",3);
			
			Assert.AreEqual(true, heap.Contains("Uno"));
			Assert.AreEqual(true, heap.Contains("Due"));
		}

		[Test()]
		public void TestUpHeap(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first > second) );

			heap.Add("Uno",1);
			heap.Add("Due",2);
			heap.Add("Tre",3);
			heap.Add("Quattro",4);
			heap.Add("Cinque",5);

			Assert.AreEqual("Cinque", heap.Get(0));
			Assert.AreEqual("Quattro", heap.Get(1));
			Assert.AreEqual("Due", heap.Get(2));
			Assert.AreEqual("Uno", heap.Get(3));
			Assert.AreEqual("Tre", heap.Get(4));
		}

		[Test()]
		public void TestDownHeap(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => first > second) );

			heap.Add("Uno",1);
			Console.WriteLine(heap);
			heap.Add("Due",2);
			Console.WriteLine(heap);
			heap.Add("Tre",3);
			Console.WriteLine(heap);
			heap.Add("Quattro",4);
			Console.WriteLine(heap);
			heap.Add("Cinque",5);

			Console.WriteLine(heap);
			Assert.AreEqual("Cinque", heap.Pop());
			Console.WriteLine(heap);
			Assert.AreEqual("Quattro", heap.Pop());
			Console.WriteLine(heap);
			Assert.AreEqual("Tre", heap.Pop());
			Console.WriteLine(heap);
			Assert.AreEqual("Due", heap.Pop());
			Console.WriteLine(heap);
			Assert.AreEqual("Uno", heap.Pop());
			Console.WriteLine(heap);
		}

		[Test()]
		public void ReverseTest(){
			heap= new PriorityQueue<String>(new PriorityQueue<string>.PriorityComparator( (first, second) => false) );

			heap.Add("Uno",1);
			heap.Add("Due",1);
			heap.Add("Tre",1);
			heap.Add("Quattro",1);
			heap.Add("Cinque",1);

			heap.Reverse();

			Assert.AreEqual("Cinque", heap.Get(0));
			Assert.AreEqual("Quattro", heap.Get(1));
			Assert.AreEqual("Tre", heap.Get(2));
			Assert.AreEqual("Due", heap.Get(3));
			Assert.AreEqual("Uno", heap.Get(4));
		}





	}
}

