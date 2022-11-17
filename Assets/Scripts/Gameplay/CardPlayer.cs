using System;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CardPlayer : MonoBehaviour
    {
        [SerializeField] public Card chosenCard;
        public Attack? AttackValue => !chosenCard ? null : chosenCard.AttackValue;
        public float Health => _currentHealth;
        public float MaxHealth => _maxHealth;
        public TMP_Text healthText;
        public float AttackDamage => _attackDamage;
        
        private float _attackDamage = -20;
        private float _maxHealth = 100;
        private float _currentHealth;
        private Card[] _cards;
        
        private void Awake()
        {
            _cards = GetComponentsInChildren<Card>();

         
            var bot = GetComponent<Bot>();
            if (bot)
            {
                ApplyBotDifficulty();
            }
            
            _currentHealth = _maxHealth;
            healthText.text = _currentHealth + "/" + _maxHealth;
            print(healthText.text);
        }

        private void ApplyBotDifficulty()
        {
            _maxHealth = BotDifficulty.Health;
            _attackDamage = BotDifficulty.AttackDamage;
        }

        public void SetChosenCard(Card card)
        {
            if (chosenCard != null)
            {
                chosenCard.ResetCard();
            }

            chosenCard = card;
        }

        public void ChangeHealth(float health)
        {
            _currentHealth += health;
            healthText.text = _currentHealth + "/" + _maxHealth;
        }

        public void ResetPlayer()
        {
            if(chosenCard)
            {
                chosenCard.ResetCard();
            }
       
            chosenCard = null;
        }

        public void SetCardsClickable(bool clickable)
        {
            foreach (Card card in _cards)
            {
                card.SetClickable(clickable);
            }
        }
    }
}