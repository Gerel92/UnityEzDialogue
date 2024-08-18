using System.Collections;
using UnityEngine;

namespace GMDialogue
{
    public class WaitNode : DecoratorNode
    {
        public float duration = 1;

        private float timer;

        protected override void OnStart()
        {
            timer = 0;
        }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            timer += Time.deltaTime;
            return (timer >= duration) ? NodeState.SUCCESS : NodeState.RUNNING;
        }
    }
}