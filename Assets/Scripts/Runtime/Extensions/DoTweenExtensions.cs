using DG.Tweening;
using ModestTree;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abstraction 
{
    public static class DoTweenExtensions
    {
        public static void AbstractionSequence(List<Tweener> tweeners, Action act)
        {
            Sequence sequence = DOTween.Sequence();

            foreach (var tween in tweeners)
            {
                sequence.Append(tween);
            }

            sequence.OnComplete(() => act());
            sequence.Play();
        }
    }
}

