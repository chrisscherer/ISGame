using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]

public class Predictor : MonoBehaviour {

	public Transform observedTransform;
	public ClientPlayerManager reciever;
	public float pingMargin = .5f;

	private float clientPing;
	private NetState[] serverStateBuffer = new NetState[20];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if((Network.player == reciever.getOwner()) || (Network.isServer)){
//			return;
//		}

		if(Network.isServer){
			clientPing = 0;
		}
		else{
			clientPing = (Network.GetAveragePing(Network.connections[0]) / 100) + pingMargin;
		}
		float interpolationTime = (float)Network.time - clientPing;

		if(serverStateBuffer[0] == null) {
			serverStateBuffer[0] = new NetState();
			serverStateBuffer[0].setState(0, transform.position, transform.rotation);
		}

		if(serverStateBuffer[0].timestamp > interpolationTime){
			for(int i = 0;i < serverStateBuffer.Length;i++){
				if(serverStateBuffer[i] == null){
					continue;
				}

				if(serverStateBuffer[i].timestamp <= interpolationTime || i == serverStateBuffer.Length - 1){
					NetState bestTarget = serverStateBuffer[Mathf.Max(i-1, 0)];

					NetState bestStart = serverStateBuffer[i];

					float timediff = bestTarget.timestamp - bestStart.timestamp;
					float lerpTime = 0.0f;

					if(timediff > 0.0001){
						lerpTime = ((interpolationTime - bestStart.timestamp) / timediff);
					}

					transform.position = Vector3.Lerp(bestStart.pos,
					                                  bestTarget.pos,
					                                  lerpTime);
					transform.rotation = Quaternion.Slerp(bestStart.rot,
					                                      bestTarget.rot,
					                                      lerpTime);

					return;
				}
			}
		}
		else{
			NetState latest = serverStateBuffer[0];
			transform.position = Vector3.Lerp(transform.position, latest.pos, 0.5f);
			transform.rotation = Quaternion.Slerp(transform.rotation, latest.rot, 0.5f);
		}
	}

	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
		Vector3 pos = observedTransform.position;
		Quaternion rot = observedTransform.rotation;

		if(stream.isWriting) {
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);
		}
		else{
			stream.Serialize(ref pos);
			stream.Serialize(ref rot);
			reciever.serverPos = pos;
			reciever.serverRot = rot;

			reciever.lerpToTarget();

			for( int i = serverStateBuffer.Length - 1; i >= 1;i--){
				serverStateBuffer[i] = serverStateBuffer[i-1];
			}

			serverStateBuffer[0] = new NetState();
			serverStateBuffer[0].setState((float)info.timestamp, pos, rot);
		}
	}
}
