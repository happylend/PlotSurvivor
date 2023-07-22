using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerState playerState;
    public Animator animator; // ����������
    private bool isBlocked; // �Ƿ��赲
    GameObject playerModel;

    public Vector3 moveDir;
    public Vector3 PlayerDir;

    // Start is called before the first frame update
    void Start()
    {
        if (playerState == null) { playerState = this.GetComponent<PlayerState>(); }
        if (animator == null) { animator = this.GetComponentInChildren<Animator>(); }
        if(playerModel == null) { playerModel=this.GetComponentInChildren<Animator>().gameObject; }

    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

    }

    void FixedUpdate()
    {
        Move();

    }

    void InputManagement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // ��ȡˮƽ������
        float vertical = Input.GetAxisRaw("Vertical"); // ��ȡ��ֱ������

        moveDir = new Vector3(horizontal, 0, vertical).normalized;
    }

    void Move()
    {

        if (moveDir.x != 0f || moveDir.z != 0f) // ֻ����������ƶ�����ת��ɫ
        {
            Vector3 movement = new Vector3(moveDir.x, 0f, moveDir.z);

            // �����ƶ�������ת��ɫ
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetRotation, 0.15f);

            // ����Ƿ�������ϰ���
            RaycastHit hit;
            if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
            {
                if (hit.collider.tag==("staticScene")) // �����ײ�岻�Ǵ���������ʾ���赲��
                {
                    isBlocked = true;
                    return; // �������ƶ�����ת
                }
            }
            else // û����ײ����ʾû�б��赲
            {
                isBlocked = false;
            }

            //�����ƶ�
            playerState.Move(movement);
            animator.SetBool("run", true);
        }

        else
        {
            animator.SetBool("run", false);
        }

    }
}
