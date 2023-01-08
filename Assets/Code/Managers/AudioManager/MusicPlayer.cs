using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        private AudioSource _source;
        [SerializeField] private float _fadeTime = 2f;
        [SerializeField] private List<StateClip> _stateClips = new();

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _source.volume = 0;
            StartCoroutine(FadeIn());
        }

        private void OnEnable()
        {
            GameStateManager.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(GameStateBase state)
        {
            if (state.IsAdditive) return;

            AudioClip clip = _stateClips.SingleOrDefault(stateClip => stateClip.State == state.Type).Clip;

            if (clip != null && _source.clip != clip)
            {
                ChangeTo(clip);
            }
            else if (clip == null)
            {
                Stop();
            }
        }

        private IEnumerator FadeIn()
        {
            _source.Play();
            float timer = 0;
            while(timer != _fadeTime)
            {
                timer = Mathf.MoveTowards(timer, _fadeTime, Time.unscaledDeltaTime);
                _source.volume = timer / _fadeTime;
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            float timer = _fadeTime;
            while(timer != 0)
            {
                timer = Mathf.MoveTowards(timer, _fadeTime, Time.unscaledDeltaTime);
                _source.volume = timer / _fadeTime;
                yield return null;
            }
            _source.Stop();
        }

        //public void Play(AudioClip clip)
        //{
        //    _source.clip = clip;
        //    StartCoroutine(FadeIn());
        //}

        public void ChangeTo(AudioClip clip)
        {
            StartCoroutine(FadeOut());
            _source.clip = clip;
            StartCoroutine(FadeIn());
        }

        public void Stop()
        {
            StartCoroutine(FadeOut());
            _source.clip = null;
        }
    }
}
