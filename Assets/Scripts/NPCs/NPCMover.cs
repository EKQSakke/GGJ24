using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    [System.Serializable]
    private class QueueMover
    {
        public NPC npc;
        public Transform mover;
        public int currentQueuePointIndex;
    }

    [SerializeField] private Transform[] path;
    [SerializeField] private Transform discardPoint;

    [Space]
    [SerializeField] private NPCSpawner spawner;
    [SerializeField] private AnimationCurve moveStepCurve;

    [Header("Read Only")]
    [SerializeField] private List<QueueMover> queueMovers;

    public int AmountOfQueuePoints { get { return path == null ? 0 : path.Length; } }

    void OnValidate()
    {
        NamePathPoints();
    }

    void OnEnable()
    {
        NPCSpawner.onSpawned += CreateQueueMovers;
        NPC.onNPCItemUsed += NPCItemUsed;
    }

    void OnDisable()
    {
        NPCSpawner.onSpawned -= CreateQueueMovers;
        NPC.onNPCItemUsed -= NPCItemUsed;
    }

    void CreateQueueMovers(List<NPC> npcs)
    {
        if (path.Length < npcs.Count)
        {
            Debug.LogError("Not enough path points for movers!");
            return;
        }

        queueMovers = new List<QueueMover>();

        for (int i = 0; i < npcs.Count; i++)
        {
            if (npcs[i] == null)
                continue;

            queueMovers.Add(new QueueMover
            {
                npc = npcs[i],
                mover = npcs[i].transform,
                currentQueuePointIndex = i
            });

            npcs[i].transform.position = path[i].position;
        }
    }

    void NPCItemUsed(NPC npc) => MoveQueue();

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

        CompleteQueueMovement();
    }

    void CompleteQueueMovement()
    {
        for (int i = 0; i < queueMovers.Count; i++)
        {
            queueMovers[i].currentQueuePointIndex++;

            if (queueMovers[i].currentQueuePointIndex == path.Length - 1)
                queueMovers[i].npc.AtDesk();
            else if (queueMovers[i].currentQueuePointIndex == path.Length)
                ResetQueuePosition(queueMovers[i]);
        }
    }

    void ResetQueuePosition(QueueMover queueMover)
    {
        queueMover.currentQueuePointIndex = 0;
        queueMover.mover.position = path[0].position;
        queueMover.npc.BackToQueue();
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
