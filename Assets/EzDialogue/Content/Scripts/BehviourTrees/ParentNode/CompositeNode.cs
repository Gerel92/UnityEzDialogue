using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMDialogue
{
    public abstract class CompositeNode : Node
    {
        /*[HideInInspector] */public List<Node> children = new List<Node>();

        public override List<Node> GetChildren()
        {
            return children;
        }

        public override void AddChild(Node child)
        {
            children.Add(child);
        }

        public override void RemoveChild(Node child)
        {
            children.Remove(child);
        }

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children.ConvertAll(c => c.Clone());
            return node;
        }
    }
}