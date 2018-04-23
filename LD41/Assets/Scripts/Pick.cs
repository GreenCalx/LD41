using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public Fretboard _fretboard;
    // Use this for initialization
    public GameObject _sprite;

    public float _width;
    public float _height;
	void Start () {
        SpriteRenderer sr = _sprite.GetComponent<SpriteRenderer>();
        if (sr)
        {
            Bounds b = sr.bounds;
            float max = b.max.x;
            float min = b.min.x;

            float ppu = sr.sprite.pixelsPerUnit;

            float pps = 0.00075f;
            float ppsSize = pps * 10;
            float ppsSizeScale = ppsSize * ppu;
            sr.transform.localScale = new Vector3(_width, _height, sr.transform.localScale.z);

            sr.color = new Color(1, 1, 1);
        }
	}
	
    public void Do()
    {
       if (Input.GetKey(KeyCode.Space))
            _fretboard.OnPick();
    }

	// Update is called once per frame
	void Update () {

    }
}
