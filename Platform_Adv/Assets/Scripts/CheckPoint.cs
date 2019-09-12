using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public GameSession GameSession;

	// Use this for initialization
	void Start () {
        GameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            GameSession.currentCheckpoint = gameObject;
        }
    }
}
