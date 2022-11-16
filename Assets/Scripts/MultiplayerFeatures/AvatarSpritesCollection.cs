using UnityEngine;

namespace MultiplayerFeatures
{
    public static class AvatarSpritesCollection
    {
        private static string[] _avatarPaths = new[]
        {
            "0x72_DungeonTilesetII_v1.4/frames/knight_f_idle_anim_f0",
            "0x72_DungeonTilesetII_v1.4/frames/skelet_idle_anim_f0",
            "0x72_DungeonTilesetII_v1.4/frames/wizzard_f_hit_anim_f0",
            "0x72_DungeonTilesetII_v1.4/frames/orc_warrior_idle_anim_f0",
            "0x72_DungeonTilesetII_v1.4/frames/necromancer_idle_anim_f1",
        };

        public static Sprite GetAvatar(int index)
        {
            return Resources.Load<Sprite>(_avatarPaths[index]);
        }
        

        // Get array length
        public static int SpriteCount()
        {
            return _avatarPaths.Length;
        }
    }
}