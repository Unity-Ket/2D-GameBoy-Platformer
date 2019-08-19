using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            enemyBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            enemyBody.velocity = new Vector2(-moveSpeed, 0f);
        }

        //enemyPatrol();
    }

    //void enemyPatrol()
    //{

    //}

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)), 1f);
    }



}//enemyMovement
