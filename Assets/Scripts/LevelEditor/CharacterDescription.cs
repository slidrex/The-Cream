using Assets.Scripts.Entities.Navigation.Pulling;
using Assets.Scripts.Entities.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Cream/Database/Descriptions/New character description")]
internal class CharacterDescription : ObjectDescription
{
    [HideInInspector] public PlayerSkill[] Skills;
    [field: SerializeField] public Sprite CharacterIcon { get; private set; }
    public void Init(PlayerSkill[] skills)
    {
        Skills = skills;
    }
}
