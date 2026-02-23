
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VisualKeyboard
{
    /// <summary>
    /// Add this component to keyboard game object to apply some light animation.
    /// </summary>
    public class Demo_VisualKeyboardAnimation_01 : MonoBehaviour
    {
        [Header("Wave lightning")]
        public float waveSpeed = 0.3f;
        public bool autoStart = true;
        public VisualKeyboard keyboard;

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

        void OnDisable() {
            StopAllCoroutines();
        }

        [ContextMenu("Wave light animation")]
        public virtual void StartAnimation() {
            StopAllCoroutines();
            if (keyboard != null)
                StartCoroutine(WaveLightAnimation());
        }

        IEnumerator WaveLightAnimation() {
            foreach (VisualKeyForKeyboard key in keyboard.keys) {
                key.HighlightOFF();
            }

            while (true) {
                yield return null;
                foreach (VisualKeyForKeyboard key in keyboard.keys) {
                    float hue = Time.time * waveSpeed + key.normalizedPosition.x;
                    if (hue > 1f)
                        hue = hue - MathF.Floor(hue);
                    Color col = Random.ColorHSV(hue, hue, 0.3f, 0.301f, 0.999f, 1f, 0.999f, 1f);
                    key.HighlightON(col);
                }
            }
        }
    }
}