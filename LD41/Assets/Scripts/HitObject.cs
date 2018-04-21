using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour {

    public float _MS_per_beat;
    public int _BPM;
    public float _length;
    public float _offset;
    public float _size;

    public AudioClip HitSound;
    public bool _is_hittable;

    public void OnHit()
    {
        if (_is_hittable)
        {
            AudioSource.PlayClipAtPoint(HitSound, transform.position);
            _is_hittable = false;
        }
    }

    // Use this for initialization
    void Start () {
        _is_hittable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
