using System;
using OnlineConnection;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class CardGameManager : MonoBehaviour
    {
        public enum GameState
        {
            NetPlayerInit,
            ChooseAttack,
            Attack,
            Damaged,
            Draw,
            GameOver
        }

        public bool IsOnline => _isOnline;
        private static bool _isOnline;
        
        public CardPlayer player1;
        public CardPlayer player2;
        public TMP_Text WinnerText;
        public GameObject netPlayerPrefab;
        private GameState _gameState;
        private CardPlayer _damagedCardPlayer;
        [SerializeField] private GameObject _restartButton;
        
        public void RestartGame()
        {
            SceneManager.LoadScene("Gameplay");
        }
        
        private void Awake()
        {
            _gameState = GameState.NetPlayerInit;
            WinnerText.gameObject.SetActive(false);
            _restartButton.SetActive(false);
            _isOnline = true;
        }

        private void Start()
        {
            
            var netPlayerGameObject =
                PhotonNetwork.Instantiate(netPlayerPrefab.name, Vector3.zero, Quaternion.identity);

            if (!netPlayerGameObject)
            {
                _gameState = GameState.ChooseAttack;
                _isOnline = false;
                print("No net player");
            }

        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.NetPlayerInit:
                    if(NetPlayer.players.Count == 2)
                    {
                        foreach (var player in NetPlayer.players)
                        {
                            print("Player: " + player);
                            if (player.photonView.IsMine)
                            {
                                player.Set(player1);
                            }
                            else
                            {
                                player.Set(player2);
                            }
                        }
                        _gameState = GameState.ChooseAttack;
                    }
                    break;
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
                        _damagedCardPlayer = GetDamagedPlayer();
                        if (_damagedCardPlayer)
                        {
                            _damagedCardPlayer.chosenCard.AnimateDamage();
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
                    if (_damagedCardPlayer == player1)
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
                    _restartButton.SetActive(true);
                    WinnerText.gameObject.SetActive(true);
                    break;
            }
        }

        private void ResetPlayers()
        {
            _damagedCardPlayer = null;

            player1.ResetPlayer();
            player2.ResetPlayer();

            player1.SetCardsClickable(true);
            player2.SetCardsClickable(true);
        }

        private CardPlayer GetDamagedPlayer()
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

        private CardPlayer GetWinner()
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