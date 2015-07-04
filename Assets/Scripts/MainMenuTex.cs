using UnityEngine;
using System.Collections;

public class MainMenuTex : MonoBehaviour {
	public Texture2D mainMenu;

	// Use this for initialization
	void Start () {
		Camera.main.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), mainMenu);
//		GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), mainMenu, ScaleMode.ScaleToFit, true, 1.75f);
	}
}
