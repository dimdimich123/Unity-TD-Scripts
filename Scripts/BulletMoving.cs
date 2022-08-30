using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _lastPosition;
    private GameObject _target;

    public void SetTarget(GameObject target) => _target = target;

    public void SetLastPosition(Vector3 position) => _lastPosition = position;

    private void Update()
    {
        if (_target != null)
        {
            Vector3 dir = _target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
            if(transform.position == _target.transform.position)
                Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _lastPosition, Time.deltaTime * _speed);
            if (transform.position == _lastPosition)
                Destroy(gameObject);
        }
        
    }
}
