using Effekseer;
using UnityEngine;

public class EffekseerTest : MonoBehaviour
{

    bool nowBtnPresd = false;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            nowBtnPresd = true;
        }
    }

    void FixedUpdate()
    {
        // �G�t�F�N�g���擾����B
        EffekseerEffectAsset effect = Resources.Load<EffekseerEffectAsset>("FireBall02");

        if (nowBtnPresd)
        {
            // transform�̈ʒu�ŃG�t�F�N�g���Đ�����
            EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
            // transform�̉�]��ݒ肷��B
            handle.SetRotation(transform.rotation);

            nowBtnPresd = false;

        }
    }
}
