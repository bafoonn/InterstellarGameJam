using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Options : MonoBehaviour
    {
        public void Back()
        {
            GameStateManager.Instance.GoBack();
        }
    }
}
