using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    List<HitObject> HitObjects;
    float _Time_Since_Start = 0;
    public bool _loop = true;
    bool _is_playing = false;
    float _length;

    // Use this for initialization
    void Start()
    {
        _length = 4 * (60000 / 60);

        HitObjects = new List<HitObject>();
        AudioClip clip1 = (AudioClip)Resources.Load("sound");

        HitObject HO_b1 = gameObject.AddComponent<HitObject>();
        HO_b1._BPM = 60;
        HO_b1._MS_per_beat = 60000 / HO_b1._BPM;
        HO_b1._size = 100;
        HO_b1._offset = HO_b1._MS_per_beat;
        HO_b1.HitSound = clip1;

        HitObject HO_b2 = gameObject.AddComponent<HitObject>();
        HO_b2._BPM = 60;
        HO_b2._MS_per_beat = 60000 / HO_b2._BPM;
        HO_b2._size = 100;
        HO_b2._offset = 2 * HO_b2._MS_per_beat;
        HO_b2.HitSound = clip1;

        HitObject HO_b3 = gameObject.AddComponent<HitObject>();
        HO_b3._BPM = 60;
        HO_b3._MS_per_beat = 60000 / HO_b3._BPM;
        HO_b3._size = 100;
        HO_b3._offset = 3 * HO_b3._MS_per_beat;
        HO_b3.HitSound = clip1;

        HitObject HO_b4 = gameObject.AddComponent<HitObject>();
        HO_b4._BPM = 60;
        HO_b4._MS_per_beat = 60000 / HO_b4._BPM;
        HO_b4._size = 100;
        HO_b4._offset = 4 * HO_b4._MS_per_beat;
        HO_b4.HitSound = clip1;

        HitObjects.Add(HO_b1);
        HitObjects.Add(HO_b2);
        HitObjects.Add(HO_b3);
        HitObjects.Add(HO_b4);

    }
    // Update is called once per frame
    void Update()
    {
        if (_is_playing)
        {
            _Time_Since_Start += Time.deltaTime * 1000;
            if (_Time_Since_Start > _length)
            {
                _Time_Since_Start = 0;
                foreach (HitObject HO in HitObjects)
                {
                    HO._is_hittable = true;
                }
            }

            foreach (HitObject HO in HitObjects)
            {
                if (HO._offset + HO._size > _Time_Since_Start && HO._offset - HO._size < _Time_Since_Start)
                {
                    //Hit
                    HO.OnHit();
                }
            }
        }
    }

    public void Begin()
    {
        _Time_Since_Start = 0;
        _is_playing = true;
    }

    public void Stop()
    {
        _is_playing = false;
        _Time_Since_Start = 0;
    }
}
