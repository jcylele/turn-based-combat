using Skill.Action;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skill.Editor
{
    public class ActionEditor : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset m_ActionEditViewAsset = default;

        [MenuItem("Skill/Action Editor")]
        public static void ShowExample()
        {
            ActionEditor wnd = GetWindow<ActionEditor>();
            wnd.titleContent = new GUIContent("ActionEditor");
            wnd.minSize = new Vector2(640f, 360f);
        }

        ListView actionList;
        //InspectorView actionInspector;
        PropertyField actionProperty;

        public ActionTemplateData TemplateData { get; private set; }

        public void CreateGUI()
        {
            TemplateData = CreateInstance<ActionTemplateData>();
            TemplateData.actionDataList = new List<BaseActionData>();

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            var editView = m_ActionEditViewAsset.Instantiate();
            root.Add(editView);

            InitControls(editView);
        }

        void InitControls(VisualElement root)
        {
            var serializedTemplateData = new SerializedObject(this.TemplateData);

            this.actionProperty = root.Q<PropertyField>("action");
            //this.actionProperty.BindProperty(serializedTemplateData);

            root.Query<PropertyField>().ForEach((a) =>
            {
                a.BindProperty(serializedTemplateData);
            });

            var tbSave = root.Q<ToolbarButton>("tbSave");
            tbSave.clicked += OnSaveClick;

            var menu = root.Q<ToolbarMenu>("menuAdd");
            InitAddMenu(menu);

            this.actionList = root.Q<ListView>("actionList");
            actionList.makeItem = MakeActionDataItem;
            actionList.bindItem = BindActionDataItem;
            actionList.selectedIndicesChanged += OnActionSelectionChanged;
            this.actionList.itemsSource = this.TemplateData.actionDataList;
        }

        void OnMenuAddAction(Type type)
        {
            var actionData = Activator.CreateInstance(type) as BaseActionData;
            this.TemplateData.actionDataList.Add(actionData);
            this.actionList.Rebuild();
        }

        void InitAddMenu(ToolbarMenu menu)
        {
            List<Type> types = new List<Type>() { typeof(MoveActionData), typeof(PlayEffectActionData) };
            foreach (var type in types)
            {
                menu.menu.AppendAction(type.Name, (action) => OnMenuAddAction(type));
            }
        }

        void OnSaveClick()
        {
            var path = $"Assets/Resources/Actions/{this.TemplateData.templateName}.asset";
            AssetDatabase.CreateAsset(this.TemplateData, path);
            Debug.Log(path);
        }


        VisualElement MakeActionDataItem()
        {
            return new Label();
        }

        void BindActionDataItem(VisualElement element, int index)
        {
            (element as Label).text = (actionList.itemsSource[index] as BaseActionData).GetType().Name;
        }

        void OnActionSelectionChanged(IEnumerable<int> indices)
        {
            SerializedObject serializedObject = new SerializedObject(this.TemplateData);
            SerializedProperty listProperty = serializedObject.FindProperty("actionDataList");

            //Single Select Mode, only one object at each time
            foreach (var index in indices)
            {
                var itemProperty = listProperty.GetArrayElementAtIndex(index);
                this.actionProperty.bindingPath = itemProperty.propertyPath;
                this.actionProperty.BindProperty(serializedObject);
            }
        }
    }
}