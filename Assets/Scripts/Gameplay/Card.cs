using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class Card : MonoBehaviour
    {
        public Attack AttackValue;
        public CardPlayer cardPlayer;
        public Transform atkPosition;
        public float tweenTime = 0.5f;
        private Vector2 startPosition;
        private Vector2 startScale;
        private Color startColor;

        private Button _button;

        private Tweener animationTweener;
        private Image _cardImage;


        private void Start()
        {
            startScale = transform.localScale;
        }

        public void OnClick()
        {
            if (_button.enabled == false) return;
            startPosition = transform.position;
            cardPlayer.SetChosenCard(this);
        }

        public void SetSelectedCardBig()
        {
            animationTweener = _cardImage.transform.DOScale(startScale * 1.5f, tweenTime);
        }

        public void SetSelectedCardSmall()
        {
            animationTweener = _cardImage.transform.DOScale(startScale, tweenTime);
        }

        public void SelectCardSilently()
        {
            if (_button.enabled == false) return;
            startPosition = transform.position;
            cardPlayer.SetChosenCard(this);
        }

        public void SetClickable(bool clickable)
        {
            _button.enabled = clickable;
        }

        public void ResetCard()
        {
            Transform cardTransform = transform;
            cardTransform.position = startPosition;
            _cardImage.color = startColor;

            SetSelectedCardSmall();
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _cardImage = GetComponent<Image>();
            var cardTransform = transform;
            startPosition = cardTransform.position;
            startScale = cardTransform.localScale;
            startColor = _cardImage.color;

            var button = GetComponent<Button>();
        }

        public void AnimateDamage()
        {
            animationTweener = _cardImage.DOColor(Color.red, tweenTime).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f);
        }

        public void AnimateAttack()
        {
            animationTweener = transform.DOMove(atkPosition.position, tweenTime)
                .OnComplete(() => { transform.position = startPosition; });
            animationTweener = _cardImage.transform.DOScale(startScale, tweenTime);
        }

        public bool IsAnimating()
        {
            return animationTweener.IsActive();
        }

        public void AnimateDraw()
        {
            animationTweener = _cardImage.DOColor(Color.blue, tweenTime).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f);
        }
    }
}