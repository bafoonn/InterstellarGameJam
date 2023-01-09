using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    public class VolumeController : MonoBehaviour
    {
        private List<ChildVolume> _sliders;
        private MasterVolume _masterVolume;

        private void Awake()
        {
            _sliders = GetComponentsInChildren<ChildVolume>().ToList();
            _masterVolume = GetComponentInChildren<MasterVolume>();

            Debug.Assert(_sliders != null, $"{name} cannot find VolumeSliders in children.");
            Debug.Assert(_masterVolume != null, $"{name} cannot find MasterVolume in children.");
        }

        private void Start()
        {
            _masterVolume.Init();
            _sliders.ForEach(slider => slider.Setup(_masterVolume.Slider.value));
            _masterVolume.Slider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }

        private void OnDisable()
        {
            _masterVolume.Slider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        }

        public void OnMasterVolumeChanged(float value)
        {
            _sliders.ForEach(slider => slider.UpdateSlider(value));
        }
    }
}
