using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFootsteps : MonoBehaviour
{

    Sprite leftFootSprite = null;
    Sprite rightFootSprite = null;
    private float currentAddInterval;
    public float timeAddInterval;
    private float currentRemoveInterval;
    public float timeRemoveInterval;
    Queue<Foot> feetQueue;
    SpriteRenderer spriteRenderer;
    public GameObject rightFoot;
    public GameObject leftFoot;


    // Use this for initialization
    void Start()
    {
        leftFootSprite = Resources.Load<Sprite>("Assets/Graphics/PrimitiveTestGraphics/left_foot.png");
        rightFootSprite = Resources.Load<Sprite>("Assets/Graphics/PrimitiveTestGraphics/right_foot.png");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        feetQueue = new Queue<Foot>();
        currentAddInterval = 0;
        currentRemoveInterval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAddInterval >= timeAddInterval)
        {
            feetQueue.Enqueue(new Foot(gameObject.transform.position.x, gameObject.transform.position.y, feetQueue.Count % 2 == 0, gameObject.transform.rotation.x, gameObject.transform.rotation.y));
            currentAddInterval = 0;
            GameObject temp = Instantiate(leftFoot);
            temp.transform.position = new Vector2(2,3);
            temp.GetComponent<Rigidbody2D>().rotation = 90;
            Destroy(temp.gameObject);
        }

        foreach (var foot in feetQueue)
        {
            //

        }

        currentAddInterval += Time.deltaTime;
    }

    class Foot
    {
        private double x;
        private double y;
        private bool isRight;
        private double rotationX;
        private double rotationY;

        public Foot(double x, double y, bool isRight, double rotationX, double rotationY)
        {
            X = x;
            Y = y;
            IsRight = isRight;
            RotationX = rotationX;
            RotationY = rotationY;
        }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public bool IsRight
        {
            get
            {
                return isRight;
            }

            set
            {
                isRight = value;
            }
        }

        public double RotationX
        {
            get
            {
                return rotationX;
            }

            set
            {
                rotationX = value;
            }
        }

        public double RotationY
        {
            get
            {
                return rotationY;
            }

            set
            {
                rotationY = value;
            }
        }
    }
}
