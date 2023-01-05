using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jam
{
    public class FadeLoader : LoaderBase
    {
        [SerializeField]
        private Image image;

        private Color color;
        
        public override LoaderType Type => LoaderType.Fade;

        private void Awake()
        {
            color = image.color;
        }

        private void Update()
        {
            image.color = color;
        }

        public override IEnumerator TransitionIn(float time)
        {
            color.a = 0;
            float timer = 0;
            while (timer != time)
            {
                timer = Mathf.MoveTowards(timer, time, Time.unscaledDeltaTime);
                color.a = timer / time;
                yield return null;
            }
            active = true;
        }

        public override IEnumerator TransitionOut(float time)
        {
            color.a = 1;
            float timer = time;
            while (timer != 0)
            {
                timer = Mathf.MoveTowards(timer, 0, Time.unscaledDeltaTime * time);
                color.a = timer / time;
                yield return null;
            }
            active = false;
        }
    }
}
