using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using OranDataStructures;

/* 	TODO: Tiles on the same coordinates but with height: The highest tile override all the other ones, and is considered the only true tile
 *  belonging to thoose coordinates.
 */
public enum Direction{
	Right=0, Top_Right, Top_Left, Left, Bottom_Left, Bottom_Right
}

public class Grid : MonoBehaviour {

/*
	//Lazy Singleton
	private static Grid grid;
	public static Grid GetGrid(){
		return grid;
	}
*/

	public int rows{get;private set;}
	public int columns{get;private set;}

	private GridFactory gridFactory;

	private Tile[,] gridMatrix;

	private Vector2[] directions = {
		new Vector2(+1, 0), new Vector2(+1,  -1), new Vector2( 0, -1),
		new Vector2(-1, 0), new Vector2(-1,  +1), new Vector2( 0, +1) 
	};

	public Tile GetGridTile(int x, int y){
		int []indexes = ConvertAxialToIndexes(x,y);
		if(x<=-rows/2 || x>=columns || y<0 || y>=rows){
			return null;
		} 
		return gridMatrix[indexes[0],indexes[1]];
	}
	private void SetGridTile(Tile tile){
		int []indexes = ConvertAxialToIndexes(tile.X,tile.Y);
		gridMatrix[indexes[0],indexes[1]] = tile;
	}

	private void SetGridTile(Tile tile, int x, int y){
		tile.X = x;
		tile.Y = y;
		SetGridTile(tile);
	}



	void Awake(){
//		grid = this;
		gridFactory = GetComponent<GridFactory>();
		if(gridFactory==null){ Debug.Log("GridFactory not found");}
	}

	void Start () {
		gridMatrix = gridFactory.CreateGrid();
		rows = gridFactory.rows;
		columns = gridFactory.columns;

	
		ManuallySetGridAndPlayers();

	}

	private void ManuallySetGridAndPlayers(){
		
		GameObject playerObject1 = GameObject.Find("Marche 1 - Ai");
		if(playerObject1 == null){ Debug.Log("Couldn't find GameObject");}
		
		GetGridTile(4,4).PlaceCharacterOnTile(playerObject1);

		GameObject playerObject2 = GameObject.Find("Marche 2 - Player");
		if(playerObject2 == null){ Debug.Log("Couldn't find GameObject");}
		
		GetGridTile(4,7).PlaceCharacterOnTile(playerObject2);

		GetGridTile(6,3).Walkable=false;
		GetGridTile(5,4).Walkable=false;
		GetGridTile(5,5).Walkable=false;
	}

	private int[] ConvertAxialToIndexes(int x, int y){
		int[] indexes = new int[2];
		indexes[0] = x + ((rows-1)/2);
		indexes[1] = y;
		return indexes;
	}

	public int GetDistance(Tile start, Tile end){
		return (Mathf.Abs(start.X - end.X) + Mathf.Abs(start.Y - end.Y) + Mathf.Abs(start.Z - end.Z)) /2;
	}

	//This method could be optimized by passing a function. The function can be used on the tiles, without additional complexity
	public List<Tile> RadiusTiles(Tile center, int radius){
		List<Tile> inRangeTiles = new List<Tile>();
		Vector3 temp = center.GetCoordinates();
		Vector2 centralTileCoordinates = new Vector2(temp.x, temp.y);

		Vector2 neighbour;
		Tile neighbourTile;

//		inRangeTiles.Add(center);

		for(int i=1; i<=radius; i++){	//Once for every ring
			neighbour = centralTileCoordinates + (directions[4] * i);
			for(int j=0; j<6; j++){		//Once for every direction
				for(int k=1; k<=i; k++){
					neighbour = neighbour + directions[j];
					neighbourTile = GetGridTile((int)neighbour.x, (int)neighbour.y);
					if(neighbourTile!=null){
						inRangeTiles.Add(neighbourTile);
					}
				}
			}
		}
		return inRangeTiles;
	}

	public List<Tile> DjikstraRadius(Tile center, int movement){
		PriorityQueue<Tile> frontier = new PriorityQueue<Tile>(new PriorityQueue<Tile>.PriorityComparator((first, second) => first < second) );
		List<Tile> movableTiles = new List<Tile>();
	//	movableTiles.Add (center);
		frontier.Add(center,0);
		
		while(frontier.Size > 0){
			float distance = frontier.PeekPriority();
			Tile current = frontier.Pop();
			
			movableTiles.Add (current);

			List<Tile> currentNeighbours = RadiusTiles(current, 1);
			
			foreach(Tile neighbour in currentNeighbours){
				float totalDistance = distance + GetDistance(current, neighbour);

				//movableTiles list becomes really really bit. I think contains does not work. 
				if(neighbour.Walkable && totalDistance <= movement && !movableTiles.Contains(neighbour)){
					frontier.Add(neighbour, totalDistance);
				}
			}
		}
		movableTiles.Remove(center);
		return movableTiles;
	}

	public void HighlightTiles(List<Tile> tilesList, Color highlightStaticVar){
		foreach(Tile current in tilesList){
			current.Highlight(highlightStaticVar);
		}
	}

	public void HighlightsOff(){
		foreach(Tile currentTile in gridMatrix){
			if(currentTile!=null){
				currentTile.HighlightOff();
			}
		}
	}

	public void HighlightTilesInDjikstraRadius(Tile center, int radius, Color highlightColor){
		List<Tile> movableTiles = DjikstraRadius(center, radius);
		HighlightTiles(movableTiles, highlightColor);
	//	StartCoroutine(DjikstraRadius_Coro(center, radius));
	}

	public List<Tile> InLineTiles(Tile startTile, Tile finishTile){
		List<Tile> inLineTiles = new List<Tile>();

		return null;
	}

	// A* Algorithm
	public List<Tile> A_StarPathfinding(Tile startTile, Tile endTile){
		AStarPriorityQueue<Tile> frontier = new AStarPriorityQueue<Tile>(new AStarPriorityQueue<Tile>.PriorityComparator((first, second) => first < second) );
		AStarPriorityQueue<Tile> visited = new AStarPriorityQueue<Tile>(new AStarPriorityQueue<Tile>.PriorityComparator((first, second) => false ));
//		visited.Add (startTile, null, 1);
		frontier.Add(startTile, null, 0);

//		Tile previous=null;
		while(frontier.Size > 0){
		
			float distance = frontier.PeekPriority();
//			Tile current = frontier.PopNode();
//			visited.Add (current, previous/*ERROR*/, 1);
			AStarPriorityNode<Tile> currentNode = frontier.PopNode();
			visited.Add(currentNode);
			Tile current = currentNode.Item;

			if(current==endTile){
				break;
			}

			List<Tile> currentNeighbours = RadiusTiles(current, 1);
			
			foreach(Tile neighbour in currentNeighbours){
				float totalDistance = GetDistance(startTile, current) + GetDistance(current, neighbour);
				float distanceFromGoal = GetDistance(neighbour, endTile);
				float distancesSum = totalDistance + distanceFromGoal;
				
				if(neighbour.Walkable && !frontier.Contains(neighbour) && !visited.Contains(neighbour) ){
					frontier.Add(neighbour, current, distancesSum);
				}

			}
		}
		visited.Remove(startTile);

		return GetPathToGoal(visited);
	}

	private List<Tile> GetPathToGoal(AStarPriorityQueue<Tile> visited){
		List<Tile> pathToGoal = new List<Tile>();
		visited.Reverse();
		AStarPriorityNode<Tile> node = visited.GetNode(0);
		pathToGoal.Add(node.Item);

		int emergencyOut=0;
		while(true){
			emergencyOut++;
			if(emergencyOut>=1000){
				break;
			}
			Tile parent = node.ParentPointer;
			if(parent==null){
//				Debug.Log("Null parent, iteration:  "+emergencyOut);
				break;
			}
			pathToGoal.Add(parent);
			node = visited.SearchNode(parent);
		}
		pathToGoal.Reverse();
		return pathToGoal;
	}

	public List<Tile> MotherfuckingPathfinding(int x0, int y0, int x1, int y1){
		Tile startTile = GetGridTile(x0,y0);
		Tile endTile = GetGridTile(x1,y1);

		return A_StarPathfinding(startTile, endTile);
	}

	//Graphic version with frontier delimitation
	public IEnumerator DebugDjikstraRadius_Coro(Tile center, int movement){
		PriorityQueue<Tile> frontier = new PriorityQueue<Tile>(new PriorityQueue<Tile>.PriorityComparator((first, second) => first < second) );
		List<Tile> movableTiles = new List<Tile>();
		movableTiles.Add (center);
		frontier.Add(center,0);

		int emergencyOut=0;
		while(frontier.Size > 0){
			float distance = frontier.PeekPriority();
			Tile current = frontier.Pop();

			movableTiles.Add (current);
			current.Highlight(HighlightColors.Move);
			yield return new WaitForSeconds(0.1f);

			List<Tile> currentNeighbours = RadiusTiles(current, 1);

			foreach(Tile neighbour in currentNeighbours){
				emergencyOut++;

				float totalDistance = distance + GetDistance(current, neighbour);

				if(neighbour.Walkable && totalDistance <= movement && !movableTiles.Contains(neighbour)){
					frontier.Add(neighbour, totalDistance);
					//					Debug.Log("Adding to frontier: "+neighbour);
					neighbour.Highlight(HighlightColors.Attack);
					yield return new WaitForSeconds(0.1f);
				}
				if(emergencyOut>=2000){
					Debug.Log("Good job! Test was incredibly successful. No really. I mean it. Good boy");
					break;
				}
			}
		}
		movableTiles.Remove(center);
		//		return movableTiles;
	}

	/*
	public IEnumerator A_StarPathfinding_Coro(Tile startTile, Tile endTile){
		AStarPriorityQueue<Tile> frontier = new AStarPriorityQueue<Tile>(new AStarPriorityQueue<Tile>.PriorityComparator((first, second) => first < second) );
		List<Tile> pathToGoal = new List<Tile>();
		pathToGoal.Add (startTile);
		frontier.Add(startTile,0);
		
		while(frontier.Size > 0){
		
			float distance = frontier.PeekPriority();
			Tile current = frontier.Pop();
			pathToGoal.Add(current);
			current.Hightligth(Highlight.Move);

			if(current==endTile){
				break;
			}
			
			yield return new WaitForSeconds(0.1f);
			
			List<Tile> currentNeighbours = RadiusTiles(current, 1);
			
			foreach(Tile neighbour in currentNeighbours){
				float totalDistance = Tile.GetDistance(startTile, current) + Tile.GetDistance(current, neighbour);
				float distanceFromGoal = Tile.GetDistance(neighbour, endTile);
				float distancesSum = totalDistance + distanceFromGoal;
				
				if(neighbour.Passable && !frontier.Contains(neighbour) && !pathToGoal.Contains(neighbour)){
					frontier.Add(neighbour, current, distancesSum);
					neighbour.Hightligth(Highlight.Attack);
					yield return new WaitForSeconds(0.1f);
				}
			}
		}
		pathToGoal.Remove(startTile);
	}
	*/




}
