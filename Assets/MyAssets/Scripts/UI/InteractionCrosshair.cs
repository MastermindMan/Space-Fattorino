using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{

    public class InteractionCrosshair : MonoBehaviour
    {
        private const float CROSSHAIR_POPUP_TIME = 0.1f;
        private const float PUNCH_ANIMATION_ON_INSTANT_INTERACT_TIME = 0.1f;
        [SerializeField] private Transform imagesParent;
        [SerializeField] private GameObject instantInteractionIndicator;
        [SerializeField] private GameObject fillablesParent;
        [SerializeField] private Image fillableImage;


        private void Start()
        {
            AddListeners();
            StartCoroutine(CrosshairAppearCoroutine(null));
        }

        private void StartLookingAtInteractable(Interactable interactable)
        {
            fillableImage.fillAmount = 0f;
            fillablesParent.SetActive(interactable.InteractionMode != Interactable.Mode.Instant);
            instantInteractionIndicator.transform.localScale = Vector3.one;
            instantInteractionIndicator.SetActive(interactable.InteractionMode != Interactable.Mode.Timed);
            StopAllCoroutines();
            StartCoroutine(CrosshairAppearCoroutine(interactable));
        }
        private void StopLookingAtInteractable(Interactable interactable)
        {
            interactable.BreakInteraction();
            StopAllCoroutines();
            StartCoroutine(CrosshairAppearCoroutine(null));
        }
        private void AnimateInstantInteraction()
        {
            StartCoroutine(InstantInteractAnimationCoroutine());
        }
        private void AnimateInteractionCompletion(float percentage)
        {
            fillableImage.fillAmount = percentage;
        }

        private IEnumerator CrosshairAppearCoroutine(Interactable interactable)
        {
            bool newTargetExists = interactable != null;

            if (newTargetExists)
                imagesParent.gameObject.SetActive(true);
            Vector3 targetSize = newTargetExists ? Vector3.one : Vector3.zero;
            float ratio = Mathf.Abs(targetSize.x - imagesParent.localScale.x);
            float time = CROSSHAIR_POPUP_TIME * ratio;

            yield return ScaleTowards(imagesParent, targetSize, time);

            if (!newTargetExists)
                imagesParent.gameObject.SetActive(false);
        }
        private IEnumerator InstantInteractAnimationCoroutine()
        {
            float time = PUNCH_ANIMATION_ON_INSTANT_INTERACT_TIME / 2;
            instantInteractionIndicator.transform.localScale = Vector3.one;

            yield return ScaleTowards(instantInteractionIndicator.transform, Vector3.one * 0.5f, time);
            yield return ScaleTowards(instantInteractionIndicator.transform, Vector3.one, time);
        }
        private IEnumerator ScaleTowards(Transform transform, Vector3 targetSize, float time)
        {
            for (float timer = 0; timer < time; timer += Time.deltaTime)
            {
                float speed = Mathf.Abs(targetSize.x - transform.localScale.x) / (time - timer);
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, speed * Time.deltaTime);
                yield return null;
            }
            transform.localScale = targetSize;
        }

        private void AddListeners()
        {
            EventsManager.StartsLookingAtInteractable.AddListener(StartLookingAtInteractable);
            EventsManager.StopsLookingAtInteractable.AddListener(StopLookingAtInteractable);
            EventsManager.InstantInteracted.AddListener(AnimateInstantInteraction);
            EventsManager.InteractionCompletion.AddListener(AnimateInteractionCompletion);
        }

    }

}