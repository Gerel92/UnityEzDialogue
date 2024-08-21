using System.Collections;
using UnityEngine;

namespace GMDialogue
{
    public class SequenceNode : CompositeNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            return NodeState.SUCCESS;
        }

        public override void RemoveChild(Node child)
        {
            children[children.IndexOf(child)] = null;

            for (int i = children.Count - 1; i >= 0; i--)
            {
                if (children[i] == null)
                    children.RemoveAt(i);
                else 
                    break;
            }
        }
    }
}