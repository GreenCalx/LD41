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
    public bool _is_alive;

    public GameObject _sprite_prefab;
    public GameObject _sprite;
    // public GameObject Clone;

    public Sequence   _sequence;

    // Use this for initialization
    void Start()
    {
        if (_sprite_prefab)
        {
            _sprite = Instantiate(_sprite_prefab);
        }
        _is_hittable = true;
        //_is_alive = true;
        SpriteRenderer sr = _sprite.GetComponent<SpriteRenderer>();
        if (sr)
        {
            Bounds b = sr.bounds;
            float max = b.max.x;
            float min = b.min.x;

            float ppu = sr.sprite.pixelsPerUnit;
            float pix_per_ms = 0.005f;
            float ppsSize = pix_per_ms * _size;
            float ppsSizeScale = ppsSize * ppu;
            sr.transform.localScale = new Vector3(ppsSizeScale, 100, 1);
            //sr.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // EVENTS
    public void OnHit()
    {
        if (_is_hittable)
        {
            AudioSource.PlayClipAtPoint(HitSound, transform.position, 1);
            Fretboard f = GetComponentInParent<Fretboard>();
            if (f)
            {
                f.OnHit();
            }
            _is_hittable = false;
           // _is_alive = false;
        }
    }

    public void OnMiss()
    {
        AudioSource.PlayClipAtPoint(HitSound, transform.position, 0.25f);
        Fretboard f = GetComponentInParent<Fretboard>();
        if (f)
        {
            f.OnMiss();
        }
    }

    public void OnKill()
    {
        if (_is_hittable) OnMiss();
        _is_hittable = false;
    }

    public void Reset()
    {
        _is_hittable = true;
       // _is_alive = true;
    }

 
}
