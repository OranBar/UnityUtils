using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Performance: If you want to improve performance, take away the OnMouseEnter script from all the tiles, and compute that shit here
//Performance: Check if raycasting twice into different layers is more efficient that doing a single raycast that hits everything

// This class handles events in a static manner. It does not need to know anything about others, nor take input from them. It just tracks the events and fires the methods. 
// TODO: remove static 
public class MouseRayEvents : MonoBehaviour {


	#region Singleton
	public static MouseRayEvents Instance{ get; private set;}

	public void Awake(){
		if(Instance != null){
			Debug.LogError("There are two instances of MouseRayEvents");
		}
		Instance = this;
	}
	#endregion

	public delegate void TileClick(Tile tile);
	public event TileClick OnTileClick;
	
	public delegate void CharClick(AbsCharacter character);
	public event CharClick OnCharClick;
	
	
	// Use this for initialization
	void Start () {
		
	}
	/*
	// Update is called once per frame
	void Update () {
		//Early out
		if(OnTileClick==null && OnCharClick==null){
			return;
		}
		
		if(Input.GetMouseButtonDown(0)){
			// Performance: Maybe using 2 rays with different layers is better than using a single ray and doing checks on the layers
			Collider2D collider =  RayCameraToMouse();
			if(collider==null){ return; }
			GameObject clicked = collider.gameObject;
			if(clicked.layer == LayerMask.NameToLayer("HexTiles")){
				if(OnTileClick!=null){
					OnTileClick(clicked.GetComponent<Tile>());
				}
			}
			if(clicked.layer == LayerMask.NameToLayer("Characters")){
				if(OnCharClick!=null){
					OnCharClick(clicked.GetComponent<Character>());
				}
			}
		}
	}
*/
	void Update () {
		//Early out
		if(OnTileClick==null && OnCharClick==null){
			return;
		}
		
		if(Input.GetMouseButtonDown(0)) {
			Collider2D hit = RayCameraToMouse();
			if(hit != null){
				string logMessage = hit.name+" clicked";
				//Log(hit.name+" clicked");
				if(hit.gameObject.layer == LayerMask.NameToLayer("Characters")){
					if(OnCharClick!=null){
						Log(logMessage + " (Character)");
				//		Log("Char clicked");
						OnCharClick(hit.GetComponent<AbsCharacter>());
					}
				}
				if(hit.gameObject.layer == LayerMask.NameToLayer("HexTiles")){
					if(OnTileClick!=null){
						Log(logMessage + " (Tile)");
						//Log("Tile clicked");
						OnTileClick(hit.GetComponent<Tile>());
					}
				}
			}

			/*
			// Unfortunarely raycasting with a layermask is not working for me
			// Performance: Maybe using 2 rays with different layers is better than using a single ray and doing checks on the layers
			// Collider2D characterHit = RayCameraToMouse( LayerMask.NameToLayer("Characters") ); 
			Collider2D characterHit = MouseRaycastHit( LayerMask.NameToLayer("Characters") ); 

			if(characterHit != null){
				Debug.Log("characterHit");
				if(OnCharClick!=null){
					Debug.Log("Mouse Ray Events : Char clicked");
					OnCharClick(characterHit.GetComponent<AbsCharacter>());
					return;
				}
			}

			// Collider2D hexHit =  RayCameraToMouse(LayerMask.NameToLayer("HexTiles"));
			Collider2D hexHit =  MouseRaycastHit( LayerMask.NameToLayer("HexTiles") );
			if( hexHit != null ){
				Debug.Log("hexHit");
				if(OnTileClick!=null){
					Debug.Log("Mouse Ray Events : Tile clicked");
					OnTileClick(hexHit.GetComponent<Tile>());
				}
			}
		*/
		}
	}
	
	
	public Collider2D RayCameraToMouse(){
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 5f;
		//	Vector2 mousePosition2D = Camera.main.WorldToScreenPoint(mousePosition);
		Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition);
		
		Collider2D rayhit = Physics2D.OverlapPoint(mousePosition2D);
		return rayhit; 
	}
	
	public Collider2D RayCameraToMouse(int layerMask){
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 5f;
	//	Vector2 mousePosition2D = Camera.main.WorldToScreenPoint(mousePosition);
		Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(mousePosition);

		Collider2D rayhit = Physics2D.OverlapPoint(mousePosition2D, 1 << layerMask);
		return rayhit; 
	}

	// Methods used to try to raycast with a layermask. Apparently not working
	public Collider2D MouseRaycastHit(){

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		return hit.collider;
	}

	public Collider2D MouseRaycastHit(int layerMask){
		
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

		return hit.collider;
	}

	protected void Log(object message){
		Debug.Log("<color=gray><b>Mouse Ray Events : </b></color>"+message+"");
	}
}
