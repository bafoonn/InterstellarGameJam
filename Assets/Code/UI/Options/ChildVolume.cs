using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    public class ChildVolume : VolumeSlider
    {
        //[SerializeField]
        //private float _maxVolume = 100;
        [SerializeField]
        private float _currentVolume = 1;
        //public float MaxVolume
        //{
        //    get => _maxVolume;
        //    set
        //    {
        //        float clampedValue = Mathf.Clamp(value, 0.1f, 1);
        //        _maxVolume = clampedValue * 100;
        //        //SetSlider(Mathf.Clamp(value, 0.1f, 1f));
        //        SetSlider(clampedValue * 100);
        //    }
        //}

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
    }
}
