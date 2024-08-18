using System;
using UnityEditor;
using UnityEngine;

namespace GMDialogue
{
    [Serializable]
    public class Choice : object
    {
        public string[] choices = new string[] { "Option 1", "Option 2" };
    }
}