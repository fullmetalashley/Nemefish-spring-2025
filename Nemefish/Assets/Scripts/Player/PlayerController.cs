using UnityEngine;
using Yarn.Compiler;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float speed;

    public float groundDist;

    public LayerMask terrainLayer;

    public Rigidbody rigidBody;

    public SpriteRenderer spriteRenderer;

    public GameObject leftSpawnPoint;
    public GameObject rightSpawnPoint;

    private Gun _gun;

    private PlayerRaycasting raycast;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _gun = this.gameObject.GetComponent<Gun>();
        raycast = this.gameObject.GetComponent<PlayerRaycasting>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        //Shoot a line down, that will only detect the terrain layer
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rigidBody.linearVelocity = moveDir * speed;
        
        
        
        
        if (x != 0 && x < 0)
        {
            //West
            FlipSprite(-1);
            raycast.ChangeDirection(3);
        }
        else if (x != 0 && x > 0)
        {
            //East
            FlipSprite(1);
            raycast.ChangeDirection(1);

        }

        if (y != 0 && y < 0)
        {
            //South
            raycast.ChangeDirection(2);
        }else if (y != 0 && y > 0)
        {
            //North
            raycast.ChangeDirection(0);
        }
    }

    public void FlipSprite(int dir)
    {
        switch (dir)
        {
            case -1:
                spriteRenderer.flipX = true;
                _gun.SetSpawnPoint(leftSpawnPoint.transform, -1);
                break;
            case 1:
                spriteRenderer.flipX = false;
                _gun.SetSpawnPoint(rightSpawnPoint.transform, 1);
                break;
            default:
                break;
        }
    }
}
