using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katasuironn : MonoBehaviour
{
    [SerializeField] GameObject _item;

    void Start()
    {
        // �^�L���X�g���Z�q�A���s����ƃG���[�ɂȂ�
        //var rt = (RectTransform)_item.transform;

        // is ���Z�q�B�w��̌^���ǂ����𒲂ׂ�B
        //if (_item.transform is RectTransform)
        //{
        //    // ���̃L���X�g�Ȃ���S�����ǁA�^�`�F�b�N��2�d�Ȃ̂�����
        //    var rt = (RectTransform)_item.transform;
        //}

        // as ���Z�q�B�ϊ��ł��Ȃ���� null �ɂȂ�B
        // �^�`�F�b�N�̍ł�����ȕ��@�B�̂���g����B
        //var rt = _item.transform as RectTransform;
        //if (rt != null) { }

        // �p�^�[���}�b�`���O(is �p�^�[����)
        // �^�`�F�b�N�ƌ^�ϊ��𓯎��ɍs����B���_���ȏ������B
        if (_item.transform is RectTransform rt) { }
    }
}
