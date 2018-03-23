using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationService : MonoBehaviour {
	bool ServiceControl = false;
	int frameCount=0;
	public Text text;

	// Use this for initialization
	IEnumerator Start () {
		Input.gyro.enabled = true;
		if (!Input.location.isEnabledByUser) {
			yield break;

		}
	

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			print ("Unable to determine device location");
			yield break;
		} 
		else {
			
			ServiceControl = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (frameCount % 10 == 0) {
			if (ServiceControl) {
				text.text = "Latitude: " + Input.location.lastData.latitude + "\nLongitude: " + Input.location.lastData.longitude + "\nAltitude: " + Input.location.lastData.altitude + "\nHorizontal Accuracy: " + Input.location.lastData.horizontalAccuracy + "\nVertical Accuracy: " + Input.location.lastData.verticalAccuracy + "\nTime Stamp: " + Input.location.lastData.timestamp +  "\nRotation attitude :" + (int)Input.gyro.attitude.eulerAngles.x + "\n " + (int)Input.gyro.attitude.eulerAngles.y + "\n " + (int)Input.gyro.attitude.eulerAngles.z + "\n" + Input.gyro.attitude.w + "\n Vaziyyet :" + Input.gyro.enabled.ToString();                                                                     
			}
		}
		frameCount++;
	}
}
