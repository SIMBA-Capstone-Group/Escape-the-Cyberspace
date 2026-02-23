
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
        /// <summary>
        /// This event is called when any key on keyboard is clicked.
        /// </summary>
        public event Action<VisualKeyForKeyboard> OnKeyClick;

        /// <summary>
        /// This event is called when some character is entered.
        /// </summary>
        public event Action<char> OnCharacterInput;

        [Header("Keyboard")]
        [Tooltip("A list of all keys.")]
        public List<VisualKeyForKeyboard> keys = new List<VisualKeyForKeyboard>(104);
        [Tooltip("If 'Shift' is hold right now? Or CapsLock mode is ON?")]
        public bool isShiftHold;
        [Tooltip("Small UI highlight mark over CapsLock key.")]
        [SerializeField] private Image shiftIndicator;
        [Tooltip("An optional UI text field to see keyboard's produced text.")]
        [SerializeField] private Text inputTextLabel;
        [Tooltip("Should we play sound when user press a key?")]
        public bool keyPressSound;
        [Tooltip("Should we play light animation when user press a key?")]
        public bool keyPressAnimation;
        [Tooltip("A color for key press animation.")]
        public Color keyPressAnimationColor;
        [SerializeField] private AudioSource audioSource;

        void OnEnable() {
            VisualKeyForKeyboard.OnKeyboardButtonClick += OnKeyboardButtonClick;
        }

        void OnDisable() {
            VisualKeyForKeyboard.OnKeyboardButtonClick -= OnKeyboardButtonClick;
        }

        /// <summary>
        /// Switch all keys highlight ON / OFF.
        /// </summary>
        /// <param name="isON"></param>
        public virtual void HighlightAllKeys(bool isON) {
            foreach (VisualKeyForKeyboard key in keys) {
                key.Highlight(isON);
            }
        }

        // A callback function. Don't call directly.
        protected virtual void OnKeyboardButtonClick(VisualKeyForKeyboard key) {
            Debug.Log($"[Visual Keyboard] Key is clicked: {key.gameObject.name}", gameObject);
            if (keyPressSound)
                audioSource.Play();
            if (keyPressAnimation)
                key.HighlightAnimation(keyPressAnimationColor, 1f);
            OnKeyClick?.Invoke(key);

            // Shift or CapsLock?
            if (key.oldKeyCode is KeyCode.LeftShift or KeyCode.RightShift or KeyCode.CapsLock) {
                isShiftHold = !isShiftHold;
                shiftIndicator.enabled = isShiftHold;
                return;
            }

            // Backspace?
            if (key.oldKeyCode is KeyCode.Backspace && inputTextLabel.text.Length > 0) {
                inputTextLabel.text = inputTextLabel.text.Substring(0, inputTextLabel.text.Length - 1);
            }

            // Some character?
            if (key.character != '\0') {
                char charEntered = isShiftHold ? key.shiftedCharacter : key.character;
                inputTextLabel.text += charEntered;
                OnCharacterInput?.Invoke(charEntered);
            }
        }

        /// <summary>
        /// Try to get a key by produced character.
        /// </summary>
        /// <returns>NULL if nothing found.</returns>
        public virtual VisualKeyForKeyboard GetKeyboardKey(char character) {
            string charAsString = character.ToString().ToLower();
            foreach (VisualKeyForKeyboard key in keys) {
                if (key.character == character)
                    return key;
            }
            return null;
        }

        /// <summary>
        /// Try to find a specific key by control path (actual for new Unity's Input System).
        /// </summary>
        /// <returns>NULL if nothing was found.</returns>
        public virtual VisualKeyForKeyboard GetKey(string controlPath) {

            // Try to find by direct path comparison.
            foreach (VisualKeyForKeyboard key in keys) {
                if (key.controlPath == controlPath) {
                    // Debug.Log($"Key was found by direct path comparison: {path} (device '{name}')", gameObject);
                    return key;
                }
            }

#if ENABLE_INPUT_SYSTEM
            // Try to find by binding mask.
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

            // Debug.Log($"Key was not found for path {path} (device '{name}')", gameObject);
            return null;
        }
        
        #region Editor
#if UNITY_EDITOR

        //[Header("Editor")]
        //[SerializeField] private List<Sprite> allSprites;
        //[SerializeField] private GameObject keyPrefab;
        //[SerializeField] private Transform container;

        //[ContextMenu("Editor - Create keys")]
        //void CreateKeys() {
        //    for (int i = 0; i < allSprites.Count; i++) {
        //        GameObject go = PrefabUtility.InstantiatePrefab(keyPrefab, container) as GameObject;
        //        VisualKeyForKeyboard key = go.GetComponent<VisualKeyForKeyboard>();
        //        // VisualKeyForKeyboard key = Instantiate(keyPrefab, container);
        //        keys.Add(key);
        //        key.image.sprite = allSprites[i];
        //        // key.EditorPlace();
        //    }
        //    EditorUtility.SetDirty(this);
        //    EditorUtility.SetDirty(gameObject);
        //}

        [ContextMenu("Editor - Check keys")]
        private void Check() {
            int c = 0;

            // Control path.
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

            //keys = new List<VisualKeyForKeyboard>(keys.Count);
            //foreach (VisualKey visualKey in keys) {
            //    keys.Add(visualKey as VisualKeyForKeyboard);
            //}
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

        //[Header("Replacing")]
        //public List<Sprite> newSprites;

        //[ContextMenu("Replace sprites")]
        //void Replace() {
        //    int success = 0;
        //    int errors = 0;
        //    foreach (VisualKeyForKeyboard key in keys) {
        //        bool done = false;
        //        foreach (Sprite newSprite in newSprites) {
        //            // Debug.Log($"Processing key. Its sprite name: {key.image.sprite.name}", key.gameObject);
        //            if (newSprite.name == key.image.sprite.name) {
        //                key.image.sprite = newSprite;
        //                success++;
        //                EditorUtility.SetDirty(key);
        //                EditorUtility.SetDirty(key.gameObject);
        //                done = true;
        //                break;
        //            }
        //        }
        //        if (!done) {
        //            errors++;
        //            Debug.Log($"Didn't replace sprite for key {key.gameObject.name}", key.gameObject);
        //        }
        //    }
        //    Debug.Log($"Job is done. {success} sprites were replaced. Errors: {errors}", gameObject);
        //}

        //        [ContextMenu("Editor - Calculate keys' normalized positions")]
        //        private void Editor_CalculateNormalizedPosition() {

        //            // Calculate min and max positions on board.
        //            Vector2 leftBottomCorner = new Vector2(99999f, 99999f);
        //            Vector2 topRightCorner = new Vector2(-99999f, -99999f);
        //            foreach (VisualKeyForKeyboard key in keys) {
        //                if (key.transform.localPosition.x < leftBottomCorner.x)
        //                    leftBottomCorner.x = key.transform.localPosition.x;
        //                if (key.transform.localPosition.y < leftBottomCorner.y)
        //                    leftBottomCorner.y = key.transform.localPosition.y;

        //                if (key.transform.localPosition.x > topRightCorner.x)
        //                    topRightCorner.x = key.transform.localPosition.x;
        //                if (key.transform.localPosition.y > topRightCorner.y)
        //                    topRightCorner.y = key.transform.localPosition.y;
        //            }
        //            float xRange = topRightCorner.x - leftBottomCorner.x;
        //            float YRange = topRightCorner.y - leftBottomCorner.y;
        //            Debug.Log($"leftBottomCorner: {leftBottomCorner}, topRightCorner: {topRightCorner}, xRange: {xRange}, YRange: {YRange}", gameObject);

        //            foreach (VisualKeyForKeyboard key in keys) {
        //                float normalizedX = (key.transform.localPosition.x - leftBottomCorner.x) / xRange;
        //                normalizedX = Mathf.Clamp(normalizedX, 0f, 1f);
        //                float normalizedY = (key.transform.localPosition.y - leftBottomCorner.y) / YRange;
        //                normalizedY = Mathf.Clamp(normalizedY, 0f, 1f);
        //                key.normalizedPosition = new Vector2(normalizedX, normalizedY);

        //                Debug.Log($"Key {key.gameObject.name}. localPos: {key.transform.localPosition}, normalizedX: {normalizedX}, normalizedY: {normalizedY}", gameObject);
        //#if UNITY_EDITOR
        //                EditorUtility.SetDirty(key);
        //                EditorUtility.SetDirty(key.gameObject);
        //#endif
        //            }
        //        }

        //        [ContextMenu("Get keys to list")]
        //        private void GetKeysToList() {
        //            keys = GetComponentsInChildren<VisualKeyForKeyboard>(true).ToList();
        //            EditorUtility.SetDirty(this);
        //            EditorUtility.SetDirty(gameObject);
        //        }

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

