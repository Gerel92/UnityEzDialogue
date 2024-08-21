using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMDialogue
{
    public abstract class Node : ScriptableObject
    {
        [NonSerialized] public NodeState state = NodeState.RUNNING;
        [NonSerialized] public bool started = false;

        [HideInInspector] public string guid;
        /*[HideInInspector]*/ public Vector2 position;

        public NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == NodeState.SUCCESS || state == NodeState.FAILURE)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();

        public abstract List<Node> GetChildren();
        public abstract void AddChild(Node child);
        public abstract void RemoveChild(Node child);

        public virtual List<Node> GetChildrenToExecute()
        {
            return GetChildren();
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        private void OnValidate()
        {
            onValidate?.Invoke();
        }

        public Action onValidate;
    }
}