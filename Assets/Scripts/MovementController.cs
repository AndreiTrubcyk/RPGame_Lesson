using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;
    private bool _isEffect;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Перемещаем персонажа в направлении _destination.
        _navMeshAgent.SetDestination(_destination);

        // TODO: Получите точку, по которой кликнули мышью и задайте ее вектор в поле _destination.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePosition, out RaycastHit hit))
            {
                transform.LookAt(hit.point);
                _destination = hit.point;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            var q = GetComponent<Outline>();
            q.OutlineWidth = 2;
            _isEffect = true;
        }

        if (other.CompareTag("Bridge") && _isEffect == false)
        {
            var w = other.gameObject.GetComponent<Bridge>();
            w.Break();
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            _fire.SetActive(true);
        }
    }
}