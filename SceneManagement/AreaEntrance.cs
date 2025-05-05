using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Awake()
    {
        if(transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            Movement.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UITransition.Instance.TransitionToClear();
        }
    }
}
