using UnityEngine;
using System.Collections;

public class MultiplayerVisibleGUI : MonoBehaviour {
	public string searchBox = "";
	public RoomInfo[] rooms = null;
	public int numRooms = 0;
		// Use this for initialization

	void Start() {
		rooms = PhotonNetwork.GetRoomList();
	}
	
	// Update is called once per frame
	void Update() {
//		Debug.Log ("in update");
//		if(gameObject.GetComponent<NetworkManager>().hostData != null && gameObject.GetComponent<NetworkManager>().enabled){
//			rooms = gameObject.GetComponent<NetworkManager>().hostData;
//		}
		if(PhotonNetwork.insideLobby){
			rooms = PhotonNetwork.GetRoomList();
		}
	}

	void OnGUI(){
		GUI.depth = -2;
		searchBox = GUI.TextField(new Rect(Screen.width / 1.9f,Screen.height / 1.235f, Screen.width / 4.9f, Screen.height / 10), searchBox);
		if(GUI.Button(new Rect(Screen.width / 16, Screen.height / 13, Screen.width / 10, Screen.height / 15), "Host Game")){
			Application.LoadLevel(1);
			gameObject.GetComponent<PUNNetworkManager>().enabled = true;
			if(gameObject.GetComponent<NetworkManager>().enabled){
				gameObject.GetComponent<NetworkManager>().startServer();
			}
			else if(gameObject.GetComponent<PUNNetworkManager>().enabled){
				PhotonNetwork.CreateRoom( null);
			}
		}
		if(gameObject.GetComponent<PUNNetworkManager>().enabled){
			if(rooms != null){
//				Debug.Log ("found some rooms");
				for(int i = 0; i < rooms.Length;i++){
					if(GUI.Button(new Rect(Screen.width / 12, Screen.width / 16 + ( (i + 1) * 40), Screen.width / 1.25f, 40), rooms[i].name + "    Players: " + rooms[i].playerCount + "/" + rooms[i].maxPlayers )){
						Application.LoadLevel(1);
						PhotonNetwork.JoinRoom(rooms[i].name);
					}
				}
			}
//			int i=0;
//			foreach(RoomInfo game in PhotonNetwork.GetRoomList()){
//			Debug.Log ("room!");
//				if(GUI.Button(new Rect(Screen.width / 12, Screen.width / 16 + ( (i + 1) * 40), Screen.width / 1.25f, 40), game.name + "    Players: " + game.playerCount + "/" + game.maxPlayers )){
//					Application.LoadLevel(1);
//					PhotonNetwork.JoinRoom(game.name);
//				}
//				i++;
//			}
		}
	}
}
