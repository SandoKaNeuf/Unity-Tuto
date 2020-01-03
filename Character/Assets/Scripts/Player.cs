using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    //variable for jumpforce
    [SerializeField]
    private float _jumpForce = 5.0f;
    //variable grounded = false
    private bool _grounded = false;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool resetJumpNeeded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal input for left/right
        float move = Input.GetAxisRaw("Horizontal");

        //if space key && grounded == true
        //current velocity = new vlocity (currnt x, jumpforce)
        //grounded = false
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            //jump!
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _grounded = false;
            resetJumpNeeded = true;
            //breath
            StartCoroutine(ResetJumpNeededCoroutine());
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .8f, _groundLayer.value); // 1 << 8
        Debug.DrawRay(transform.position, Vector2.down * .8f, Color.green);

        //2D raycast to the ground
        //if hitinfo != null
        //grounded = true
        if (hitInfo.collider != null)
        {
            Debug.Log("Hit: " + hitInfo.collider.name);

            if (resetJumpNeeded == false)
            {
                _grounded = true;
            }
            
        }

        // current velocity = new velocity (horizontal input, current velocity.y);
        _rigid.velocity = new Vector2(move, _rigid.velocity.y);
    }

    IEnumerator ResetJumpNeededCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
