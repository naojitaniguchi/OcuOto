using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class SendOculusRotByOSC : MonoBehaviour {
	#region Network Settings
	public string TargetAddr;
	public int OutGoingPort;
	public int InComingPort;
	public bool SendOSC = false ;
	public GameObject targetCamera ;
	public GameObject createTowerObj ;

	#endregion
	private Dictionary<string, ServerLog> servers;
	Vector3 sendValue ;
	// Script initialization
	void Start() {  
		OSCHandler.Instance.Init(TargetAddr, OutGoingPort, InComingPort);
		servers = new Dictionary<string, ServerLog>();
	}
	
	// NOTE: The received messages at each server are updated here
	// Hence, this update depends on your application architecture
	// How many frames per second or Update() calls per frame?
	void Update() {
		// must be called before you try to read value from osc server
		OSCHandler.Instance.UpdateLogs();
		
//		// データ受信部
//		servers = OSCHandler.Instance.Servers;
//		foreach( KeyValuePair<string, ServerLog> item in servers )
//		{
//			// If we have received at least one packet,
//			// show the last received from the log in the Debug console
//			if(item.Value.log.Count > 0) 
//			{
//				int lastPacketIndex = item.Value.packets.Count - 1;
//				
//				UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}", 
//				                                    item.Key, // Server name
//				                                    item.Value.packets[lastPacketIndex].Address, // OSC address
//				                                    item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
//			}
//		} 

		if ( targetCamera != null ){
			Vector3 cameraEulerAngles = targetCamera.transform.rotation.eulerAngles ;

			// Vector3 sendValue ;
			sendValue.x = 0 ;
			sendValue.y = 0 ;
			sendValue.z = 0 ;

			bool sendX = false ;
			if ( cameraEulerAngles.x < 90.0f ){
				sendValue.x = -1.0f * ( cameraEulerAngles.x / 90.0f );
				sendX = true ;
			}else if ( 270 <= cameraEulerAngles.x && cameraEulerAngles.x <= 360.0f ){
				sendValue.x = ( 360.0f - cameraEulerAngles.x ) / 90.0f ;
				sendX = true ;
			}

			if ( cameraEulerAngles.y > 180.0f ){
				sendValue.y = ( cameraEulerAngles.y - 180.0f ) / 180.0f ;
			}else{
				sendValue.y = -1.0f * ( 180 - cameraEulerAngles.y ) / 180.0f ;
			}


			bool sendZ = false ;
			if ( cameraEulerAngles.z < 90.0f ){
				sendValue.z = -1.0f * ( cameraEulerAngles.z / 90.0f ) ;
				sendZ = true ;
			}else if ( 270 <= cameraEulerAngles.z && cameraEulerAngles.z <= 360.0f ){
				sendValue.z = ( 360.0f - cameraEulerAngles.z ) / 90.0f ;
				sendZ = true ;
			}

			CreateTower createTower = createTowerObj.GetComponent<CreateTower>();
			createTower.setValueFromOculus( sendValue );

			if ( SendOSC ){
				if ( sendX ){
					OSCHandler.Instance.SendMessageToClient("Max/Msp", "/oculus_x", sendValue.x);
				}
				OSCHandler.Instance.SendMessageToClient("Max/Msp", "/oculus_y", sendValue.y);
				if ( sendZ ){
					OSCHandler.Instance.SendMessageToClient("Max/Msp", "/oculus_z", sendValue.z);
				}
			}
		}
		
		// データ送信部
		if(Input.GetMouseButtonDown(0)) {
			Debug.Log("SendMessage");
			var sampleVals = new List<int>(){1, 2, 3};
			OSCHandler.Instance.SendMessageToClient("Max/Msp", "/hoge/a", sampleVals);
		}
	}
}
