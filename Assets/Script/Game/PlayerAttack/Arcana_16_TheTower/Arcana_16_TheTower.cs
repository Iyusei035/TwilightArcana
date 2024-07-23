using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_16_TheTower : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("16_TheTower/Laser AOE");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
    }
}
