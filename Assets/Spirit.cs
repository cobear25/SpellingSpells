using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpiritType
{
    fire = 0,
    water = 1,
    grass = 2
}
public class Spirit : MonoBehaviour
{
    public Color watercolor;
    public Color firecolor;
    public Color grasscolor;
    public Sprite waterSprite;
    public Sprite fireSprite;
    public Sprite grassSprite;
    public GameObject spriteObject;
    public ParticleSystem particles;

    public bool isDestroyed = false;
    public SpiritType spiritType;
    public GameController gameController;

    float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spiritType = (SpiritType)Random.Range(0, 3); 
        switch (spiritType)
        {
            case SpiritType.water:
                spriteObject.GetComponent<SpriteRenderer>().sprite = waterSprite;
                break;
            case SpiritType.fire:
                spriteObject.GetComponent<SpriteRenderer>().sprite = fireSprite;
                break;
            case SpiritType.grass:
                spriteObject.GetComponent<SpriteRenderer>().sprite = grassSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-11, transform.position.y), moveSpeed * Time.deltaTime);
        if (transform.position.x <= -10.0f)
        {
            gameController.SpiritReachedEnd();
        }
    }

    public void Kill()
    {
        isDestroyed = true;
        Destroy(spriteObject);
        var main = particles.main;
        switch (spiritType)
        {
            case SpiritType.water:
                main.startColor = watercolor;
                break;
            case SpiritType.fire:
                main.startColor = firecolor;
                break;
            case SpiritType.grass:
                main.startColor = grasscolor;
                break;
        }
        particles.Play();
        Destroy(gameObject, 2f);
    }
}
