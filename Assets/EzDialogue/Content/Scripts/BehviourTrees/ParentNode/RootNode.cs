using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GMDialogue
{
    public class RootNode : Node
    {
        /*[HideInInspector] */public Node child;

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

        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override NodeState OnUpdate() 
        { 
            return NodeState.SUCCESS; 
        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}