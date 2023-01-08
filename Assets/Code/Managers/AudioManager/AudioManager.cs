using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Jam
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioMixer _mixer;
        [SerializeField, Range(0f, 1f)] private float _baseVolume = 0.5f;
        [SerializeField] private AudioSource _sfxSourceTemplate;
        private List<AudioSource> _sfxSources = new();

        [SerializeField] private List<SoundEffect> _soundEffects = new();

        protected override void Init()
        {
            Debug.Assert(_mixer != null, $"{name} has no AudioMixer set in the inspector.");
            Debug.Assert(_sfxSourceTemplate != null, $"{name} has no SfxSourceTemplate set in the inspector.");
        }

        public void PlaySound(string effectName, bool addPitch = true)
        {
            // Get clip
            AudioClip clip = _soundEffects.SingleOrDefault(sfx => sfx.Name.ToLower().Equals(effectName.ToLower())).Clip;

            // Check if there is a clip to be played.
            if (clip == null)
            {
                Debug.LogWarning($"{effectName} sound effect has no AudioClip.");
                return;
            }

            // Get source
            AudioSource source = _sfxSources.SingleOrDefault(src => src.clip == clip);

            // If no source is found, create a new one
            if(source == null)
            {
                source = Instantiate(_sfxSourceTemplate, transform);
                source.clip = clip;
                _sfxSources.Add(source);
            }

            source.Stop();

            if (addPitch)
            {
                source.pitch = 1 + Random.Range(0, 0.2f);
            }
            else
            {
                source.pitch = 1;
            }

            source.Play();
        }

        public static float ToLinear(float db)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, db / 20.0f));
        }

        public static float toDB(float linear)
        {
            return linear <= 0 ? -80f : Mathf.Log10(linear) * 20.0f;
        }

        public void SetVolume(string parameter, float value)
        {
            _mixer.SetFloat(parameter, toDB(value));
        }

        public bool GetVolume(string parameter, out float value)
        {
            if (_mixer.GetFloat(parameter, out float volume))
            {
                value = volume;
                return true;
            }

            value = 0;
            return false;
        }
    }
}
