using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    [RequireComponent(typeof(Slider))]
    public abstract class VolumeSlider : MonoBehaviour
    {
        public Slider Slider { get; protected set; }
        [field: SerializeField] public string ParameterName { get; protected set; }
        

        protected virtual void Awake()
        {
            Slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            Slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            Slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(float value)
        {
            AudioManager.Instance.SetVolume(ParameterName, value);
        }
    }
}
