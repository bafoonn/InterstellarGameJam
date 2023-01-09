using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    public class ChildVolume : VolumeSlider
    {
        [SerializeField]
        private float _currentVolume = 1;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnValueChanged(float value)
        {
            _currentVolume = value / Slider.maxValue;
            base.OnValueChanged(Slider.maxValue * 0.001f * _currentVolume);
        }

        private void SetSlider(float value)
        {
            Slider.maxValue = value;
            Slider.value = Slider.maxValue * _currentVolume;
        } 

        public void UpdateSlider(float masterVolume)
        {
            float clampedValue = Mathf.Clamp(masterVolume, 0.001f, 1);
            SetSlider(clampedValue * 1000);
        }

        public void Setup(float masterVolume)
        {
            if(AudioManager.Instance.GetVolume(ParameterName, out float value))
            {
                _currentVolume = value.ToLinear() / masterVolume;
                _currentVolume = Mathf.Clamp(_currentVolume, 0, 1);
            }
            UpdateSlider(masterVolume);
        }
    }
}
