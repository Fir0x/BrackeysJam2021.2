using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Pathfinder _pathfinder;
    private Vector2 _targetPosition;

    [SerializeField] private float _speed = 2f;

    [Header("Damages")]
    [SerializeField] private int _startDamage = 1;
    [SerializeField] private float _damageZoneRadius;
    [SerializeField] private float _damageScaling = 0.5f;

    [Header("Pathfinding")]
    [SerializeField] private float _minSearchDistance = 1.5f;
    [SerializeField] private float _maxSearchDistance = 3;
    [SerializeField] private LayerMask _roomMask;
    [SerializeField] private LayerMask _collisionMask;

    private float _lightingIntensity;
    private Light2D _lighting;

    private void Awake()
    {
        _pathfinder = GetComponent<Pathfinder>();
        _lighting = GetComponent<Light2D>();
        _lightingIntensity = _lighting.intensity;
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    // Used to wait one frame after Start
    // For obscure reasons it's broken without this
    private IEnumerator Init()
    {
        yield return null;
        FindTarget();
    }

    public GameObject circle;
    public void FindTarget()
    {
        int limiter = 1000;
        Vector2 targetPosition;
        bool found;
        do
        {
            do
            {
                float theta = 2f * Mathf.PI * Random.Range(0f, 1f);
                float u = Random.Range(0f, _maxSearchDistance) + Random.Range(0f, _maxSearchDistance);
                float r = u > _maxSearchDistance ? 2 * _maxSearchDistance - u : u;
                r = r < _minSearchDistance ? _minSearchDistance + r * ((_maxSearchDistance - _minSearchDistance) / _minSearchDistance) : r;

                float xDistance = r * Mathf.Cos(theta);
                float yDistance = r * Mathf.Sin(theta);

                targetPosition.x = transform.position.x + xDistance;
                targetPosition.y = transform.position.y + yDistance;

                found = Physics2D.OverlapPoint(targetPosition, _roomMask) != null;
                found &= Physics2D.OverlapBox(targetPosition, _pathfinder.EntitySize, 0, _collisionMask) == null;

                limiter--;
            } while (!found && limiter > 0);

            found &= _pathfinder.FindPath(targetPosition);
            limiter--;
        } while (!found && limiter > 0);

        Optional<Vector2> optTarget = _pathfinder.GetNextNode();
        if (optTarget.IsPresent())
            _targetPosition = optTarget.Get();
    }

    public void SwitchOn()
    {
        _lighting.intensity = _lightingIntensity;
    }

    public void SwitchOff()
    {
        _lighting.intensity = 0;
    }

    private void Update()
    {
        if (_pathfinder.IsPositionNearNextNode(transform.position))
        {
            Optional<Vector2> optTarget = _pathfinder.GetNextNode();
            if (optTarget.IsPresent())
                _targetPosition = optTarget.Get();
            else
                FindTarget();
        }
        else
        {
            transform.Translate((_targetPosition - (Vector2)transform.position).normalized * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_lighting.intensity == 0)
            return;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, collision.ClosestPoint(transform.position));
            int damage = (int)(((_damageZoneRadius - distance) * _damageScaling + 1) * _startDamage);
            player.Damage(damage);
        }
    }

#if UNITY_EDITOR
    [SerializeField] private bool _debug;

    private void OnDrawGizmos()
    {
        if (_debug)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _minSearchDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _maxSearchDistance);
        }
    }
#endif
}
