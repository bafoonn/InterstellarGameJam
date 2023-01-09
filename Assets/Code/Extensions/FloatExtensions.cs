using UnityEngine;

namespace Jam
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Changes float from decibels to linear.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static float ToLinear(this float db)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, db / 20.0f));
        }

        /// <summary>
        /// Changes float from linear to decibels.
        /// </summary>
        /// <param name="linear"></param>
        /// <returns></returns>
        public static float toDB(this float linear)
        {
            return linear <= 0 ? -80f : Mathf.Log10(linear) * 20.0f;
        }
    }
}
