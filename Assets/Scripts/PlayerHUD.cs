using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	private float halfScreenW;
	private float halfScreenH;
	public Texture2D crosshairs;

	// Use this for initialization
	void Start () {
		halfScreenW = Screen.width / 2;
		halfScreenH = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(halfScreenW - 12, halfScreenH - 12, 24, 24), crosshairs, ScaleMode.ScaleToFit, true, 0);
	}
}
