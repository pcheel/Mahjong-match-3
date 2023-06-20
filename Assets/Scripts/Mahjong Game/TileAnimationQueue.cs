using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileAnimationQueue : MonoBehaviour
{
    private Queue<Tween> _animationQueue;
    private bool _readyToPlayNextAnimation;

    public void AddAnimationToQueue(Tween tween)
    {
        _animationQueue.Enqueue(tween);
        if (_animationQueue.Count == 1 && _readyToPlayNextAnimation)
        {
            PlayNextAnimation();
        }
    }
    public void PlayNextAnimation()
    {

    }

    private void Awake()
    {
        _animationQueue = new Queue<Tween>();
        _readyToPlayNextAnimation = true;
    }
}
