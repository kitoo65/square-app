using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NewMenu : MonoBehaviour
{
    public float animationTime;
    enum State {notActing, acting};
    State state;
    NewSquare square;
    RectTransform squareRectTransform;
    public Text actionText;
    AudioSource audioSourceComponent;
    public AudioClip buttonAudioClip;
    public AudioClip squareAudioClip;
    [Header("UI Positions")]
    public RectTransform upperPosition;
    public Vector3 middlePosition;
    public RectTransform leftPosition;
    public RectTransform rightPosition;

    //Lo que utilice para realizar la tarea, fue un plugin llamado "DOTween". Permite hacer animaciones en la UI y en Unity de forma ordenada y controlada.
    //Para chequear funcionalidades, aqui esta el link a la documentacion de DOTween. http://dotween.demigiant.com/documentation.php
    //La idea es bastante simple, a partir de un solo controlador de Menu, puedo controlar el Behaviour de mi Square. 
    private void Awake()
    {
        square = FindObjectOfType<NewSquare>();
        squareRectTransform = square.GetComponent<RectTransform>();
        middlePosition = squareRectTransform.position;
        audioSourceComponent = GetComponent<AudioSource>();
        state = State.notActing;
        
    }
    
    public void TriggerAnimation(string animation)
    {
        audioSourceComponent.PlayOneShot(buttonAudioClip);
        if (state == State.notActing)
        {
            StartCoroutine(Animating(animation));
        }
    }

    IEnumerator Animating(string anim)
    {
        state = State.acting;
        if (anim == "Jumping")
        {
            actionText.text = anim;
            Sequence jumpSequence = DOTween.Sequence(); //La secuencia, es un conjunto de animaciones, la forma en la que se concatenan, es poniendo .Append al final de cada una. 
            //Tambien se pueden concatenar animaciones de forma paralela, con .Join (se reproducen en simultaneo)
            jumpSequence.Append(squareRectTransform.DOMove(upperPosition.position, animationTime)).Append(squareRectTransform.DOMove(middlePosition, animationTime));
            yield return jumpSequence.WaitForCompletion(); //Este estado espera a que finalice y muera la animacion.
            state = State.notActing;
            actionText.text = "Insert Action";
        }
        if (anim == "Rotating")
        {
            actionText.text = anim;
            Tween squareRotateZ = squareRectTransform.DORotate(new Vector3(0, 0, 360), animationTime, RotateMode.FastBeyond360);
            yield return squareRotateZ.WaitForCompletion();
            state = State.notActing;
            actionText.text = "Insert Action";
        }
        if (anim == "PingPoning")
        {
            actionText.text = anim;
            Sequence pingPongSequence = DOTween.Sequence(); 
            pingPongSequence.Append(squareRectTransform.DOMove(leftPosition.position, animationTime)).Append(squareRectTransform.DOMove(rightPosition.position, animationTime)).Append(squareRectTransform.DOMove(middlePosition, animationTime));
            yield return pingPongSequence.WaitForCompletion();
            state = State.notActing;
            actionText.text = "Insert Action";
        }
        if (anim == "Animation")
        {
            actionText.text = anim;
            Sequence animationSequence = DOTween.Sequence();
            animationSequence.Append(squareRectTransform.DOScale(new Vector3(0, 0, 0), 3)).Join(squareRectTransform.DORotate(new Vector3(0,0,360),3,RotateMode.FastBeyond360));
            audioSourceComponent.PlayOneShot(squareAudioClip);
            yield return animationSequence.WaitForCompletion();
            squareRectTransform.DOScale(new Vector3(1,1,1),0.2f);
            state = State.notActing;
            actionText.text = "Insert Action";

        }
    }


}
