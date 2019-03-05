using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


    public class Playlist : MonoBehaviour {

    public enum PlayBeaviour { loopLast, loopPlaylist};
    public PlayBeaviour playlistBehaviour = PlayBeaviour.loopLast;

    public List<AudioClip> songs = new List<AudioClip>();
    
	void Start () {
        AudioManager.instance.playSongs(songs, playlistBehaviour);
	}
	
}
