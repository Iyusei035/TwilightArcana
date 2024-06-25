using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_WheelOfFortuneAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("WheelOfFortune/Rudder");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
        Destroy(_normalEffect.gameObject, 10.0f);
    }
}
