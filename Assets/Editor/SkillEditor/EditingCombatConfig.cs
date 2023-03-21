using System;
using Editor.Reflect;
using Skill.Base;
using Skill.Buffs;
using Skill.Skills;
using UnityEditor;
using UnityEngine;

namespace Editor.SkillEditor
{
    /// <summary>
    /// Used as a serializedObject and an editing copy of original data
    /// </summary>
    public class EditingCombatConfig : ScriptableObject
    {
        [SerializeReference] public BaseSkill originSkill;
        [SerializeReference] public BaseBuff originBuff;
        [SerializeReference] public BaseSkill skill;
        [SerializeReference] public BaseBuff buff;
        public CombatConfig allData;
        
        public EditingItemData NewEditingItemData(EditClassInfo classInfo, BaseIdItem item, bool isOrigin)
        {
            //use copy, not the original
            if (item != null)
            {
                item = item.CopyObject<BaseIdItem>();
            }

            string fieldName = String.Empty;
            //TODO can't find an uniform way to process skill and buff
            if (classInfo.SelfType == typeof(BaseSkill))
            {
                if (isOrigin)
                {
                    fieldName = "originSkill";
                    this.originSkill = item as BaseSkill;
                }
                else
                {
                    fieldName = "skill";
                    this.skill = item as BaseSkill;
                }
            }
            else if (classInfo.SelfType == typeof(BaseBuff))
            {
                if (isOrigin)
                {
                    fieldName = "originBuff";
                    this.originBuff = item as BaseBuff;
                }
                else
                {
                    fieldName = "buff";
                    this.buff = item as BaseBuff;
                }
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                throw new Exception($"Invalid Select: {classInfo}, {item}");
            }

            var serializedObject = new SerializedObject(this);
            var newProperty = serializedObject.FindProperty(fieldName);
            return new EditingItemData(newProperty);
        }
    }
}