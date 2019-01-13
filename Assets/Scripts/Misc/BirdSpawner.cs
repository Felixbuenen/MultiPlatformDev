using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
  public GameObject birdPrefab;
  public Transform player;

  public float minTimeBetween;
  public float maxTimeBetween;

  public float birdSpeed;
  public float birdHeight;

  public float xRangeTreshold;

  private bool isSimulatingBird = false;
  private bool nextBirdDetermined = false;

  private float timer = 0f;
  private Vector3 birdVelocity;
  private GameObject bird;

  // Update is called once per frame
  void Update()
  {
    if (!isSimulatingBird)
    {
      if (!nextBirdDetermined)
      {
        timer = Random.Range(minTimeBetween, maxTimeBetween);
      }
      else
      {
        timer -= Time.deltaTime;
      }

      if (timer <= 0f) SpawnBird();
    }

    else
    {
      // update bird position
      bird.transform.position += birdVelocity * birdSpeed * Time.deltaTime;
      // if bird out of range, is simulating bird is false
      if (bird.transform.position.x > xRangeTreshold || bird.transform.position.x < -xRangeTreshold)
      {
        Destroy(bird);
        isSimulatingBird = false;
      }
    }

  }

  private void SpawnBird()
  {
    Vector3 position = RandomPosition();
    birdVelocity = RandomVelocity(position);

    bird = Instantiate(birdPrefab, position, Quaternion.identity);

    isSimulatingBird = true;
  }

  private Vector3 RandomPosition()
  {
    Vector3 position = new Vector3();
    position.y = birdHeight;
    position.z = player.position.z;

    bool left = Random.Range(0f, 1f) <= 0.5f;
    position.x = left ? -xRangeTreshold : xRangeTreshold;

    return position;
  }

  private Vector3 RandomVelocity(Vector3 position)
  {
    Vector3 velocity = new Vector3();
    velocity.x = position.x < 0 ? 1 : -1;
    velocity.z = Random.Range(0.2f, 0.2f);

    return velocity;
  }
}