using Assets.Scripts.Entities.Navigation.Pulling;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Cream/Database/Descriptions/New object description")]
public class ObjectDescription : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string DescriptionKey { get; private set; }
    [field: SerializeField] public CharacteristicModel[] Characteristics { get; private set; }

    [Serializable]
    public struct CharacteristicModel
    {
        public IconType IconType;
        public Sprite CharacteristicIcon;
        public string Value;
    }

    public enum IconType
    {
        DAMAGE, HEALTH, SPEED, ATTACK_SPEED, OTHER
    }
}
