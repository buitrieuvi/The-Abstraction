using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Abstraction
{
    [RequireComponent(typeof(Image))]
    public class DarkPanel : MonoBehaviour
    {
        public Image darkImage;
        public float fadeDuration = 0.3f;

        private void Reset()
        {
            darkImage = GetComponent<Image>();
            darkImage.color = new Color(0, 0, 0, 0);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            darkImage.Fade(0f);
        }

        private async UniTask FadeIn()
        {
            gameObject.SetActive(true);
            await darkImage.DOFade(1f, fadeDuration).ToUniTask();
        }

        private async UniTask FadeOut()
        {
            await darkImage.DOFade(0f, fadeDuration).OnComplete(Hide).ToUniTask();
        }

        public async void TransitionAsync(Func<UniTask> callBack)
        {
            await FadeIn();
            await callBack();
            await FadeOut();
        }

        public async UniTask Transition(Action callBack)
        {
            await FadeIn();
            callBack?.Invoke();
            await UniTask.WaitForSeconds(0.25f);
            await FadeOut();
        }
    } 
}