using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana_17_TheStar : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("17_The Star/TheStar");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
    }
}