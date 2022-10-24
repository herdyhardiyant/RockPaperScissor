using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            ChooseAttack,
            Attack,
            Damaged,
            Draw,
            GameOver
        }

        public Player player1;
        public Player player2;
        public TMP_Text WinnerText;
    
        private GameState _gameState;
        private Player damagedPlayer;

        private void Start()
        {
            WinnerText.gameObject.SetActive(false);
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.ChooseAttack:
                    if (player1.AttackValue != null && player2.AttackValue != null)
                    {
                        player1.chosenCard.AnimateAttack();
                        player2.chosenCard.AnimateAttack();
                        player1.SetCardsClickable(false);
                        player2.SetCardsClickable(false);
                        _gameState = GameState.Attack;
                    
                    }
                    break;
            
                case GameState.Attack:
                    if (player1.chosenCard.IsAnimating() == false && player2.chosenCard.IsAnimating() == false)
                    {
                        damagedPlayer = GetDamagedPlayer();
                        if (damagedPlayer)
                        {
                            damagedPlayer.chosenCard.AnimateDamage();
                            _gameState = GameState.Damaged;
                        }
                        else
                        {
                            player1.chosenCard.AnimateDraw();
                            player2.chosenCard.AnimateDraw();
                            _gameState = GameState.Draw;
                        }
                    }
                    break;
            
                case GameState.Damaged:
                    if (damagedPlayer == player1)
                    {
                        player1.ChangeHealth(-20);
                    }
                    else
                    {
                        player2.ChangeHealth(-20);
                    }

                    var winner = GetWinner();
                    if (!winner)
                    {
                        _gameState = GameState.ChooseAttack;
                        ResetPlayers();
                    }
                    else
                    {
                        _gameState = GameState.GameOver;
                    }
                    break;
            
                case GameState.Draw:
                    ResetPlayers();
                    _gameState = GameState.ChooseAttack;
                    break;
            
                case GameState.GameOver:
                    WinnerText.text = GetWinner().name + " wins!";
                    WinnerText.gameObject.SetActive(true);
                    break;
            
            }
        }

        private void ResetPlayers()
        {
            damagedPlayer = null;
        
            player1.ResetPlayer();
            player2.ResetPlayer();
        
            player1.SetCardsClickable(true);
            player2.SetCardsClickable(true);
        }

        private Player GetDamagedPlayer()
        {
            Attack? player1Attack = player1.AttackValue;
            Attack? player2Attack = player2.AttackValue;

            if (player1Attack == Attack.rock && player2Attack == Attack.paper)
            {
                return player1;
            }
            else if (player1Attack == Attack.paper && player2Attack == Attack.scissors)
            {
                return player1;
            }
            else if (player1Attack == Attack.scissors && player2Attack == Attack.rock)
            {
                return player1;
            }
            else if (player2Attack == Attack.rock && player1Attack == Attack.paper)
            {
                return player2;
            }
            else if (player2Attack == Attack.paper && player1Attack == Attack.scissors)
            {
                return player2;
            }
            else if (player2Attack == Attack.scissors && player1Attack == Attack.rock)
            {
                return player2;
            }

            return null;
        }

        private Player GetWinner()
        {
            if (player1.Health == 0)
            {
                return player2;
            }
            else if (player2.Health == 0)
            {
                return player1;
            }
            else
            {
                return null;
            }
        }
    }
}