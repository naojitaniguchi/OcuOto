﻿using UnityEngine;
using System.Collections;

public class CreateTower : MonoBehaviour {

	public GameObject prefab;
	public int xNum = 20;
	public int yNum = 20;
	public float radius = 5f;
	public float height = 10.0f ;
	public float startAngle = 0.0f ;
	public float vSinScale = 2.0f ;
	public float radShiftMax = 2.0f ;
	public float timeCoef = 3.0f ;

	GameObject[] sphereArray ;
	float elaspedTime = 0 ;
	float startAngleRad ;

	// Use this for initialization
	void Start () {
		sphereArray = new GameObject[xNum*yNum];

		startAngleRad = Mathf.Deg2Rad * startAngle;
		int count = 0 ;
		float y = -1.0f * ( height / 2.0f ) ;
		float ySetp = height / (float)yNum ;
		for ( int i = 0 ; i < yNum ; i ++ ){
			for (int j = 0; j < xNum; j++) {
				float angle = j * Mathf.PI * 2 / xNum + startAngleRad ;
				Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
				pos.y = y ;
				sphereArray[count] = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
				count ++ ;
			}
			y += ySetp ;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float startAngleByTime = elaspedTime * 0.5f ;

		int count = 0 ;
		float y = -1.0f * ( height / 2.0f ) ;
		float ySetp = height / (float)yNum ;
		for ( int i = 0 ; i < yNum ; i ++ ){
			float radCoef = Mathf.Sin ( elaspedTime * timeCoef + ( (float)i / yNum ) * Mathf.PI * 2.0f * vSinScale );
			float radShift = radCoef * radShiftMax ;
			float radAtY = radius + radShift ;
			float startAngleByTimeAyY = startAngleByTime + ( (float)i / (float)yNum ) * 4.0f  ;
			for (int j = 0; j < xNum; j++) {
				float angle = j * Mathf.PI * 2 / xNum + startAngleByTimeAyY ;
				Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radAtY;
				pos.y = y ;
				sphereArray[count].transform.position = pos ;
				count ++ ;
			}
			y += ySetp ;
		}

		elaspedTime += Time.deltaTime ;
	}
}