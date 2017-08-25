using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public GameObject player;
    public GameObject destroyer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        destroyer.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z -20);
	}

    void OnCollisionEnter(Collision col)
    {
 
        Destroy(col.gameObject);
    }
}
