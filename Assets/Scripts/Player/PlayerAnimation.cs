using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Dependencies")]
    public Animator animator;

    private int comboCounter = 0;
    private Coroutine comboWindowCoroutine;

    private bool isAttackDelayOver = true;

    private const float ComboWindowTime = 0.7f;
    private const float AttackDelayTime = 0.5f;

    public void Start()
    {
        animator.Play("StartGame");
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movementInput = value.ReadValue<Vector2>();

        animator.SetBool("isRunningUp", movementInput.y > 0);
        animator.SetBool("isRunning", movementInput.y <= 0 && movementInput.magnitude > 0f);
    }

    public void OnInteraction(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0f && isAttackDelayOver)
        {
            StartCoroutine(AttackDelay());

            if (comboWindowCoroutine != null)
            {
                StopCoroutine(comboWindowCoroutine);
            }

            comboWindowCoroutine = StartCoroutine(ComboWindow());
            comboCounter++;

            switch (comboCounter)
            {
                case 1:
                    animator.SetTrigger("Attack");
                    break;
                case 2:
                    animator.SetTrigger("SecondAttack");
                    break;
                case 3:
                    animator.SetTrigger("ThirdAttack");
                    comboCounter = 0;
                    break;
            }
        }
    }

    IEnumerator ComboWindow()
    {
        yield return new WaitForSeconds(ComboWindowTime);
        comboCounter = 0;
    }

    IEnumerator AttackDelay()
    {
        isAttackDelayOver = false;
        yield return new WaitForSeconds(AttackDelayTime);
        isAttackDelayOver = true;
    }
}