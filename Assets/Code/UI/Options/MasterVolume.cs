using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class MasterVolume : VolumeSlider
    {
        public void Init()
        {
            if (AudioManager.Instance.GetVolume(ParameterName, out float value))
            {
                Slider.value = value.ToLinear();
            }
        }
    }
}
