using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public Fretboard _fretboard;
	// Use this for initialization
	void Start () {
	}
	
    public void Do()
    {
       if (Input.GetKeyDown(KeyCode.Space))
            _fretboard._sequence.Pick();
    }

	// Update is called once per frame
	void Update () {

    }
}
