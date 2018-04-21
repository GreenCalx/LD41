using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fretboard : MonoBehaviour {

    public Sequence s;

	// Use this for initialization
	void Start () {
        s = gameObject.AddComponent<Sequence>();
        Init();
    }

    private void Init()
    {
        s.Begin();
    }
    // Update is called once per frame
    void Update () {
	}
}
