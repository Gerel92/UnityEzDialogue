using UnityEngine;

namespace GMDialogue
{
    [CreateAssetMenu(fileName = "New Character", menuName = "DialogueSystem/Character")]
    public class Character : ScriptableObject
    {
        public string charaName;
        public Sprite sprite;
    }
}