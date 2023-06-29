using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NumGates
{

    public static class AudioMusic
    {
        public static readonly string GameplayMusic = "BGM001";
        public static readonly string EndgameMusic = "BGM002";
        public static readonly string HomeMusic = "BGM003";
    }

    public static class SoundMusic
    {
        public static readonly string CoinCollect = "SFX001";
        public static readonly string ShieldHit = "SFX002";
        public static readonly string ShieldCharge = "SFX003";
        public static readonly string NormalCollect = "SFX004";
        public static readonly string BonusActivate = "SFX005";
        public static readonly string Missed = "SFX006";
        public static readonly string Upgrade = "SFX007";
        public static readonly string ItemDestroy = "SFX008";
        public static readonly string HomeMusic = "SFX009";
        public static readonly string UIClick = "SFX010";
        public static readonly string Countdown = "SFX011";
        public static readonly string GameStart = "SFX012";
        public static readonly string GameOver = "SFX013";
    }

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup[] mixerGroup;
        [SerializeField] private Audio[] musics;
        [SerializeField] private Audio[] sounds;

        public void Initialize()
        {
            foreach (Audio music in musics)
            {
                music.source = gameObject.AddComponent<AudioSource>();
                music.source.outputAudioMixerGroup = mixerGroup[0];
                music.source.clip = music.clip;
                music.source.pitch = music.pitch;
                music.source.volume = music.volume;
            }

            foreach (Audio sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.outputAudioMixerGroup = mixerGroup[1];
                sound.source.clip = sound.clip;
                sound.source.pitch = sound.pitch;
                sound.source.volume = sound.volume;
            }
        }

        public void Terminate()
        {

        }

        public void PlayMusic(string name)
        {
            Audio audio = FindAudio(musics, name);
            audio.source.Play();
            audio.source.loop = true;
        }

        public void StopMusic(string name)
        {
            Audio audio = FindAudio(musics, name);
            audio.source.Stop();
        }

        public void PlaySound(string name)
        {
            Audio audio = FindAudio(sounds, name);
            audio.source.Play();
        }

        private Audio FindAudio(Audio[] array, string name)
        {
            return Array.Find(array, audio => audio.name == name);
        }
    }
}
