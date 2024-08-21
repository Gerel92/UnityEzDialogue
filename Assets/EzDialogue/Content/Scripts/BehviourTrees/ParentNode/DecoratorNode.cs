using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMDialogue
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node child;

        public override List<Node> GetChildren()
        {
            List<Node> children = new List<Node>();
            if (child != null) children.Add(child);
            return children;
        }

        public override void AddChild(Node child)
        {
            this.child = child;
        }

        public override void RemoveChild(Node child)
        {
            child = null;
        }

        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);

            if (node.child != null)
                node.child = child.Clone();

            return node;
        }
    }
}