using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace dkproj
{
    /// <summary>
    /// うまく動かない
    /// ひとまず Global Light 2D 本体の Active の ON OFF で対応
    /// </summary>
    public class DebugEnableGlobalLight : MonoBehaviour
    {
        /// <summary>
        /// true...グローバルライト
        /// </summary>
        private bool m_enableGlobalLight = true;
        public bool EnableGlobalLight { 
            get => m_enableGlobalLight;
            set {
                m_enableGlobalLight = value;
                Light2D light = GetComponentInChildren<Light2D>();
                light.gameObject.SetActive(m_enableGlobalLight);
            } 
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}