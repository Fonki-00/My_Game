using UnityEngine;

public class Kill : MonoBehaviour
{
    public int score = 0;

    private void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
            score++;
            Debug.Log(score);

        }
    }
}
