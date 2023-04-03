using System.Collections.Generic;
using Action;
using Action.Base;

namespace Skill.Action
{
    public class ActionTemplate
    {
        private List<BaseAction> actionList;
        private int totalFrame;
        private int curFrame;
        public bool Finished { get; private set; }
        public bool Playing { get; private set; }

        public ActionTemplate(ActionTemplateData templateData)
        {
            this.totalFrame = templateData.totalFrame;
            this.curFrame = 0;
            this.actionList = new List<BaseAction>(templateData.actionDataList.Count);
            foreach (var data in templateData.actionDataList)
            {
                var action = ActionUtils.CreateAction(data);
                this.actionList.Add(action);
            }
            this.Finished = false;
            this.Playing = false;
        }

        public void Play()
        {
            if (this.Playing || this.Finished)
            {
                return;
            }
            foreach (var action in this.actionList)
            {
                action.OnInit();
            }
            this.Playing = true;
            //run at this frame 
            this.Update();
        }

        public void Update()
        {
            if (!this.Playing)
            {
                return;
            }
            foreach (var action in this.actionList)
            {
                action.Update(curFrame);
            }
            curFrame++;
            if (curFrame < totalFrame)
            {
                return;
            }
            foreach (var action in this.actionList)
            {
                action.OnClose();
            }
            this.Playing = false;
            this.Finished = true;
        }
    }
}
