using System;
using UnityEngine;
using UnityEngine.UI;

namespace MultiplayerFeatures
{
    public class AvatarSelection : MonoBehaviour
    {
        public static readonly string SelectedAvatarIndexPropertyName = "selectedAvatarIndex";
        
        [SerializeField] Image avatarImage;
        [SerializeField] Button changeAvatarButton;

        public int SelectedAvatarIndex => _avatarIndex;
        
        private int _avatarIndex = 0;

        private void Awake()
        {
            changeAvatarButton.onClick.AddListener(ChangeAvatar);
            SetAvatarToCurrentAvatarIndex();
        }

        private void ChangeAvatar()
        {
            ChangeAvatarIndexToTheNextAvatarIndex();

            SetAvatarToCurrentAvatarIndex();
        }

        private void ChangeAvatarIndexToTheNextAvatarIndex()
        {
            _avatarIndex++;
            if (_avatarIndex == AvatarSpritesCollection.SpriteCount())
            {
                _avatarIndex = 0;
            }
        }

        private void SetAvatarToCurrentAvatarIndex()
        {
            var sprite = AvatarSpritesCollection.GetAvatar(_avatarIndex);
            
            print(sprite);
            
            avatarImage.sprite = sprite;
            
            avatarImage.SetNativeSize();
        }

        
    }
}
