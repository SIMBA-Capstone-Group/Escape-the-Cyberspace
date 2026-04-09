using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualKeyboard
{
    public class VisualKeyboard : MonoBehaviour
    {
        public event Action<VisualKeyForKeyboard> OnKeyClick;
        public event Action<char> OnCharacterInput;

        [Header("Keyboard")]
        [Tooltip("A list of all keys.")]
        public List<VisualKeyForKeyboard> keys = new List<VisualKeyForKeyboard>(104);
        [Tooltip("If 'Shift' is hold right now? Or CapsLock mode is ON?")]
        public bool isShiftHold;
        [Tooltip("Small UI highlight mark over CapsLock key.")]
        [SerializeField] private Image shiftIndicator;

        // CHANGED: Text to InputField
        [Tooltip("An optional UI text field to see keyboard's produced text.")]
        [SerializeField] private InputField inputTextLabel;

        [Tooltip("Should we play sound when user press a key?")]
        public bool keyPressSound;
        [Tooltip("Should we play light animation when user press a key?")]
        public bool keyPressAnimation;
        [Tooltip("A color for key press animation.")]
        public Color keyPressAnimationColor;
        [SerializeField] private AudioSource audioSource;

        void OnEnable()
        {
            VisualKeyForKeyboard.OnKeyboardButtonClick += OnKeyboardButtonClick;
        }

        void OnDisable()
        {
            VisualKeyForKeyboard.OnKeyboardButtonClick -= OnKeyboardButtonClick;
        }

        public virtual void HighlightAllKeys(bool isON)
        {
            foreach (VisualKeyForKeyboard key in keys)
            {
                key.Highlight(isON);
            }
        }

        protected virtual void OnKeyboardButtonClick(VisualKeyForKeyboard key)
        {
            if (key.parentKeyboard != this)
                return;
            Debug.Log($"[Visual Keyboard] Key is clicked: {key.gameObject.name}", gameObject);
            if (keyPressSound)
                audioSource.Play();
            if (keyPressAnimation)
                key.HighlightAnimation(keyPressAnimationColor, 1f);
            OnKeyClick?.Invoke(key);

            if (key.oldKeyCode is KeyCode.LeftShift or KeyCode.RightShift or KeyCode.CapsLock)
            {
                isShiftHold = !isShiftHold;
                shiftIndicator.enabled = isShiftHold;
                return;
            }

            if (key.oldKeyCode is KeyCode.Backspace && inputTextLabel.text.Length > 0)
            {
                inputTextLabel.text = inputTextLabel.text.Substring(0, inputTextLabel.text.Length - 1);
            }

            if (key.character != '\0')
            {
                char charEntered = isShiftHold ? key.shiftedCharacter : key.character;
                inputTextLabel.text += charEntered;
                OnCharacterInput?.Invoke(charEntered);
            }
        }

        public virtual VisualKeyForKeyboard GetKeyboardKey(char character)
        {
            string charAsString = character.ToString().ToLower();
            foreach (VisualKeyForKeyboard key in keys)
            {
                if (key.character == character)
                    return key;
            }
            return null;
        }

        public virtual VisualKeyForKeyboard GetKey(string controlPath)
        {
            foreach (VisualKeyForKeyboard key in keys)
            {
                if (key.controlPath == controlPath)
                {
                    return key;
                }
            }

#if ENABLE_INPUT_SYSTEM
            InputBinding searchedMask = new InputBinding(path: controlPath);
            foreach (VisualKeyForKeyboard key in keys) {
                InputBinding keyMask = new InputBinding(path: key.controlPath);
                if (searchedMask.Matches(keyMask)) {
                    Debug.Log($"Key was found by mask matching for path {controlPath}. Key: {key.gameObject.name}. Searched mask matches key mask", key.gameObject);
                    return key;
                }

                if (keyMask.Matches(searchedMask)) {
                    Debug.Log($"Key was found by mask matching for path {controlPath}. Key: {key.gameObject.name}. Key mask matches searched mask", key.gameObject);
                    return key;
                }
            }
#endif
            return null;
        }

        #region Editor
#if UNITY_EDITOR

        [ContextMenu("Editor - Check keys")]
        private void Check() {
            int c = 0;
            foreach (VisualKeyForKeyboard key in keys) {
                if (string.IsNullOrEmpty(key.controlPath)) {
                    c++;
                    Debug.Log($"Key {key.gameObject.name} has no path.", gameObject);
                }
            }
            Debug.Log($"Total missed control paths: {c}", gameObject);

            c = 0;
            foreach (VisualKeyForKeyboard key in keys) {
                if (key.oldKeyCode == KeyCode.None) {
                    c++;
                    Debug.Log($"Key {key.gameObject.name} has no key code for old system.", key.gameObject);
                }
            }
            Debug.Log($"Total missed key codes: {c}", gameObject);
        }

        [ContextMenu("Editor - Sort children by name")]
        private void Editor_SortChildrenByName() {
            var sorted = keys.OrderBy((item) => item.gameObject.name).ToList();
            for (int i = 0; i < sorted.Count; i++) {
                Debug.Log($"{i}: {sorted[i].gameObject.name}", gameObject);
                sorted[i].transform.SetAsLastSibling();
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(this.gameObject);
#endif
        }

        [ContextMenu("Editor - Set Dirty")]
        private void Editor_SetDirty() {
            keys = new List<VisualKeyForKeyboard>(keys.Count);
            foreach (VisualKeyForKeyboard key in keys) {
                keys.Add(key);
                EditorUtility.SetDirty(key);
                EditorUtility.SetDirty(key.gameObject);
            }

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(gameObject);
        }

#endif
        #endregion Editor
    }
}