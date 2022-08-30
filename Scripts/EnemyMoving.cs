using UnityEngine;
using System.Collections;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currentSpeed;
    private Transform[] _pathPoints;
    private int[] _LookDirections;
    private Transform _body;
    private int _pointInd = 1;
    private float _distance = 0.05f;
    private Coroutine _coroutine;

    public float Speed => _speed;

    public void ChangeSpeedOnPercent(float value) 
    {
        _currentSpeed -= _currentSpeed * value / 100;
    }

    public void UpdateSpeed() 
    {
        _currentSpeed = _speed;
    }

    private void Start()
    {
        _currentSpeed = _speed;
        
        _pathPoints = transform.parent.GetComponent<MovementPath>().GetPathPoints();
        if (_pathPoints == null)
        {
            Destroy(gameObject);
        }

        _LookDirections = transform.parent.GetComponent<MovementPath>().GetLookDirection();

        _body = transform.GetChild(0);

        _coroutine = StartCoroutine(Move());
        
    }
    
    private IEnumerator Move()
    {
        while (true)
        {
            if (_pointInd == _pathPoints.Length)
            {
                LevelManager.Current.DecreaseHearts();
                transform.parent.GetComponentInChildren<SpawnEnemies>()?.IncreaseCountDestroyedEnemies();
                Destroy(gameObject);
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, _pathPoints[_pointInd].position, Time.deltaTime * _currentSpeed);

            if ((transform.position - _pathPoints[_pointInd].position).sqrMagnitude < _distance * _distance)
            {
                if (_LookDirections[_pointInd] != _LookDirections[_pointInd - 1])
                    _body.rotation = Quaternion.Euler(0, _LookDirections[_pointInd], 0);

                _pointInd += 1;
            }
            yield return null;
        }
    }

    public void DieAction() 
    {
        StopCoroutine(_coroutine);
        _body.GetComponent<Animator>().Play("die");
        GetComponentInChildren<Dissolve>().StartDissolve();
    }
}
