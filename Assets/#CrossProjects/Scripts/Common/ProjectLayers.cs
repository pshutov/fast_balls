using UnityEngine;

namespace _CrossProjects.Common
{
    public static class ProjectLayers
    {
        public static readonly int Default = LayerMask.NameToLayer("Default");
        public static readonly int TransparentFX = LayerMask.NameToLayer("TransparentFX");
        public static readonly int IgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        
        public static readonly int Water = LayerMask.NameToLayer("Water");
        public static readonly int UI = LayerMask.NameToLayer("UI");
        
        
        public static readonly int UIEffect = LayerMask.NameToLayer("UI Effect");
        
        public static readonly int Environment = LayerMask.NameToLayer("Environment");
        public static readonly int Wall = LayerMask.NameToLayer("Wall");
        
        public static readonly int Character = LayerMask.NameToLayer("Character");
        public static readonly int CharacterWeapon = LayerMask.NameToLayer("Character Weapon");
        
        public static readonly int Enemy = LayerMask.NameToLayer("Enemy");
        public static readonly int EnemyWeapon = LayerMask.NameToLayer("Enemy Weapon");
        
        public static readonly int Interactive = LayerMask.NameToLayer("Interactive");
        public static readonly int Obstacle = LayerMask.NameToLayer("Obstacle");
    }
}