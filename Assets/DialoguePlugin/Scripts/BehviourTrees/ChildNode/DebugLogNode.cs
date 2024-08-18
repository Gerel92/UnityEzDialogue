using UnityEngine;

namespace GMDialogue
{
    public class DebugLogNode : DecoratorNode
    {
        public string message;

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.SUCCESS;
        }
    }
}