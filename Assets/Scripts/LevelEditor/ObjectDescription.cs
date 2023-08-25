using Assets.Scripts.Entities.Navigation.Pulling;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Cream/Database/Descriptions/New object description")]
public class ObjectDescription : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
    [field: SerializeField] public CharacteristicModel[] Characteristics { get; private set; }

    [Serializable]
    public struct CharacteristicModel
    {
        public IconType IconType;
        public Sprite CharacteristicIcon;
        public float Value;
    }

    public enum IconType
    {
        DAMAGE, HEALTH, SPEED, ATTACK_SPEED, OTHER
    }
}
