using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMouseCursor : MonoBehaviour {
    public Texture2D cur;
    public CursorMode cursorMode = CursorMode.Auto;

    // Use this for initialization
    void Start () {
        
        Cursor.SetCursor(cur, new Vector2(cur.width / 2, cur.height / 2), cursorMode);
	}
}
