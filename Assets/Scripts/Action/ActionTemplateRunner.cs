using UnityEngine;

namespace Skill.Action
{
    public class ActionTemplateRunner : MonoBehaviour
    {
        public ActionTemplateData templateData;
        private ActionTemplate actionTemplate;

        void Awake()
        {
            actionTemplate = new ActionTemplate(templateData);
        }

        void Start()
        {
            actionTemplate.Play();
        }

        void Update()
        {
            actionTemplate.Update();
        }
    }
}
