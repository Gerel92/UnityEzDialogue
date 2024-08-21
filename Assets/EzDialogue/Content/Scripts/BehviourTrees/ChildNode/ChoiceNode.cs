using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GMDialogue
{
    public class ChoiceNode : CompositeNode
    {
        public Choice choices;

        private int choiceIndex;

        protected override void OnStart()
        {
            choiceIndex = -1;
            // spawn choice
            // subscribe to choice
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            return (choiceIndex == -1) ? NodeState.RUNNING : NodeState.SUCCESS;
                    
        }

        public override List<Node> GetChildrenToExecute()
        {
            return new List<Node>() { children[choiceIndex] };
        }

        public void OnChoose(int choiceIndex)
        {
            // unsubscribe to choice
            this.choiceIndex = choiceIndex;
        }
    }
}