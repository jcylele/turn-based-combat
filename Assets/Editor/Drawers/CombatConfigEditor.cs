using Skill.Base;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    // [CustomEditor(typeof(CombatConfig))]
    public class CombatConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            base.OnInspectorGUI();
            GUI.enabled = true;
        }
    }
}