using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    [System.Serializable]
    private class QueueMover
    {
        public Transform mover;
        public int currentQueuePointIndex;
    }

    [SerializeField] private Transform[] path;
    [SerializeField] private Transform discardPoint;

    [Space]
    [SerializeField] private List<Transform> movers = new List<Transform>();
    [SerializeField] private AnimationCurve moveStepCurve;

    [Header("Read Only")]
    [SerializeField] private List<QueueMover> queueMovers;

    void OnValidate()
    {
        NamePathPoints();
    }

    void Awake()
    {
        CreateQueueMovers();
    }

    void CreateQueueMovers()
    {
        if (path.Length < movers.Count)
        {
            Debug.LogError("Not enough path points for movers!");
            return;
        }

        queueMovers = new List<QueueMover>();

        for (int i = 0; i < movers.Count; i++)
        {
            if (movers[i] == null)
                continue;

            queueMovers.Add(new QueueMover
            {
                mover = movers[i],
                currentQueuePointIndex = i
            });

            movers[i].position = path[i].position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveQueue();
        }
    }

    public void MoveQueue()
    {
        StopAllCoroutines();
        StartCoroutine(MoveQueueRoutine());
    }

    IEnumerator MoveQueueRoutine()
    {
        float timer = 0f;
        float duration = moveStepCurve.keys[moveStepCurve.length - 1].time;
        float evaluatedValue = 0f;
        Vector3 startPosition, endPosition;

        while (timer <= duration)
        {
            evaluatedValue = moveStepCurve.Evaluate(timer / duration);

            for (int i = 0; i < queueMovers.Count; i++)
            {
                if (queueMovers[i] == null || queueMovers[i].currentQueuePointIndex >= path.Length)
                    continue;

                startPosition = path[queueMovers[i].currentQueuePointIndex].position;

                if (path.Length > queueMovers[i].currentQueuePointIndex + 1)
                    endPosition = path[queueMovers[i].currentQueuePointIndex + 1].position;
                else
                    endPosition = discardPoint.position;

                queueMovers[i].mover.position = Vector3.LerpUnclamped(startPosition, endPosition, evaluatedValue);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < queueMovers.Count; i++)
        {
            queueMovers[i].currentQueuePointIndex++;
        }
    }

    void OnDrawGizmos()
    {
        DrawPath();
    }

    void NamePathPoints()
    {
        if (path == null)
            return;

        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] == null)
                continue;

            path[i].gameObject.name = "Path Point: " + i;
        }
    }

    void DrawPath()
    {
        if (path == null)
            return;

        Gizmos.color = Color.blue;

        for (int i = 1; i < path.Length; i++)
        {
            if (path[i] == null)
                continue;

            Gizmos.DrawLine(path[i - 1].position, path[i].position);
        }
    }
}
