using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GridFactory : MonoBehaviour {

	public Vector2 cameraAnchor;
	public GameObject hexPrefab;
	public int rows=10, columns=10;
	public bool newGrid = false;
	public bool newPrefab = false;

	private Tile[,] gridMatrix;

	private float hexPixelHeight;
	private float hexPixelWidth;

	void Awake(){
		hexPixelHeight = hexPrefab.GetComponent<Renderer>().bounds.size.y;
		hexPixelWidth = hexPrefab.GetComponent<Renderer>().bounds.size.x;
	}

	public Tile[,] CreateGrid(){
		DeleteChildren();
		
		//Creating new empty GameObj to group all the tiles
		GameObject level0 = new GameObject();
		level0.name = "Level 0";
		level0.transform.parent = this.transform;
		
		gridMatrix = new Tile[columns + rows/2, rows];
		int centerX = (int) Mathf.Floor(columns /2);
		int centerY = (int) Mathf.Floor(rows /2);
		int xOffset = ((rows+1)/2);
		int xMaxOffset = xOffset-1;
		
		// Makes a rowsXcolumns grid, every tile is at level 0
		for(int y=0; y<rows; y++){
			if(y%2==0){ xOffset--; }
			
			for(int x=0; x< columns; x++){
				
				Vector3 hexPosition = ComputeHexOnScreenPosition(x-centerX, y - centerY );
				GameObject newHex = Instantiate(hexPrefab, hexPosition, Quaternion.identity) as GameObject;
				Tile newHexScript = newHex.GetComponent<Tile>();
				
				newHex.transform.parent = level0.transform;	//Groups the tile under an empty GameObj
				newHex.layer = LayerMask.NameToLayer("HexTiles");
				
				// Axial Storage
				newHexScript.StoreAxialCoordinates(x+xOffset-xMaxOffset, y);
				
				int[] indexes = ConvertAxialToIndexes(x+xOffset-xMaxOffset, y);
				gridMatrix[indexes[0], indexes[1]] = newHexScript;
			}
		}

		if(newPrefab){
			CreateGridPrefab();
		}
		return gridMatrix;
	}

	private void DeleteChildren(){
		var children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}

	private Vector3 ComputeHexOnScreenPosition(int column, int row){
		float x = column * hexPixelWidth;
		float y = row * (3f/4f) * hexPixelHeight;
		if(Mathf.Abs(row)%2==1){
			x = x - hexPixelWidth/2f;
		}
		return new Vector3(x,-y,0f);
	}

	private int[] ConvertAxialToIndexes(int x, int y){
		int[] indexes = new int[2];
		indexes[0] = x + ((rows-1)/2) /*-1*/;
		indexes[1] = y;
		return indexes;
	}

	void CreateGridPrefab (){
		//Resetting the bools, so the prefab is clean
		newGrid = false;
		newPrefab = false;
		
		GameObject grid = GameObject.Find ("Grid");
		if (grid == null) {
			Debug.Log ("Grid not found");
		}
		PrefabUtility.CreatePrefab ("Assets/Prefabs/Grid.prefab", grid);
	}

	private void ScaleCamera(){
		//TODO: Scale based on cameraAnchor, or i don't know, just fix the camera somehow

		float y = hexPixelHeight * columns/2;
		float biggerSideLength = Mathf.Max(columns, rows); 
		
		// Proportion --  biggerSideLength : orthographicSize = 10.5 : 12 
		Camera.main.orthographicSize = 12 * biggerSideLength * 0.0952380952f;	//Using mult for efficiency
	}
}
