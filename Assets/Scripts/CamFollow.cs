using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
	public Transform player;
	public float yOffset = 300;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform; 
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x , player.position.y + 2, -yOffset); 
		
	}
}
