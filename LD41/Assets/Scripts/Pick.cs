using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour {
    public Fretboard _fretboard;
    // Use this for initialization
    public GameObject _sprite_prefab;
    public GameObject _sprite;

    public float _width;
    public float _height;

    public Vector3 _position;

    public Color _color;

    public KeyCode _KeyPress;

	void Start () {
        Init();
	}

    public void Init()
    {
        // Instanciate sprite
        if (_sprite_prefab)
        {
            _sprite = Instantiate(_sprite_prefab);
            if (_sprite)
            {
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
                    _sprite.transform.localPosition = _position;
                    sr.color = _color;
                }
            }
        }
 
    }

    public void Do()
    {
       if (Input.GetKey(_KeyPress))
            _fretboard.OnPick();
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sr = _sprite.GetComponent<SpriteRenderer>();
        if (sr)
        {
            GameObject go = sr.gameObject;
            go.SetActive(true);
        }
    }
}
