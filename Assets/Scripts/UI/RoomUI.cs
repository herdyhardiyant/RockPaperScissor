using OnlineFeatures;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RoomUI : MonoBehaviour
    {
        [SerializeField] GameObject playerItemPrefab;
        [SerializeField] GameObject playerListObject;
        [SerializeField] private Button startGameButton;

        public GameObject AddPlayerItemOnPlayerListInRoomUI()
        {
            var playerItem = Instantiate(playerItemPrefab, playerListObject.transform, worldPositionStays: false);
            return playerItem;
        }
        
        public void SetStartGameButtonActive(bool isActive)
        {
            startGameButton.interactable = isActive;
        }
        
    }
}