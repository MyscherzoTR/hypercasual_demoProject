using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager PlayerManagerInstance;
    GameObject player;

    public Transform _player;
    public Transform path;
    private Vector3 startMousePos, startPlayerPos, playerScaleChange;
    private bool moveThePlayer;

    [SerializeField] [Range(0f,1f)] private float playerSpeed;
    [SerializeField] [Range(0f, 50f)] private float pathSpeed;
    private float velocity, playerHeight, height, growthRate = 0.25f, shrinkRate;

    public ParticleSystem airEffect, ballTrail, CollideParticle;

    void Start()
    {
        PlayerManagerInstance = this;
        _player = transform;
        player = GameObject.FindGameObjectWithTag("Player");
        Obstacle.obsShrinkRate += ObstacleShrinkRate;
        airEffect.Stop();
        ballTrail.Stop();
    }

    public void ResetPlayer()
    {
        _player.localScale = Vector3.one;
        _player.position = new Vector3(0, 20.5f, -38.5f);
    }

    void ObstacleShrinkRate(float shrinkRate)
    {
        this.shrinkRate = shrinkRate;
    }

    void Update()
    {
        if (MenuManager.MenuManagerInstance.GameState)
        {

            if (Input.GetMouseButtonDown(0))
            {
                moveThePlayer = true;
                Plane newPlan = new Plane(Vector3.up, 0f);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (newPlan.Raycast(ray, out var distance))
                {
                    startMousePos = ray.GetPoint(distance);
                    startPlayerPos = _player.position;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveThePlayer = false;
            }

            if (moveThePlayer)
            {
                Plane newPlan = new Plane(Vector3.up, 0f);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (newPlan.Raycast(ray, out var distance))
                {
                    Vector3 mouseNewPos = ray.GetPoint(distance);
                    Vector3 MouseNewPos = mouseNewPos - startMousePos;
                    Vector3 desiredPlayerPos = MouseNewPos + startMousePos;

                    desiredPlayerPos.x = Mathf.Clamp(desiredPlayerPos.x, -2.24f, 2.24f);

                    _player.position = new Vector3(Mathf.SmoothDamp(_player.position.x, desiredPlayerPos.x, ref velocity, playerSpeed),
                        _player.position.y, _player.position.z);
                }
            }

            var pathNewPos = path.position;

            path.position = new Vector3(pathNewPos.x, pathNewPos.y, Mathf.MoveTowards(pathNewPos.z, -1000f, pathSpeed * Time.deltaTime));
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        playerHeight = player.transform.localScale.y;

        var NewParticle = Instantiate(CollideParticle, transform.position, Quaternion.identity);
        //var particleColor = NewParticle.GetComponent<Renderer>().material;

        if (other.CompareTag("collectable"))
        {
            NewParticle.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;

            player.transform.position = new Vector3(transform.position.x, transform.position.y + (playerHeight * growthRate) / 2, transform.position.z);
            height = (playerHeight * growthRate) + playerHeight;
            playerScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = playerScaleChange;


            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("obstacle"))
        {
            if ( shrinkRate == 0.25f)
            {
                NewParticle.GetComponent<Renderer>().material.color = new Color(255, 10, 0);

            }
            else
            {
                NewParticle.GetComponent<Renderer>().material.color = new Color(220, 0, 0);
            }

            player.transform.position = new Vector3(transform.position.x, transform.position.y - (playerHeight * shrinkRate) / 2, transform.position.z);
            height = playerHeight - (playerHeight * shrinkRate);
            playerScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = playerScaleChange;

            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("finishLine"))
        {
          GameManager.GameManagerInstance.LevelFinish();
        }

        if (height < 0.10f)
        {
            GameManager.GameManagerInstance.LevelFail();
        }
    }
}
