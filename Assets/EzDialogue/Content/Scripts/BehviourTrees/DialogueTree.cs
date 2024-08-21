using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GMDialogue
{
    [CreateAssetMenu(menuName = "GMDialogue/DialogueTree")]
    public class DialogueTree : ScriptableObject
    {
        [HideInInspector] public NodeState state = NodeState.RUNNING;
        [HideInInspector] public Node rootNode;
        [HideInInspector] public List<Node> nodes = new List<Node>();

        private Stack<Node> nodesToExecute = new Stack<Node>();

        private void Awake()
        {
            nodesToExecute.Push(rootNode);
        }

        public NodeState Update()
        {
            if (nodesToExecute.Count == 0)
                return state = NodeState.SUCCESS;

            switch (nodesToExecute.Peek().state)
            {
                case NodeState.RUNNING:
                    break;

                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;

                case NodeState.SUCCESS:
                    GetNextToExecute();
                    break;
            }

            if (nodesToExecute.Count == 0) 
                return state = NodeState.SUCCESS;

            nodesToExecute.Peek().Update();

            return state;
        }

        private void GetNextToExecute()
        {
            List<Node> nexts = nodesToExecute.Pop().GetChildrenToExecute();

            for (int i = nexts.Count - 1; i >= 0; i--)
                if (nexts[i] != null)
                    nodesToExecute.Push(nexts[i]);
        }

        public Node CreateNode(Type type)
        {
            Node node = CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public DialogueTree Clone()
        {
            DialogueTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
    }
}