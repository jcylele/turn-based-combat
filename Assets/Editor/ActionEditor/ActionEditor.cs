using System;
using System.Collections.Generic;
using System.Linq;
using Action.ActionData;
using Action.Base;
using Editor.Reflect;
using Skill.Action;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.ActionEditor
{
    public class ActionEditor : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_ActionEditViewAsset = default;
        [SerializeField] private VisualTreeAsset m_ActionItemAsset = default;


        [MenuItem("Skill/Action Editor")]
        public static void ShowExample()
        {
            ActionEditor wnd = GetWindow<ActionEditor>();
            wnd.titleContent = new GUIContent("ActionEditor");
            wnd.minSize = new Vector2(640f, 360f);
        }


        private EditTypeMgr mEditTypeMgr;

        private ListView mActionList;

        //InspectorView actionInspector;
        private PropertyField mActionProperty;
        private SerializedProperty mActionDataListProperty;

        private ActionTemplateData TemplateData { get; set; }

        public void CreateGUI()
        {
            mEditTypeMgr = new EditTypeMgr(typeof(BaseActionData));

            TemplateData = CreateInstance<ActionTemplateData>();
            TemplateData.actionDataList = new List<BaseActionData>();

            // var templateList = GetAllAssets<ActionTemplateData>("Assets/Resources/Actions/");

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Instantiate UXML
            var editView = m_ActionEditViewAsset.Instantiate();
            root.Add(editView);

            InitControls(editView);
        }

        private void OnTotalFrameChanged(SerializedPropertyChangeEvent evt)
        {
            foreach (var actionData in this.TemplateData.actionDataList)
            {
                switch (actionData)
                {
                    case BaseTickActionData tickActionData:
                        tickActionData.enterFrame =
                            Mathf.Clamp(tickActionData.enterFrame, 0, this.TemplateData.totalFrame - 1);
                        tickActionData.exitFrame =
                            Mathf.Clamp(tickActionData.exitFrame, 0, this.TemplateData.totalFrame - 1);
                        break;
                    case BaseTriggerActionData triggerActionData:
                        triggerActionData.triggerFrame = Mathf.Clamp(triggerActionData.triggerFrame, 0,
                            this.TemplateData.totalFrame - 1);
                        break;
                }
            }

            this.mActionList.Rebuild();
        }

        private void InitControls(VisualElement root)
        {
            var serializedObject = new SerializedObject(this.TemplateData);
            this.mActionDataListProperty = serializedObject.FindProperty("actionDataList");

            this.mActionProperty = root.Q<PropertyField>("action");

            root.Query<PropertyField>().ForEach((a) => { a.BindProperty(serializedObject); });

            var totalFramePropertyField = root.Q<PropertyField>("totalFrame");
            totalFramePropertyField.RegisterValueChangeCallback(OnTotalFrameChanged);

            var tbSave = root.Q<ToolbarButton>("tbSave");
            tbSave.clicked += OnSaveClick;

            var menu = root.Q<ToolbarMenu>("menuAdd");
            InitAddMenu(menu);

            this.mActionList = root.Q<ListView>("actionList");
            mActionList.makeItem = MakeActionDataItem;
            mActionList.bindItem = BindActionDataItem;
            mActionList.selectedIndicesChanged += OnActionSelectionChanged;
            this.mActionList.itemsSource = this.TemplateData.actionDataList;
        }

        private void OnMenuAddAction(Type type)
        {
            var actionData = Activator.CreateInstance(type) as BaseActionData;
            this.TemplateData.actionDataList.Add(actionData);
            this.mActionDataListProperty.serializedObject.Update();
            this.mActionList.Rebuild();
        }

        private void InitAddMenu(ToolbarMenu menu)
        {
            List<Type> types = new List<Type>() { typeof(MoveActionData), typeof(PlayEffectActionData) };
            foreach (var type in types)
            {
                menu.menu.AppendAction(type.Name, (action) => OnMenuAddAction(type));
            }
        }

        private void OnSaveClick()
        {
            var path = $"Assets/Resources/Actions/{this.TemplateData.templateName}.asset";
            AssetDatabase.CreateAsset(this.TemplateData, path);
            Debug.Log(path);
        }


        private VisualElement MakeActionDataItem()
        {
            return m_ActionItemAsset.Instantiate();
        }

        private void BindActionDataItem(VisualElement element, int index)
        {
            var data = this.TemplateData.actionDataList[index];
            var itemProperty = this.mActionDataListProperty.GetArrayElementAtIndex(index);

            var typeName = data.GetType().Name;
            var tick = element.Q<MinMaxSlider>("tick");
            var trigger = element.Q<Slider>("trigger");

            switch (data)
            {
                case BaseTickActionData tickActionData:
                    tick.label = typeName;
                    tick.lowLimit = 0;
                    tick.highLimit = this.TemplateData.totalFrame - 1;
                    tick.value = new Vector2(tickActionData.enterFrame, tickActionData.exitFrame);
                    tick.RegisterValueChangedCallback((evt) =>
                    {
                        tickActionData.enterFrame = (int)evt.newValue.x;
                        tickActionData.exitFrame = (int)evt.newValue.y;
                    });

                    tick.style.display = DisplayStyle.Flex;
                    trigger.style.display = DisplayStyle.None;
                    break;
                case BaseTriggerActionData triggerActionData:
                    //TODO due to some unknown bug, slider can not be selected in listview
                    trigger.label = typeName;
                    trigger.lowValue = 0;
                    trigger.highValue = this.TemplateData.totalFrame - 1;

                    trigger.value = triggerActionData.triggerFrame;
                    trigger.RegisterValueChangedCallback((evt) => { triggerActionData.triggerFrame = (int)evt.newValue; });
                    // trigger.BindProperty(itemProperty.FindPropertyRelative("triggerFrame"));

                    tick.style.display = DisplayStyle.None;
                    trigger.style.display = DisplayStyle.Flex;
                    break;
            }
        }

        private void OnActionSelectionChanged(IEnumerable<int> indices)
        {
            //Single Select Mode, only one object at each time
            foreach (var index in indices)
            {
                var itemProperty = this.mActionDataListProperty.GetArrayElementAtIndex(index);
                // this.mActionProperty.bindingPath = itemProperty.propertyPath;
                this.mActionProperty.BindProperty(itemProperty);
                this.mActionProperty.label = this.TemplateData.actionDataList[index].GetType().Name;
                this.mActionProperty.RegisterValueChangeCallback((evt) =>
                {
                    Debug.Log($"ActionProperty Changed: {index}");
                    this.mActionList.RefreshItem(index);
                });
            }
        }

        public static IEnumerable<string> GetAllAssets<T>(string folder) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folder });
            return guids.Select(guid => AssetDatabase.GUIDToAssetPath(guid));
        }
    }
}