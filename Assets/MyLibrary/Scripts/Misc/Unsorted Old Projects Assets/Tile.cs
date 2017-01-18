using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
public enum Highlight{
	Default=0, Move=1, Attack=2
}
*/
using System;

public struct HighlightColors{
	//Don't use the default. That's too rigid, new hex, you have to change this
	public static Color Default = Color.white;

	public static Color Move = Color.cyan;
	public static Color Attack = Color.red;
	public static Color Defense = Color.green;
		
};

public class Tile : MonoBehaviour {

	//	public int x; public int y; public int z; //Used for Unity in-editor debugging
	public bool mouseOverCoordinates=true;

	public GameObject CharacterOnTile {get; set;}
	public BattleAction AttackOnTile {get;set;}
	public Color CurrentHighlight{get;set;}

	private bool _walkable;
	private Color defaultColor;
	private Material material;
	private float tileWidth;
	private float tileHeight;
	private int sortingOrder;
	private Grid grid;

	// Axial coordinates is a system of storing hexagons that uses 3 axis. The third Axis is used for computation only. 
	// http://www.redblobgames.com/grids/hexagons/#coordinates
	
	//Axial Coordinates
	public int X {get;set;} 
	public int Y {get;set;} 	
	public int Z {get;set;}
	
	public int Height {get;set;} //Tile height or level.

	public bool Movable { 
		get{ return Walkable && CharacterOnTile==null; }
		private set{ Debug.LogWarning("This setter does nothing"); }
	} 

	public bool Walkable {
		get{ return _walkable; }
		set{
			_walkable=value;
			if(!_walkable){
				CurrentHighlight = defaultColor = material.color = Color.gray;	//TODO: remove this line when adding obstacles
//				Movable=false;
			}
		} 
	}

	/*
	public void SetCharacter(AbsCharacter character){
		if(!Movable){
			throw new Exception(character.name+" can't be on "+ToString()+" because it is not movable");
		}
		if(CharacterOnTile!=null){
			throw new Exception(character.name+" can't be on "+ToString()+" because character"+CharacterOnTile.name+" is already on the tile");
		}

		CharacterOnTile = character.gameObject;

//		Movable = false;
	}
	*/

	void Awake() {
		this.gameObject.layer = LayerMask.NameToLayer("HexTiles");
		grid = FindObjectOfType<Grid>();

		CharacterOnTile = null;
		material = this.GetComponent<Renderer>().material;
		defaultColor = HighlightColors.Default;
		CurrentHighlight = defaultColor;

		SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
		sortingOrder = spriteRenderer.sortingOrder;
		tileWidth = (spriteRenderer.bounds.size.x);
		tileHeight = (spriteRenderer.bounds.size.x);
		Walkable=true;	//TODO: When bringing obstacles in, this line will have to be changed
	//	Movable=true;	//TODO: When bringing obstacles in, this line will have to be changed
	}

	void Start () {

	}

	void OnMouseEnter () {
		if(mouseOverCoordinates) {
			Debug.Log ("x: "+X+"| y: "+Y);
		}
		this.GetComponent<Renderer>().material.color = Color.yellow;
	}

	void OnMouseExit(){
		this.GetComponent<Renderer>().material.color = CurrentHighlight;
	}

	public int GetDistance(Tile start, Tile goal) {
		return grid.GetDistance(start, goal);
	}

	public Vector3 GetCoordinates(){
		return new Vector3(X, Y, Z);
	}

	public void StoreAxialCoordinates(int x, int y) {
		X = x;
		Y = y;
		Z = -x-y;
	}

	// Method for positioning at the beginning of the fight
	public void PlaceCharacterOnTile(GameObject gameObj) {
		Vector3 onTilePosition = GetCenteredPosition();

		CharacterOnTile = gameObj;
		AbsCharacter characterScript = gameObj.GetComponent<AbsCharacter>();

		gameObj.transform.position = onTilePosition;
		gameObj.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 1;
		gameObj.GetComponent<Character>().CurrentTile = this;
//		Movable=false;
	}

	public Vector3 GetCenteredPosition(){
		Vector3 centeredPosition = this.transform.position;
		centeredPosition.y += tileHeight/4 ;
		return centeredPosition;
	}

	public void Highlight(Color highlightColor){
		if(!Walkable){ return; }

		//material.color = highlightToTurnOn;
		this.GetComponent<Renderer>().material.color = highlightColor;
		CurrentHighlight = highlightColor;
	}

	public void HighlightOff(){
		// material.color = defaultColor;
		this.GetComponent<Renderer>().material.color = defaultColor;
		CurrentHighlight = defaultColor;
	}

	public override string ToString(){
		return "X: "+X+", Y:"+Y;
	}
}
