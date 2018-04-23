﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fretboard : MonoBehaviour {

    public Sequence _sequence_prefab;

    public Sequence _sequence;
    public Sequence _sequence_loop; //needed for loops

    public Sequence _other_sequence;

    public Pick _pick;

    public GameObject _HitSprite;

    private Queue<Assets.Scripts.Token> _tokens;

    public Vector3 _position_pick;

    public float _pick_percentage_position;
    public float _pick_width_percentage;
    public float _pick_height_percentage;

    public SpriteRenderer _sprite_render;

    public float _time;

	// Use this for initialization
	void Start () {

        if (_sequence_prefab)
        {
            _sequence = Instantiate(_sequence_prefab);
            _sequence.transform.parent = gameObject.transform;
            _other_sequence = Instantiate(_sequence_prefab);
            _other_sequence.transform.parent = gameObject.transform;

            //Destroy(_sequence_prefab);
        }

        _time = 0;
        _pick_percentage_position = 0.2f;
        _pick_width_percentage = 0.05f;
        _pick_height_percentage = 1f;

        _sprite_render = GetComponent<SpriteRenderer>();
        Bounds bounds = _sprite_render.bounds;
        float ppu = _sprite_render.sprite.pixelsPerUnit;

        float position_pick_x = bounds.min.x + _pick_percentage_position * (bounds.max.x - bounds.min.x);
        _position_pick = new Vector3(position_pick_x, 0, 0);

        _pick = gameObject.AddComponent<Pick>();
        _pick._width = _pick_width_percentage * (bounds.max.x - bounds.min.x) * ppu;
        _pick._height = _pick_height_percentage * (bounds.max.y - bounds.min.y) * ppu;
        _pick._sprite = Instantiate(_HitSprite);
        _pick._fretboard = this;
        _pick._sprite.transform.localPosition = _position_pick;

        if (!_sequence)
        {
            _sequence = gameObject.AddComponent<Sequence>();
            _sequence._sprite = _HitSprite;
            _sequence.Init();
        }

        if (!_other_sequence)
        {
            _other_sequence = gameObject.AddComponent<Sequence>();
            _other_sequence._sprite = _HitSprite;
            _other_sequence.Init2();
        }
        /*_sequence_loop = gameObject.AddComponent<Sequence>();
        _sequence_loop._sprite = _HitSprite;
        _sequence_loop.Init();
        _sequence_loop.AddOffset(_sequence._length);
        _sequence_loop._length += _sequence._length;
        */
        // _sequence._length *= 2;

        Init();
    }


    private void Init()
    {
        _tokens = new Queue<Assets.Scripts.Token>();
        _sequence.Begin();
        //_sequence_loop.Begin();
    }

    void switchSequence()
    {
        _sequence.Stop();
        _other_sequence.Stop();
        Sequence tmp = _sequence;
        _sequence = _other_sequence;
        _other_sequence = tmp;
        _sequence.Begin();
    }

    public void OnSequenceEnd()
    {
            switchSequence();
    }

    // Update is called once per frame
    void Update()
    {
        // Order is important
        if (_pick && _sequence) // && _sequence_loop)
        {

            _pick.Do();
            
            _sequence.Do();

            DrawSequence();
            // _sequence_loop.Do();
        }
    }

    // TOKENS 
    public void AddToken(Assets.Scripts.Token Token)
    {
        _tokens.Enqueue(Token);
    }

    public Assets.Scripts.Token GetToken()
    {
        if (_tokens.Count != 0)
        {
            return _tokens.Dequeue();
        }
        return null;
    }

    // EVENTS
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

    public void OnPick()
    {
        _sequence.OnPick();
        //_sequence_loop.OnPick();
    }

    // RENDER
    void DrawAtPixelFromBPM( HitObject HO )
    {
        float current_time = _sequence._Time_Since_Start;
        float hb_end = HO._offset + HO._size;
         float current_time_clipped = (HO._offset ) - current_time;
        float hb_end_time = hb_end - current_time;

        if (hb_end_time > 0)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float size = sr.bounds.max.x - sr.bounds.min.x;
                float left = _pick._sprite.transform.position.x;
                float right = sr.bounds.max.x + (_pick._sprite.transform.position.x - sr.bounds.max.x);
                float pixel_per_unit = sr.sprite.pixelsPerUnit;
                float pixel_per_ms = (size / 4) / _sequence.HitObjects[0]._MS_per_beat;
                float new_position = left + (pixel_per_ms * (current_time_clipped + HO._size / 2));
                float new_position_scaled = new_position / 1000.0f;
                if (_HitSprite)
                {
                    HO._sprite.transform.position = new Vector3(new_position, 0, 0);
                    //HO._sprite.transform.position = new Vector3(0, 0, 0);
                }
            }
        }
        else
        {
            if (_sequence._loop)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    float size = sr.bounds.max.x - sr.bounds.min.x;
                    float left = _pick._sprite.transform.position.x;
                    float right = sr.bounds.max.x + (_pick._sprite.transform.position.x - sr.bounds.min.x);
                    float pixel_per_ms = (size / 4) / _sequence.HitObjects[0]._MS_per_beat;
                    float new_position = right + (pixel_per_ms * (current_time_clipped + HO._size / 2));
                    float new_position_scaled = new_position / 1000.0f;
                    if (_HitSprite)
                    {
                        HO._sprite.transform.localPosition = new Vector3( new_position, 0, 0);
                        //HO._sprite.transform.position = new Vector3(0, 0, 0);
                    }
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

        /*
        foreach (HitObject HO in _sequence_loop.HitObjects)
        {
            if( _sequence_loop._is_playing )
            {
               // DrawAtPixelFromBPM(HO);
            }
        }*/
    }
}
