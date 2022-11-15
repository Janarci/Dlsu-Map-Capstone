using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public enum food
    {
        cat_nip,
        cat_food,
        fish
    }

    public enum toy
    {
        yarn,
        laser,
        box
    }

    Dictionary<food, int> food_Favor;
    Dictionary<toy, int> toy_Favor;

    private int friendship_value = 0;
    protected int befriendAttempts = 0;

    private CatUI ui;
    // Start is called before the first frame update
    protected void Start()
    {
        InitializeCat();
        if(gameObject.TryGetComponent(out CatUI cat_ui))
        {
            ui = cat_ui;
            ui.SetFriendshipBarValue(getFriendshipPercentage());
            ui.cat = this;
        }

        else
        {
            ui = gameObject.AddComponent<CatUI>();
            ui.cat = this;
            Debug.Log("cat ui added");
        }

        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void InitializeCat()
    {
        InitializeFoodFavors(10, 10, 10);
        InitializeFoodFavors(10, 10, 10);

    }

    protected void InitializeFoodFavors(int cat_nip_favor, int cat_food_favor, int fish_favor)
    {
        food_Favor = new Dictionary<food, int>();
        food_Favor.Add(food.cat_nip, cat_nip_favor);
        food_Favor.Add(food.cat_food, cat_food_favor);
        food_Favor.Add(food.fish, fish_favor);

    }

    protected void InitializeToyFavors(int yarn_favor, int laser_favor, int box_favor)
    {
        
        toy_Favor = new Dictionary<toy, int>();
        toy_Favor.Add(toy.yarn, yarn_favor);
        toy_Favor.Add(toy.laser, laser_favor);
        toy_Favor.Add(toy.box, box_favor);

    }


    protected void AttemptBefriendCat(food given_food)
    {
        if (befriendAttempts >= 5)
        {
            Debug.Log("The cat prefers to be left alone");
            EventManager.Instance.CatBefriendFail(this);
            return;
        }

        if (food_Favor[given_food] >= 0)
        {
            friendship_value = Mathf.Min(friendship_value + food_Favor[given_food], 100);
        }

        else
        {
            friendship_value = Mathf.Max(friendship_value + food_Favor[given_food], 0);
        }

        Debug.Log("Food Favor Received: " + food_Favor[given_food]);

        befriendAttempts++;

        if (getFriendshipPercentage() >= 1.0f)
        {
            EventManager.Instance.CatBefriendSuccess(this);
            Debug.Log("The cat has chosen to be your friend");
        }
    }

    public void FeedCatNip()
    {
        AttemptBefriendCat(food.cat_nip);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void FeedCatFood()
    {
        AttemptBefriendCat(food.cat_food);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void FeedFish()
    {
        AttemptBefriendCat(food.fish);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    protected void AttemptBefriendCat(toy given_toy)
    {
        if(befriendAttempts >= 5)
        {
            Debug.Log("The cat prefers to be left alone");
            EventManager.Instance.CatBefriendFail(this);
            return;
        }

        if (toy_Favor[given_toy] >= 0)
        {
            friendship_value = Mathf.Min(friendship_value + toy_Favor[given_toy], 100);
        }

        else
        {
            friendship_value = Mathf.Max(friendship_value + toy_Favor[given_toy], 0);
        }

        Debug.Log("Toy Favor Received: " + toy_Favor[given_toy]);

        befriendAttempts++;

        if(getFriendshipPercentage() >= 1.0f)
        {
            EventManager.Instance.CatBefriendSuccess(this);
            Debug.Log("The cat has chosen to be your friend");
        }
    }

    public void PlayWithYarn()
    {
        AttemptBefriendCat(toy.yarn);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void PlayWithLaser()
    {
        AttemptBefriendCat(toy.laser);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public void PlayWithBox()
    {
        AttemptBefriendCat(toy.box);
        ui?.SetFriendshipBarValue(getFriendshipPercentage());
    }

    public int getFriendshipValue()
    {
        return friendship_value;
    }

    public float getFriendshipPercentage()
    {
        return friendship_value/60.0f;
    }
}
