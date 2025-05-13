using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class TestEnemy : MonoBehaviour
{
    public TestScript target;
    Vector3 Size;
    float speed;
    bool isAttacking;
    Animator enemyAnimtor;
    public GameObject ParentObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 1f;
        ParentObject = transform.parent.gameObject;

        enemyAnimtor = GetComponent<Animator>();

        Bounds totalBounds = new Bounds(transform.position, Vector3.zero);  // transform.position : bound �߽�, Vector3.zero : �ٿ�� �ʺ�

        // ���� ��ҵ��� ��������Ʈ ��� �����ͼ�
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
        {
            totalBounds.Encapsulate(sr.bounds); // �� ��������Ʈ�� ������ ����. �� �κ��� Scriptable Object ���� �ؾ� �� ��
        }

        Size = totalBounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + Size.x / 2 < target.transform.position.x - target.PlayerSize.x / 2)
        {
            Vector3 direction = (target.ParentObject.transform.position - ParentObject.transform.position).normalized;

            ParentObject.transform.Translate(direction * Time.deltaTime * speed);
            //transform.position += direction * Time.deltaTime * speed;
            enemyAnimtor.SetBool("1_Move", true);
        }
        else
        {
            enemyAnimtor.SetBool("1_Move", false);

            // �ִϸ��̼� ��ø���� �ذ�
            if (!isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }

    

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        enemyAnimtor.SetTrigger("2_Attack");

        // �ִϸ��̼� ���� ���� ��� (��: 1.2��)
        yield return new WaitForSeconds(0.25f);

        isAttacking = false;
    }
}
