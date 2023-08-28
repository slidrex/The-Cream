using UnityEngine;

public class CharacteristicIcons : MonoBehaviour
{
    public static CharacteristicIcons Instance;
    [field: SerializeField] public Sprite _damageIcon { get; private set; }
    [field: SerializeField] public Sprite _healthIcon { get; private set; }
    [field: SerializeField] public Sprite _speedIcon { get; private set; }
    [field: SerializeField] public Sprite _attackSpeedIcon { get; private set; }

    private void Start()
    {
        Instance = this;
    }
}
