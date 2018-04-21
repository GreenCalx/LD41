using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fretboard : MonoBehaviour {

    public Sequence _sequence;
    public Pick _pick;

	// Use this for initialization
	void Start () {
        _sequence = gameObject.AddComponent<Sequence>();
        _pick = gameObject.AddComponent<Pick>();
        _pick._fretboard = this;
        Init();
    }

    private void Init()
    {
        _sequence.Begin();
    }
    // Update is called once per frame
    void Update () {
	}
}
