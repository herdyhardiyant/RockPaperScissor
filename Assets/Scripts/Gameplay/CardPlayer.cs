using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CardPlayer : MonoBehaviour
    {
        [SerializeField] public Card chosenCard;

        private readonly float maxHealth = 100;
        private float currentHealth;
        public TMP_Text healthText;
        private Card[] cards;
    
        private void Awake()
        {
            cards = GetComponentsInChildren<Card>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public Attack? AttackValue => !chosenCard ? null : chosenCard.AttackValue;

        public float Health => currentHealth;
        public float MaxHealth => maxHealth;

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
            currentHealth += health;
            healthText.text = currentHealth + "/" + maxHealth;
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
            foreach (Card card in cards)
            {
                card.SetClickable(clickable);
            }
        }
    }
}