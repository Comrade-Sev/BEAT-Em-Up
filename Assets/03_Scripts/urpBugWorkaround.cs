using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace Game
{
    public class urpBugWorkaround : MonoBehaviour
    {
        private void Awake()
        {
            DebugManager.instance.enableRuntimeUI = false;
        }
    }
}
