using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{

    //MUSIC SPECIFIC
    private AudioSource musicAudioSource;
    private float defaultMusicVolume = 0.5f;

    private bool musicEnabled;
    private const string musicEnabledKey = "MusicEnabled";
    public event Action<bool> musicEnableDisable;

    public AudioClip music_pre;
    public AudioClip music_start;
    public AudioClip music_during;
    public AudioClip music_tallying;
    public AudioClip music_failure;
    public AudioClip music_success;
    public AudioClip music_credits;

    public enum MusicType
    {
        Pre,
        Start,
        During,
        Tally,
        Failure,
        Success,
        Credits
    }
    private Dictionary<MusicType, AudioClip> musicAudioClips;

    //SFX SPECIFIC

    // private float defaultSFXVolume = 0.5f;

    private bool sfxEnabled;
    private const string sfxEnabledKey = "SFXEnabled";
    public event Action<bool> sfxEnableDisable;

    public AudioClip sfx_Profile_In;
    public AudioClip sfx_Profile_Out;
    public AudioClip sfx_Profile_ClickIn;
    public AudioClip sfx_Profile_ClickOut;
    public AudioClip sfx_Heart_Up;
    public AudioClip sfx_Heart_Down;
    public AudioClip sfx_InterestReveal;
    public AudioClip sfx_ChatBubble_popup;
    public AudioClip sfx_ChatBubble_convo;
    public AudioClip sfx_FailedMatch;
    public AudioClip sfx_SuccessfulMatch;
    public AudioClip sfx_TickTock;

    public enum SFXType
    {
        Profile_In,
        Profile_Out,

        Profile_ClickIn,
        Profile_ClickOut,

        Heart_Up,
        Heart_Down,

        InterestReveal,

        ChatBubble_popup,
        ChatBubble_convo,

        FailedMatch,
        SuccessfulMatch,

        TickTock
    }
    private Dictionary<SFXType, AudioClip> sfxAudioClips;


    //VA SPECIFIC

    // private float defaultVAVolume = 0.5f;

    private bool vaEnabled;
    private const string vaEnabledKey = "SFXEnabled";
    public event Action<bool> vaEnableDisable;

    public AudioClip[] va_quips;
    public AudioClip[] va_attempts;

    public enum VAType
    {
        Quips,
        AttemptDialog
    }
    private Dictionary<VAType, AudioClip[]> vaAudioClips;





    private AudioSource NewAudioSource(string name)
    {
        GameObject audioObject = new GameObject(name);
        audioObject.transform.SetParent(this.transform);

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        return audioSource;
    }


    private void InitSounds()
    {
        //Initiate music

        musicAudioClips = new Dictionary<MusicType, AudioClip>();
        musicAudioClips.Add(MusicType.Pre, music_pre);
        musicAudioClips.Add(MusicType.Start, music_start);
        musicAudioClips.Add(MusicType.During, music_during);
        musicAudioClips.Add(MusicType.Tally, music_tallying);
        musicAudioClips.Add(MusicType.Failure, music_failure);
        musicAudioClips.Add(MusicType.Success, music_success);
        musicAudioClips.Add(MusicType.Credits, music_credits);

        //Initiate sfx

        sfxAudioClips = new Dictionary<SFXType, AudioClip>();
        sfxAudioClips.Add(SFXType.Profile_In, sfx_Profile_In);
        sfxAudioClips.Add(SFXType.Profile_Out, sfx_Profile_Out);
        sfxAudioClips.Add(SFXType.Profile_ClickIn, sfx_Profile_ClickIn);
        sfxAudioClips.Add(SFXType.Profile_ClickOut, sfx_Profile_ClickOut);
        sfxAudioClips.Add(SFXType.Heart_Up, sfx_Heart_Up);
        sfxAudioClips.Add(SFXType.Heart_Down, sfx_Heart_Down);
        sfxAudioClips.Add(SFXType.InterestReveal, sfx_InterestReveal);
        sfxAudioClips.Add(SFXType.ChatBubble_popup, sfx_ChatBubble_popup);
        sfxAudioClips.Add(SFXType.ChatBubble_convo, sfx_ChatBubble_convo);
        sfxAudioClips.Add(SFXType.FailedMatch, sfx_FailedMatch);
        sfxAudioClips.Add(SFXType.SuccessfulMatch, sfx_SuccessfulMatch);
        sfxAudioClips.Add(SFXType.TickTock, sfx_TickTock);

        //Initiate voice acting

        vaAudioClips = new Dictionary<VAType, AudioClip[]>();
        vaAudioClips.Add(VAType.Quips, va_quips);
        vaAudioClips.Add(VAType.AttemptDialog, va_attempts);
    }

    private void InitAudioSources()
    {
        musicAudioSource = NewAudioSource("Music");

        musicAudioSource.loop = true;
        musicAudioSource.volume = defaultMusicVolume;
        musicAudioSource.playOnAwake = false;
    }

    private bool MusicEnabled
    {
        get { return PlayerPrefs.GetInt(musicEnabledKey, 1) == 1 ? true : false; }
        set
        {
            musicEnabled = value;
            PlayerPrefs.SetInt(musicEnabledKey, value ? 1 : 0);
        }
    }

    public void EnableDisabledMusic()
    {
        MusicEnabled = !musicEnabled;

        if (musicEnableDisable != null)
        {
            musicEnableDisable(musicEnabled);
        }
    }

    public void PlayMusic(string name)
    {
        if (!musicEnabled) return;
        switch (name)
        {
            case "pre":
                musicAudioSource.clip = musicAudioClips[MusicType.Pre];
                break;
            case "start":
                musicAudioSource.clip = musicAudioClips[MusicType.Start];
                break;
            case "during":
                musicAudioSource.clip = musicAudioClips[MusicType.During];
                break;
            case "tally":
                musicAudioSource.clip = musicAudioClips[MusicType.Tally];
                break;
            case "failure":
                musicAudioSource.clip = musicAudioClips[MusicType.Failure];
                break;
            case "success":
                musicAudioSource.clip = musicAudioClips[MusicType.Success];
                break;
            case "credits":
                musicAudioSource.clip = musicAudioClips[MusicType.Credits];
                break;
            default: break;
        }
        musicAudioSource.Play();
    }

    private bool SFXEnabled
    {
        get { return PlayerPrefs.GetInt(sfxEnabledKey, 1) == 1 ? true : false; }
        set
        {
            sfxEnabled = value;
            PlayerPrefs.SetInt(sfxEnabledKey, value ? 1 : 0);
        }
    }

    public void EnableDisabledSFX()
    {
        SFXEnabled = !sfxEnabled;

        if (sfxEnableDisable != null)
        {
            sfxEnableDisable(sfxEnabled);
        }
    }

    private float getSFXVolumeHelper(SFXType sfxType)
    {
        float volume = 1.0f;
        switch (sfxType)
        {
            case SFXType.Profile_ClickOut:
                volume = 0.5f; break;
            default: break;
        }

        return volume;
    }

    private void AdjustSFXPitchHelper(
        AudioSource audioSource,
        SFXType sfxType,
        float pitch = 1.0f
        )
    {
        if (pitch == -1) pitch = UnityEngine.Random.Range(0.75f, 1.0f);

        switch (sfxType)
        {
            case SFXType.Profile_ClickIn:
            case SFXType.Profile_ClickOut:
            case SFXType.Profile_In:
            case SFXType.Profile_Out:
            case SFXType.SuccessfulMatch:
                pitch = 1.0f;
                break;
            case SFXType.FailedMatch:
                pitch = 2.0f;
                break;
            default: break;
        }

        audioSource.pitch = pitch;
    }

    void AddEffect(string effect, GameObject parentObj)
    {
        string[] effectSplit = effect.Split('-');
        string effectName = effectSplit[0];

        switch (effectName)
        {
            case "echo":
            case "e":
                AudioEchoFilter echo = parentObj.AddComponent<AudioEchoFilter>();

                float delay = effectSplit[1] == null ? 4.2f : float.Parse(effectSplit[1]);
                float wet = effectSplit[2] == null ? 1 : float.Parse(effectSplit[2]);

                echo.delay = delay;
                echo.wetMix = wet;

                break;

            case "reverb":
            case "r":

                AudioReverbFilter reverb = parentObj.AddComponent<AudioReverbFilter>();

                string ReverbType = effectSplit[1] == null ? "ConcertHall" : effectSplit[1];
                switch (ReverbType)
                {
                    case "Cave":
                        reverb.reverbPreset = AudioReverbPreset.Cave;
                        break;
                    case "ConcertHall":
                        reverb.reverbPreset = AudioReverbPreset.Concerthall;
                        break;
                    case "Underwater":
                        reverb.reverbPreset = AudioReverbPreset.Underwater;
                        break;
                    default: break;
                }

                break;
            default:
                break;
        }
    }

    private void AdjustSFXEffects(string effects, GameObject sfxGameObject)
    {
        string[] effectsSplit = effects.Split('_');

        for (int i = 0; i < effectsSplit.Length; i++)
        {
            string effect = effectsSplit[i];
            AddEffect(effect, sfxGameObject);
        }
    }

    public void PlaySFX(string name, float pitch = 1.0f)
    {
        switch (name)
        {
            case "profile select":
                PlaySFX(SFXType.Profile_ClickIn, "", pitch);
                break;
            case "profile unselect":
            case "profile deselect":
                PlaySFX(SFXType.Profile_ClickOut, "", pitch);
                break;
            case "profile in":
                PlaySFX(SFXType.Profile_In, "", pitch);
                break;
            case "profile out":
                PlaySFX(SFXType.Profile_Out, "", pitch);
                break;
            case "match success":
                PlaySFX(SFXType.SuccessfulMatch, "", pitch);
                break;
            case "match fail":
                PlaySFX(SFXType.FailedMatch, "", pitch);
                break;
            case "tick tock":
                PlaySFX(SFXType.TickTock, "", pitch);
                break;
            default: break;
        }
    }

    public void PlaySFX(SFXType sfxType, string effects = "", float pitch = 1.0f)
    {
        AudioClip sfxAudioClip = sfxAudioClips[sfxType];

        if (!sfxEnabled || sfxAudioClip == null) return;

        AudioSource sfxAudioSource = NewAudioSource("SFX");
        GameObject sfxGameObject = sfxAudioSource.gameObject;

        if (pitch != 1.0) AdjustSFXPitchHelper(sfxAudioSource, sfxType, pitch);
        float volume = getSFXVolumeHelper(sfxType);
        if (effects != "") AdjustSFXEffects(effects, sfxGameObject);

        sfxAudioSource.PlayOneShot(sfxAudioClip, volume);
        float clipLength = sfxAudioClip.length;
        Destroy(sfxAudioSource.gameObject, clipLength);
    }

    private bool VAEnabled
    {
        get { return PlayerPrefs.GetInt(vaEnabledKey, 1) == 1 ? true : false; }
        set
        {
            vaEnabled = value;
            PlayerPrefs.SetInt(vaEnabledKey, value ? 1 : 0);
        }
    }

    public void EnableDisabledVA()
    {
        VAEnabled = !vaEnabled;

        if (vaEnableDisable != null)
        {
            vaEnableDisable(vaEnabled);
        }
    }

    private float getVAVolumeHelper(VAType vaType)
    {
        float volume = 1.0f;
        switch (vaType)
        {
            case VAType.Quips:
            case VAType.AttemptDialog:
                volume = 1.0f;
                break;
            default: break;
        }

        return volume;
    }

    private void AdjustVAPitchHelper(
        AudioSource audioSource,
        VAType vaType,
        float pitch = 1.0f
        )
    {
        if (pitch == -1) pitch = UnityEngine.Random.Range(0.75f, 1.0f);

        switch (vaType)
        {
            case VAType.Quips:
            case VAType.AttemptDialog:
                pitch = 1.0f;
                break;
            default: break;
        }

        audioSource.pitch = pitch;
    }

    public void TryPlayVA(string name)
    {
        if (UnityEngine.Random.Range(0, 100) >= 50) return;
        Managers.SoundManager.PlayVA(name);
    }

    public void PlayVA(string name, float pitch = 1.0f)
    {
        switch (name)
        {
            case "quip":
                PlayVA(VAType.Quips, "echo-5-2", pitch);
                break;
            case "attempt":
            case "attempt dialog":
            case "evaluate":
            case "evaluate match":
                PlayVA(VAType.AttemptDialog, "echo-5-2", pitch);
                break;
            default: break;
        }
    }

    public void PlayVA(VAType vaType, string effects = "", float pitch = 1.0f)
    {
        AudioClip vaAudioClip = vaAudioClips[vaType][UnityEngine.Random.Range(0, vaAudioClips[vaType].Length)];

        if (!vaEnabled || vaAudioClip == null) return;

        AudioSource vaAudioSource = NewAudioSource("VA");
        GameObject vaGameObject = vaAudioSource.gameObject;

        if (pitch != 1.0) AdjustVAPitchHelper(vaAudioSource, vaType, pitch);
        float volume = getVAVolumeHelper(vaType);
        if (effects != "") AdjustSFXEffects(effects, vaGameObject);

        vaAudioSource.PlayOneShot(vaAudioClip, volume);
        float clipLength = vaAudioClip.length;
        Destroy(vaAudioSource.gameObject, clipLength);
    }


    private void Start()
    {
        musicEnabled = MusicEnabled;
        sfxEnabled = SFXEnabled;
        vaEnabled = VAEnabled;

        InitAudioSources();
        InitSounds();
    }
}
