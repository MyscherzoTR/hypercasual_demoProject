using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager PlayerManagerInstance;

    private Transform _player;
    public Transform path;
    private Vector3 startMousePos, startPlayerPos;
    private bool moveThePlayer;

    [SerializeField] [Range(0f,1f)] private float playerSpeed;
    [SerializeField] [Range(0f, 50f)] private float pathSpeed;
    private float velocity;

    public ParticleSystem airEffect, ballTrail;

    void Start()
    {
        PlayerManagerInstance = this;
        _player = transform;
        airEffect.Stop();
        ballTrail.Stop();
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
}
