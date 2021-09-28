using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace dkproj
{
    [CustomEditor(typeof(DebugEnableGlobalLight))]
    public class DebugEnableGlobalLightEditor : Editor
    {
        private DebugEnableGlobalLight m_target = null;

        private bool m_currentFlag = true;

        public void OnEnable()
        {
            m_target = target as DebugEnableGlobalLight;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_currentFlag = GUILayout.Toggle(m_target.EnableGlobalLight, "グローバルライト");
            if (m_target.EnableGlobalLight != m_currentFlag) {
                m_target.EnableGlobalLight = m_currentFlag;
            }
        }
    }
}