using UnityEngine;
using System.Collections;

public class MultiplayerHiddenGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("in hidden");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.depth = 1;
		if(GUI.Button(new Rect(Screen.width / 17,Screen.height / 1.235f, Screen.width / 4.5f, Screen.height / 10), "Join")){

		}

		if(GUI.Button(new Rect(Screen.width / 3.35f,Screen.height / 1.235f, Screen.width / 4.5f, Screen.height / 10), "Search")){
			Debug.Log (gameObject.GetComponent<MultiplayerVisibleGUI>().searchBox);
			gameObject.GetComponent<MultiplayerVisibleGUI>().numRooms = PhotonNetwork.countOfRooms;
			gameObject.GetComponent<MultiplayerVisibleGUI>().rooms = PhotonNetwork.GetRoomList();
//			Debug.Log ("host data: " + hostData);
		}

		if(GUI.Button(new Rect(Screen.width / 1.37f,Screen.height / 1.235f, Screen.width / 4.5f, Screen.height / 10), "Quit")){
			gameObject.GetComponent<MultiplayerHiddenGUI>().enabled = false;
			gameObject.GetComponent<MultiplayerVisibleGUI>().enabled = false;
			gameObject.GetComponent<MultiplayerLobbyMenuTex>().enabled = false;
			gameObject.GetComponent<MainMenuTex>().enabled = true;
			gameObject.GetComponent<MainMenuGUI>().enabled = true;
			gameObject.GetComponent<NetworkManager>().refreshing = false;
			gameObject.GetComponent<NetworkManager>().enabled = false;
		}
	}
}
