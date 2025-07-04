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
        gameManager.ResetScore();
        gameManager.ChangeHighScore();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        (float,float) nextPipeInfo = pipeManager.GetNextPipe(transform.localPosition);
        float distanceX = nextPipeInfo.Item1;
        float center = nextPipeInfo.Item2;
        sensor.AddObservation(transform.localPosition.y/10);
        sensor.AddObservation(bird.GetGravity()/10);
        sensor.AddObservation(center/10);
        sensor.AddObservation(distanceX / 10);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int fly = actions.DiscreteActions[0];
        if(fly!=0)
        {
            bird.Flap();
        }
        // Trong OnActionReceived
        //(float distanceX, float centerY) = pipeManager.GetNextPipe(transform.localPosition);
        //float deltaY = transform.localPosition.y - centerY;

        //if (distanceX < 0.5f && distanceX > -0.5f) 
        //{
        //    AddReward(-Mathf.Abs(deltaY) * 0.05f); 
        //}
        AddReward(0.001f); 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            AddReward(-10f);
            EndEpisode();
        }
        if (collision.CompareTag("TriggerPoint")) 
        {
            AddReward(1.0f);
            gameManager.AddScore(1);
        }
        if (collision.CompareTag("Ground"))
        {
            AddReward(-15f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKeyDown(KeyCode.B) ? 1 : 0;
    }

}