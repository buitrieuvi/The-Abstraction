using DG.Tweening;
using ModestTree;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abstraction 
{
    public static class DoTweenExtensions
    {
        public static void DoSequenceMoveDelay<T>(List<T> slots, Action act, float timeComplete, float timeDelay) where T : SlotBase
        {
            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < slots.Count; i++)
            {
                var item = slots[i];
                item.SlotViewBase.Rt?.DOKill();

                seq.Join(item.SlotViewBase.Rt.DOAnchorPosX(0f, timeComplete).SetDelay(i * timeDelay));
            }

            seq.OnComplete(() => act?.Invoke());
        }

        public static void DoSequenceScaleDelay<T>(List<T> slots, Action act, float timeComplete, float timeDelay) where T : SlotBase
        {
            slots.ForEach(slot =>
            {
                slot.SlotViewBase.Rt.localScale = Vector3.zero;
            });

            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < slots.Count; i++)
            {
                var item = slots[i];
                item.SlotViewBase.Rt?.DOKill();

                seq.Join(item.SlotViewBase.Rt.DOScale(1f, timeComplete).SetDelay(i * timeDelay));
            }

            seq.OnComplete(() => act?.Invoke());
        }

        public static void DoSequenceScaleXDelay<T>(List<T> slots, Action act, float timeComplete, float timeDelay) where T : SlotBase
        {
            slots.ForEach(slot =>
            {
                slot.SlotViewBase.Rt.localScale = new Vector3(0, 1, 1);
            });

            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < slots.Count; i++)
            {
                var item = slots[i];
                item.SlotViewBase.Rt?.DOKill();

                seq.Join(item.SlotViewBase.Rt.DOScaleX(1f, timeComplete).SetDelay(i * timeDelay));
            }

            seq.OnComplete(() => act?.Invoke());
        }


        public static void TweenersAndAction(List<Tweener> tweeners, Action c)
        {
            Sequence seq = DOTween.Sequence();

            tweeners.ForEach(t =>
            {
                seq.Join(t);
            });

            seq.OnComplete(() => c?.Invoke());
        }
    }
}

