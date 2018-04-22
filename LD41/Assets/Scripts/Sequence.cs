using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    public List<HitObject> HitObjects;
    public float _Time_Since_Start = 0;
    public bool _loop = true;
    public bool _is_playing = false;
    public float _length;
    public float _offset;
    public int _miss = 0;
    int _player_miss = 0;
    int _hit = 0;

    public GameObject _sprite;

    // Use this for initialization
    void Start()
    {

    }

    public void Init()
    {
        _Time_Since_Start = 0;
        _is_playing = false;
        _loop = true;

        int BPM = 120;
        _length = 4 * (60000 / BPM);

        HitObjects = new List<HitObject>();
        AudioClip clip1 = (AudioClip)Resources.Load("sound");

        HitObject HO_b1 = gameObject.AddComponent<HitObject>();
        HO_b1.Init();
        HO_b1._BPM = BPM;
        HO_b1._MS_per_beat = 60000 / HO_b1._BPM;
        HO_b1._size = 250;
        HO_b1._offset = 0 * HO_b1._MS_per_beat;
        HO_b1.HitSound = clip1;
        HO_b1._sprite = Instantiate(_sprite);
        SpriteRenderer sr = HO_b1._sprite.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 0);
        HO_b1._sequence = this;


        HitObject HO_b2 = gameObject.AddComponent<HitObject>();
        HO_b2._BPM = BPM;
        HO_b2.Init();
        HO_b2._MS_per_beat = 60000 / HO_b2._BPM;
        HO_b2._size = 250;
        HO_b2._offset = 1 * HO_b2._MS_per_beat;
        HO_b2.HitSound = clip1;
        HO_b2._sprite = Instantiate(_sprite); ;
        HO_b2._sequence = this;

        HitObject HO_b3 = gameObject.AddComponent<HitObject>();
        HO_b3._BPM = BPM;
        HO_b3.Init();
        HO_b3._MS_per_beat = 60000 / HO_b3._BPM;
        HO_b3._size = 250;
        HO_b3._offset = 2 * HO_b3._MS_per_beat;
        HO_b3.HitSound = clip1;
        HO_b3._sprite = Instantiate(_sprite); ;
        HO_b3._sequence = this;

        HitObject HO_b4 = gameObject.AddComponent<HitObject>();
        HO_b4._BPM = BPM;
        HO_b2.Init();
        HO_b4._MS_per_beat = 60000 / HO_b4._BPM;
        HO_b4._size = 250;
        HO_b4._offset = 3 * HO_b4._MS_per_beat;
        HO_b4.HitSound = clip1;
        HO_b4._sprite = Instantiate(_sprite); ;
        HO_b4._sequence = this;

        Destroy(_sprite);

        HitObjects.Add(HO_b1);
        HitObjects.Add(HO_b2);
        HitObjects.Add(HO_b3);
        HitObjects.Add(HO_b4);
    }

    public void Do()
    {
        if (_is_playing)
        {
            _Time_Since_Start += Time.deltaTime * 1000;
            if (_Time_Since_Start > _length)
            {
                OnEnd();
            }

            foreach (HitObject HO in HitObjects)
            {
                if (HO._is_hittable)
                {
                    if (HO._offset + HO._size < _Time_Since_Start)
                    {
                        HO.OnKill();
                        ++_miss;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // EVENTS
    public void OnEnd()
    {
        if (_loop) BeginAt(_Time_Since_Start - _length);
        else
        {
            Stop();
            _Time_Since_Start = 0;
        }

        foreach (HitObject HO in HitObjects)
        {
            HO.Reset();
        }

        if (_miss != 0)
        {

            Fretboard f = GetComponent<Fretboard>();
            if (f)
            {
                if (_player_miss != 0)
                {
                    Assets.Scripts.Token t = new Assets.Scripts.Token("test1", Assets.Scripts.Token.Sequence_State.Fail, _hit, _player_miss);
                    f.AddToken(t);
                }
                else
                {
                    Assets.Scripts.Token t = new Assets.Scripts.Token("test1", Assets.Scripts.Token.Sequence_State.Background, _hit, _miss);
                    f.AddToken(t);
                }
            }
            //output fail pattern;
        }
        else
        {
            Fretboard f = GetComponent<Fretboard>();
            if (f)
            {
                Assets.Scripts.Token t = new Assets.Scripts.Token("test1", Assets.Scripts.Token.Sequence_State.Success, _hit, _miss);
                f.AddToken(t);
            }
            // output success pattern
        }
    }

    public void OnPick()
    {
        if (_is_playing)
        {
            foreach (HitObject HO in HitObjects)
            {
                if (HO._offset + HO._size > _Time_Since_Start && HO._offset < _Time_Since_Start)
                {
                    if (HO._is_hittable)
                    {

                        //Hit
                        HO.OnHit();
                        ++_hit;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }

            }
            ++_player_miss;
            Fretboard f = GetComponent<Fretboard>();
            if (f)
            {
                f.OnMiss();
            }
        }
    }

    // MISC
    public void Begin()
    {
        _Time_Since_Start = 0;
        _is_playing = true;
        _miss = 0;
        _hit = 0;
        _player_miss = 0;
    }

    public void BeginAt(float offset)
    {
        _Time_Since_Start = offset;
        _is_playing = true;
        _miss = 0;
        _hit = 0;
        _player_miss = 0;
    }

    public void Stop()
    {
        _is_playing = false;
        _Time_Since_Start = 0;

    }

    public void AddOffset(float offset)
    {
        foreach (HitObject HO in HitObjects)
        {
            HO._offset += offset;
        }
        _length += offset;
    }




}
