using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMDialogue
{
    public class RepeatNode : DecoratorNode
    {
        public uint loops = 3;

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate() 
        { 
            return NodeState.SUCCESS;
        }

        public override List<Node> GetChildrenToExecute()
        {
            List<Node> nodes = new List<Node>();

            if (child == null) return nodes;

            for (int i = 0; i < loops; i++)
                nodes.Add(child);

            return nodes;
        }
    }
}