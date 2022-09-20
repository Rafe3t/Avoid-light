using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float sprint;
    public int stamina = 100;
    Rigidbody2D rbody;
    Animator anim;
    float sprintTime;
    bool hold;
    float sprint_speed;
    Vector2 sprintvector;
    float RL, UD;
    Vector2 movement_vector;
    float stamina_generator;
    float stamina_time_hold;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        if(!hold)
        {
            movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.SetBool("right", true);
                anim.SetBool("left", false);
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                RL = 1;
                UD = 0;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                anim.SetBool("right", false);
                anim.SetBool("left", true);
                anim.SetBool("up", false);
                anim.SetBool("down", false);
                RL = -1;
                UD = 0;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                anim.SetBool("right", false);
                anim.SetBool("left", false);
                anim.SetBool("up", true);
                anim.SetBool("down", false);
                UD = 1;
                RL = 0;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                anim.SetBool("right", false);
                anim.SetBool("left", false);
                anim.SetBool("up", false);
                anim.SetBool("down", true);
                UD = -1;
                RL = 0;
            }
        }
        else
        {
            movement_vector = Vector2.zero;
        }

        

        if (Input.GetAxisRaw("Jump") > 0 && !hold && stamina >= 10)
        {
            anim.SetBool("sprint", true);
            sprintTime = Time.time;
            hold = true;
            sprint_speed = sprint;
            stamina -= 10;
            stamina_time_hold = Time.time;
        }

        if (sprintTime + 0.3< Time.time)
        {
            anim.SetBool("sprint", false);
            hold = false;
            sprint_speed = 0;
        }

        if(stamina < 100 && stamina_generator + 0.4 < Time.time && stamina_time_hold + 2 < Time.time)
        {
            stamina += 5;
            stamina_generator = Time.time;
        }

        sprintvector = new Vector2(RL, UD);
        rbody.MovePosition(rbody.position + movement_vector +sprintvector*sprint_speed * Time.fixedDeltaTime);
    }
}
