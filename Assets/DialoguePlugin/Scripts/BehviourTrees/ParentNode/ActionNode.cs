using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMDialogue
{
    public abstract class ActionNode : Node
    {
        public override List<Node> GetChildren()
        {
            return new List<Node>();
        }

        public override void AddChild(Node child) { }

        public override void RemoveChild(Node child) { }
    }
}