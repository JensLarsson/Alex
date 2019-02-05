using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float time = 0.0f;
    private float duration;

    public Timer(float d = 0.0f)
    {
        duration = d;
    }

    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            time = value;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }
        set
        {
            duration = value;
            Time = 0.0f;
        }
    }

    public bool expired
    {
        get
        {
            return time > duration;
        }
    }
    

    public bool expireRepeat(float time)
    {
        if (this.time >= time)
        {
            this.time = 0.0f;
            return true;
        }
        return false;
    }

}
