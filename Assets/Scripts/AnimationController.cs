using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator Animator;

    private float _timeToTextPlay = 10f;
    private int _currentAnimID = 1;


    public void PlayAnim(string animName)
    {
        Animator.Play(animName, 0, 0);
    }

    void Update()
    {
        _timeToTextPlay -= Time.deltaTime;
        if (_timeToTextPlay <= 0) 
        {
            PlayAnim($"MenuAnim{_currentAnimID}");
            _currentAnimID = _currentAnimID < 12 ? _currentAnimID + 1 : 1;
            _timeToTextPlay = 10f;
        }
    }
}
