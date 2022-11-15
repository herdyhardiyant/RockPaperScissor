using Photon.Pun;
using UnityEngine;

namespace OnlineConnection
{
    public class GameplayMultiplayerConnection : MonoBehaviour
    {
        public int GetPing()
        {
          return PhotonNetwork.GetPing();
        }
    }
}
