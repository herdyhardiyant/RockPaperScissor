using System;
using System.Collections.Generic;
using Gameplay;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace OnlineConnection
{
    public class NetPlayer : MonoBehaviourPun
    {
        public static List<NetPlayer> players = new List<NetPlayer>(2);

        private CardPlayer _cardPlayer;

        private Card[] cards;

        public void Set(CardPlayer player)
        {
            _cardPlayer = player;
            cards = player.GetComponentsInChildren<Card>();
            foreach (var card in cards)
            {
                print("card: " + card);
                var button = card.GetComponent<Button>();
                button.onClick.AddListener(() => RemoteClick(card.AttackValue));
            }
        }

        private void RemoteClick(Attack attack)
        {
            if (photonView.IsMine)
            {

                photonView.RPC("RemoteClickButtonRPC", RpcTarget.Others, (int)attack);
            }
        }

        [PunRPC]
        private void RemoteClickButtonRPC(int value)
        {
            foreach (var card in cards)
            {
                if(card.AttackValue == (Attack)value)
                {
                    print(card.name);
                    print("Click");
                    card.OnClick();
                    // var button = card.GetComponent<Button>();
                    // button.onClick.Invoke();
                    break;
                }
            }
        }

        private void OnEnable()
        {
            players.Add(this);
        }

        private void OnDisable()
        {
            players.Remove(this);
        }

        private void OnDestroy()
        {
            foreach (var card in cards)
            {
                var button = card.GetComponent<Button>();
                button.onClick.RemoveListener(() => RemoteClick(card.AttackValue));
            }
        }
    }
}