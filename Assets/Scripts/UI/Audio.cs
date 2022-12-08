using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    #region Variables
    private static Audio instance;
    private AudioSource source;
    [SerializeField] AudioMixerGroup[] mixers;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    #endregion

    #region Methods
    /// <summary>
    /// Reset song when change between different scene's types
    /// </summary>
    public void resetSong()
    {
        source.Stop();
        source.Play();
    }

    public void changeToBattleSong()
    {
        source.outputAudioMixerGroup = mixers[1];
    }

    public void changeToMenuTheme()
    {
        source.outputAudioMixerGroup = mixers[0];
    }

    public bool isNotPlayingBattleTheme()
    {
        return source.outputAudioMixerGroup.name.Equals(mixers[0].name);
    }
    #endregion


}
