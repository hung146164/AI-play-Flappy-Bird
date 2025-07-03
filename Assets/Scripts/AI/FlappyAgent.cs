using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class FlappyAgent : Agent
{
    private BirdController bird;
    private PipeManager pipeManager;
    private GameManager gameManager;
    private bool isDead;

    private void Awake()
    {
        bird = GetComponent<BirdController>();
        pipeManager = FindObjectOfType<PipeManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void OnEpisodeBegin()
    {
        bird.ResetBird();
        pipeManager.ResetPipes();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        (float,float) nextPipeInfo = pipeManager.GetNextPipe(transform.localPosition);
        float distanceX = nextPipeInfo.Item1;
        float center = nextPipeInfo.Item2;
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(bird.GetGravity());
        sensor.AddObservation(center);
        sensor.AddObservation(distanceX);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int fly = actions.DiscreteActions[0];
        if(fly!=0)
        {
            bird.Flap();
        }
        AddReward(0.01f);
        (float distanceX, float centerY) = pipeManager.GetNextPipe(transform.localPosition);
        float deltaY = transform.localPosition.y - centerY;

        AddReward(-Mathf.Abs(deltaY) * 0.01f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            AddReward(-1f);
            EndEpisode();
        }
        if (collision.CompareTag("TriggerPoint"))
        {
           // gameManager.AddScore(1);
            AddReward(1.5f);
        }
        if (collision.CompareTag("Ground"))
        {
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKeyDown(KeyCode.B) ? 1 : 0;
    }

}