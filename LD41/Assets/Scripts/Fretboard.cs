using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fretboard : MonoBehaviour {

    public Sequence _sequence;
    public Sequence _sequence_loop;
    public Pick _pick;
    public GameObject _HitSprite;

	// Use this for initialization
	void Start () {
        Instantiate(_HitSprite);

        _sequence = gameObject.AddComponent<Sequence>();
        _sequence._sprite = _HitSprite;
        _sequence.Init();

        _sequence_loop = gameObject.AddComponent<Sequence>();
        _sequence_loop._sprite = _HitSprite;
        _sequence_loop.Init();
        _sequence_loop.AddOffset(_sequence_loop._length);

        _pick = gameObject.AddComponent<Pick>();
        _pick._fretboard = this;

        Init();
    }

    private void Init()
    {

        _sequence.Begin();
        _sequence_loop.Begin();
    }

    public void OnHit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
        {
            sr.color = new Color(0, 1, 0);
        }
    }

    public void OnMiss()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr)
        {
            sr.color = new Color(1, 0, 0);
        }
    }

    void DrawAtPixelFromBPM( HitObject HO )
    {
        float current_time = _sequence._Time_Since_Start;
        float current_time_clipped = HO._offset - current_time;
        //if (current_time_clipped > 0)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float size = sr.bounds.max.x - sr.bounds.min.x;
                float left = sr.bounds.min.x;
                float right = sr.bounds.max.x;
                float pixel_per_ms = (size / 4) / _sequence.HitObjects[0]._MS_per_beat;
                if (_HitSprite)
                {
                    HO.Clone.transform.position = new Vector3(left + (pixel_per_ms * current_time_clipped), transform.position.y, 0);
                }
            }
        }
    }

    void DrawSequence()
    {
        foreach( HitObject HO in _sequence.HitObjects )
        {
            if( _sequence._is_playing )
            {
                DrawAtPixelFromBPM(HO);
            }
        }

        foreach (HitObject HO in _sequence_loop.HitObjects)
        {
            if( _sequence_loop._is_playing )
            {
                DrawAtPixelFromBPM(HO);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        _pick.Do();
        DrawSequence();
        _sequence.Do();
	}
}
