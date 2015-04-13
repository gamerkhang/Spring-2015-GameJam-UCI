using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour {
	public Texture2D cursor_img;
	public Texture2D cursor_left;
	public Texture2D cursor_right;
	Texture2D current_img;
	public static bool CursorChanging = false;
	public static int CursorType = 0;
	CursorMode cursorMode = CursorMode.ForceSoftware;
	Vector2 hotSpot = Vector2.zero;
	
	
	
	// Use this for initialization
	void Start () {
		current_img = cursor_img;
		//hotSpot.y = 0;
		//hotSpot.x = 0;
		Cursor.SetCursor(cursor_img, hotSpot, cursorMode);

	}

	void Update(){
		if (CursorChanging) {
			switch(CursorType){
			case 0:
				current_img = cursor_img;
				break;
			case 1:
				current_img = cursor_right;
				break;
			case 2:
				current_img = cursor_left;
				break;
			}
			Cursor.SetCursor(current_img, hotSpot, cursorMode);
			CursorChanging = false;
		}
	}

	public static void ChangeBack(){
		CursorType = 0;
		CursorChange.CursorChanging = true;
	}

	public static void RightClick(){
		CursorType = 1;
		CursorChange.CursorChanging = true;
	}

	
	public static void LeftClick(){
		CursorType = 2;
		CursorChange.CursorChanging = true;
	}
}
