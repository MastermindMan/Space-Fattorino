using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public enum Mode { Instant, Timed, Both }

    [SerializeField] private Mode mode;
    [SerializeField] private float interactionTime = 0.3f;
    [SerializeField] private UnityEvent OnInstantInteraction;
    [SerializeField] private UnityEvent OnTimedInteraction;

    private bool interacting = false;

    public Mode InteractionMode => mode;

    public void OnInteractionKeyDown()
    {
        switch (mode)
        {
            case Mode.Instant:
                InstantInteract();
                break;
            case Mode.Timed:
            case Mode.Both:
                StartCoroutine(HoldingInteraction());
                break;
        }
    }
    public void OnInteractionKeyUp()
    {
        if (!interacting)
            return;
        StopAllCoroutines();
        QuitFromHolding();
        if (mode == Mode.Both)
            InstantInteract();
    }
    public void BreakInteraction()
    {
        if (interacting)
        {
            StopAllCoroutines();
            QuitFromHolding();
        }
    }

    private void InstantInteract()
    {
        OnInstantInteraction.Invoke();
        EventsManager.InstantInteracted.Invoke();
    }
    private void QuitFromHolding()
    {
        EventsManager.InteractionCompletion.Invoke(0);
        interacting = false;
    }

    private IEnumerator HoldingInteraction()
    {
        interacting = true;
        for (float time = 0; time < interactionTime; time += Time.deltaTime)
        {
            EventsManager.InteractionCompletion.Invoke(time / interactionTime);
            yield return null;
        }
        QuitFromHolding();
        OnTimedInteraction.Invoke();
    }

}
