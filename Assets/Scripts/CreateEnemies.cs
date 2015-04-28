using System;
//using UnityEditor;
using UnityEngine;
using System.Collections;

public class CreateEnemies : MonoBehaviour
{
    public GameObject circlePrefab;
    public int num;
    public Vector3 start;
    public float spacing;
    public float offsetMagic;
    public float radius;

    public float speed;
    public float speedMagic;
	// Use this for initialization
	void Start ()
	{
	    //circlePrefab = (GameObject) Instantiate(Resources.Load("CircleEnemy"));
	    for (int i = 0; i < num; i++)
	    {
	        GameObject temp = (GameObject)Instantiate(circlePrefab, new Vector3(), Quaternion.identity);
	        Vector3 newCenter = start + new Vector3(i*spacing, 0, 0);

	        temp.GetComponent<CircleEnemyController>().Initialize(newCenter, radius, i*offsetMagic, speed + (float)i/num*speedMagic);
	    }
	}

 
	
	// Update is called once per frame
	void Update () {
	
	}
}
