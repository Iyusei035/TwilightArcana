using UnityEngine;

public class Arcana_HermitAction : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("9_Hermit/ForceField");
        _normalEffect = Instantiate(_prefab, _pos, Quaternion.identity);
        _normalEffect.gameObject.SetActive(true);
    }
}