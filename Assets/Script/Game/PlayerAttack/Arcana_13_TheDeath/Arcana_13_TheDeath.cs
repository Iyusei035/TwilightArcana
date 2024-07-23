using UnityEngine;

public class Arcana_13_TheDeath : ArcanaBase
{
    public override void ArcanaEffect()
    {
        _prefab = Resources.Load<GameObject>("13_TheDeath/TheDeath");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objs.Length; i++)
        {
            _normalEffect = Instantiate
                (_prefab, objs[i].gameObject.transform.position, Quaternion.identity);
            _normalEffect.gameObject.SetActive(true);
        }
    }
}
