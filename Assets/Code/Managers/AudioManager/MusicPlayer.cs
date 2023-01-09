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
        [SerializeField] private AudioClip _defaultClip;
        [SerializeField] private float _fadeTime = 2f;
        [SerializeField] private List<StateClip> _stateClips = new();

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Init()
        {
            _source.volume = 0;
            if (_defaultClip) StartCoroutine(FadeIn(_defaultClip));
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

            if(clip && !_source.clip)
            {
                StartCoroutine(FadeIn(clip));
            }
            else if (clip && _source.clip != clip)
            {
                StartCoroutine(ChangeTo(clip));
            }
            else if (!clip && _source.clip)
            {
                Stop();
            }
        }

        private IEnumerator FadeIn(AudioClip clip)
        {
            _source.clip = clip;
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
                timer = Mathf.MoveTowards(timer, 0, Time.unscaledDeltaTime);
                _source.volume = timer / _fadeTime;
                yield return null;
            }
            _source.Stop();
            _source.clip = null;
        }

        //public void ChangeTo(AudioClip clip)
        //{
        //    StartCoroutine(FadeOut());
        //    StartCoroutine(FadeIn(clip));
        //}

        private IEnumerator ChangeTo(AudioClip clip)
        {
            yield return StartCoroutine(FadeOut());
            yield return StartCoroutine(FadeIn(clip));
        }

        public void Stop()
        {
            StartCoroutine(FadeOut());
        }
    }
}
