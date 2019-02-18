using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//public class Songs
//{
//    public AudioClip clip;
//    [Tooltip("0 för oändligt loopande")] [SerializeField] public int plays = 0;
//}

    public class Playlist : MonoBehaviour {

    public enum PlayBeaviour { loopLast, loopPlaylist};
    public PlayBeaviour playlistBehaviour = PlayBeaviour.loopLast;

    public List<AudioClip> songs = new List<AudioClip>();
    
	void Start () {
        AudioManager.instance.playSongs(songs, playlistBehaviour);
	}
	
}
