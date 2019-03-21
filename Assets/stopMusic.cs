using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopMusic : MonoBehaviour
{

    // Use this for initialization
    public void mute()
    {
        AudioManager.instance.muteMusic();
    }
    public void unMute()
    {
        AudioManager.instance.unMuteMusic();
    }
}
