using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GMDialogue
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SentenceDisplay : MonoBehaviour
    {
        [SerializeField] private float delayBetweenChar = 0.1f;

        public static SentenceDisplay Instance { get; private set; }

        public Action OnTextEnd;
        private Action DoAction;

        private TextMeshProUGUI textField;
        private string fullText;
        private int charIndex;
        private float timer;

        private void Awake()
        {
            if (Instance != null) Debug.LogError($"Multiple ReplicaText in scene");
            Instance = this;

            textField = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            fullText = text;
            charIndex = 0;
            timer = 0;
            DoAction = DoActionDisplay;
        }

        private void Update()
        {
            DoAction?.Invoke();
        }

        private void DoActionDisplay()
        {
            timer += Time.deltaTime;

            int newIndex = Mathf.FloorToInt(timer / delayBetweenChar);

            if (charIndex == newIndex)
                return;

            charIndex = newIndex;
            textField.text = fullText.Substring(0, charIndex);

            if (charIndex >= fullText.Length)
                DoAction = DoActionWaitForInput;
        }

        private void DoActionWaitForInput()
        {
            OnTextEnd?.Invoke();
            DoAction = null;
        }
    }
}