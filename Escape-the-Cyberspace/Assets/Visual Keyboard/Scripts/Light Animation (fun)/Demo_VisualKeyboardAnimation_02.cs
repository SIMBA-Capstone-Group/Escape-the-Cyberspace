
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VisualKeyboard
{
    /// <summary>
    /// Add this component to keyboard game object to apply some light animation.
    /// </summary>
    public class Demo_VisualKeyboardAnimation_02 : MonoBehaviour
    {
        [Header("Random lights")]
        public Color color = Color.yellow;
        public float randomLightDelay = 0.3f;
        public float lightDuration = 1f;
        public bool autoStart = true;
        public VisualKeyboard keyboard;

        protected virtual void OnEnable() {
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
                StartCoroutine(RandomLightAnimation());
        }

        protected virtual IEnumerator RandomLightAnimation() {
            while (true) {
                yield return new WaitForSeconds(randomLightDelay);
                //foreach (VisualKeyForKeyboard key in keyboard.keys) {
                //    key.HighlightOFF();
                //}
                keyboard.keys[Random.Range(0, keyboard.keys.Count)].HighlightAnimation(color, lightDuration);
            }
        }

    }
}