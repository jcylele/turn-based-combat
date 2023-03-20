using Skill.Data;
using UnityEditor;
using UnityEngine;

namespace Editor.SkillEditor
{
    [CustomEditor(typeof(CombatConfig))]
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