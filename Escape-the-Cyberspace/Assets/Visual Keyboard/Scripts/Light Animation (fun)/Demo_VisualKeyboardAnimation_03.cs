using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VisualKeyboard
{
    /// <summary>
    /// Add this component to keyboard game object to apply some light animation.
    /// </summary>
    public class Demo_VisualKeyboardAnimation_03 : MonoBehaviour
    {
        [Header("Wave lightning")]
        public string animatedText = "Hello world";
        public float randomLightDelay = 0.35f; 
        public bool autoStart = true;
        public VisualKeyboard keyboard;

        protected virtual
                void OnEnable() {
            if (keyboard == null)
                keyboard = GetComponent<VisualKeyboard>();
            if (keyboard == null)
                keyboard = GetComponentInParent<VisualKeyboard>();
            if (keyboard == null)
                keyboard = GetComponentInChildren<VisualKeyboard>();

            if (autoStart)
                StartAnimation();
        }

        protected virtual void OnDisable() {
            StopAllCoroutines();
        }

        public virtual void StartAnimation() {
            StopAllCoroutines();
            if (keyboard != null)
                StartCoroutine(WordsAnimation());
        }

        protected virtual IEnumerator WordsAnimation() {
            foreach (char ch in animatedText) {

                // Space - long delay.
                if (ch == ' ') {
                    yield return new WaitForSeconds(randomLightDelay * 2);
                    continue;
                }

                yield return new WaitForSeconds(randomLightDelay);
                VisualKeyForKeyboard key = keyboard.GetKeyboardKey(ch);
                if (key != null) {
                    key.HighlightON();
                }
                yield return new WaitForSeconds(randomLightDelay);
                if (key != null) {
                    key.HighlightOFF();
                }
            }
        }
    }
}