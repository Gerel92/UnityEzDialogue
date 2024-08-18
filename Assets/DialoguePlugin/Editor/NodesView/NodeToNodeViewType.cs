using System;
using UnityEditor;
using UnityEngine;

namespace GMDialogue.EditorUI
{
    public static class NodeToNodeViewType
    {
        public static Type GetNodeViewType(Type nodeType)
        {
            if (!nodeType.IsSubclassOf(typeof(Node))) return null;

            if (nodeType == typeof(RootNode)) return typeof(RootNodeView);
            if (nodeType == typeof(SentenceNode)) return typeof(SentenceNodeView);
            if (nodeType == typeof(SequenceNode)) return typeof(SequenceNodeView);
            if (nodeType == typeof(ChoiceNode)) return typeof(ChoiceNodeView);

            if (nodeType.IsSubclassOf(typeof(DecoratorNode))) return typeof(DecoratorNodeView);

            return typeof(NodeView);
        }
    }
}