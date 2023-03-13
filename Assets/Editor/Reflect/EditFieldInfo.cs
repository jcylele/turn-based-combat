﻿using Skill.Attribute;
using Skill.Skills;
using System;
using System.Reflection;

namespace Skill.Reflect
{
    public class EditFieldInfo
    {
        private readonly FieldInfo field;

        public Type IDSelectType { get; private set; }

        public virtual string ShowName
        {
            get
            {
                return field.Name;
            }
        }

        public virtual Type FieldType
        {
            get
            {
                return field.FieldType;
            }
        }

        public EditFieldInfo(FieldInfo fieldInfo)
        {
            field = fieldInfo;

            var skillIdSelect = fieldInfo.GetCustomAttribute<SkillIdSelectAttribute>();
            if (skillIdSelect != null)
            {
                IDSelectType = skillIdSelect.idType;
            }
        }

        public T GetValue<T>(BaseEditItem editSkill)
        {
            return (T)field.GetValue(editSkill);
        }

        public void SetValue(BaseEditItem editSkill, object val)
        {
            field.SetValue(editSkill, val);
        }
    }
}