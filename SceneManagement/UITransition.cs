using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UITransition : Singleton<UITransition>
{
    [SerializeField] private UnityEngine.UI.Image transitionScreen;
    [SerializeField] private float transitionSpeed;

    private IEnumerator transitionRoutine;
    public void TransitionToBlack()
    {
        if (transitionRoutine != null)
        { 
            StopCoroutine(transitionRoutine);
        }
        transitionRoutine = TransitionRoutine(1);
        StartCoroutine(transitionRoutine);
    }
    public void TransitionToClear()
    {
        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
        }
        transitionRoutine = TransitionRoutine(0);
        StartCoroutine(transitionRoutine);
    }
    private IEnumerator TransitionRoutine(float targetAlpha)
    {
        while(!Mathf.Approximately(transitionScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(transitionScreen.color.a, targetAlpha, transitionSpeed * Time.deltaTime);
            transitionScreen.color = new Color(transitionScreen.color.r, transitionScreen.color.g, transitionScreen.color.b, alpha);
            yield return null;
        }
    }
}
