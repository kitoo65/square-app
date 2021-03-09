using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    [SerializeField] private Text actionText;

    public string currentAction;

    private bool performing = false;

    private void Awake()
    {
    }

    void Start()
    {
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        if (Menu.Instance.jumping && !performing)
            ActionStarted("jumping");
        if (Menu.Instance.spinning && !performing)
            ActionStarted("spinning");
        if (Menu.Instance.pingPonging && !performing)
            ActionStarted("pingPonging");
        if (Menu.Instance.playingAnimation && !performing)
            ActionStarted("playAnimation");
    }

    public void ActionStarted(string action)
    {
        performing = true;
        currentAction = action;
        StartCoroutine(Perform(action));
    }

    public string ActionFinished()
    {
        performing = false;
        return currentAction;
    }

    IEnumerator Perform(string action)
    {
        float timer = 0f;
        float actionTime = 5f;

        while (timer < actionTime)
        {
            float percentCompleted = timer / actionTime;

            if (currentAction == "jumping")
            {
                Vector2 jumpVectorThisFrame =
                    Vector2.Lerp((Vector2.up + new Vector2(0, 50)), Vector2.zero, percentCompleted);
                transform.localPosition = jumpVectorThisFrame;
            }

            if (currentAction == "spinning")
            {
                float zRotation = Mathf.Lerp(0, 360, percentCompleted) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                    zRotation);
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }
}