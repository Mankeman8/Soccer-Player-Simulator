using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [Header("Basic Values")]
    [Tooltip("What is your name?")]
    public string name;
    [Range(15, 40), Tooltip("Your age in game")]
    public int age;
    [Tooltip("What nation are you from?")]
    public string nationality;
    [Tooltip("How much money you have")]
    public float money;
    [Range(1, 1000000000), Tooltip("How much do you get paid just for playing?")]
    public float wages;
    [Range(0, 1000000000), Tooltip("How much do you get paid from sponsors?")]
    public float sponsorWages;

    //WIP
    public string position;
    public string horoscope;
    public float height;
    public float weight;
    public bool bigDick = false;
    //WIP

    [Tooltip("Are you in a relationship? Affects style and fame depending on" +
        "the relationship and type of person they are.")]
    public bool relationship = false;
    [Tooltip("If the person wants to enter a relationship. Affects the formula " +
        "for likability and fame.")]
    public bool canDate = false;
    [Tooltip("Add a personality quiz at the beginning to determine innate traits " +
        "and type of answer's a person will give")]
    public string personality;
    [Header("In-Game Values")]
    [Range(1,100), Tooltip("How likely it is for you to be synced up with your team " +
        "and the likelihood of them passing the ball to you. Also affects if you get " +
        "to play in games or not due to chemistry with manager.")]
    public int chemistryWithTeam;
    [Range(1,100), Tooltip("How likely the manager will want to play you, along with " +
        "making impressions and having them wanna give you a raise")]
    public int chemistryWithManager;
    [Range(1, 100), Tooltip("How liked you are in game. Determines teammate's giving you " +
        "a chance to score, manager's wanting to give you a raise, sponsors, etc")]
    public int likability;
    [Range(1, 100), Tooltip("Your style in game. Determines which choices you can choose " +
        "during dialogue and indirectly affects likability")]
    public int style;
    [Tooltip("Your fame. Determines sponsors and being called up to the national team " +
        "along with an indirect affect to likability")]
    public int fame;
    [Header("Shooting")]
    [Tooltip("The Shooting Category. Adds shots, header and volley, then divides by 3.")]
    public int Shooting;
    [Tooltip("How accurate are your shots?")]
    public int shots;
    [Tooltip("How accurate are your headers?")]
    public int header;
    [Tooltip("How accurate are your volleys?")]
    public int volley;
    [Header("Skill")]
    [Tooltip("The Skill Category. Adds dribbling, curve and ball control, then divides by 3.")]
    public int Skill;
    [Tooltip("How well can you dribble?")]
    public int dribbling;
    [Tooltip("How well can you curve the ball?")]
    public int curve;
    [Tooltip("How well is your ball control?")]
    public int ballControl;
    [Header("Movement")]
    [Tooltip("The Movement Category. Adds speed, stamina and reaction, then divides by 3.")]
    public int Movement;
    [Tooltip("How fast are you?")]
    public int speed;
    [Tooltip("How much stamina do you have?")]
    public int stamina;
    [Tooltip("How quick can you react to things?")]
    public int reaction;
    [Header("Strength")]
    [Tooltip("The Strength Category. Adds shot power, body and jump power, then divides by 3.")]
    public int Strength;
    [Tooltip("How powerful is your kicks? (Influences header too)")]
    public int shotPower;
    [Tooltip("How well can you endure injury?")]
    public int body;
    [Tooltip("How high can you jump?")]
    public int jumpPower;
    [Header("Mentality")]
    [Tooltip("The Mentality Category. Adds positioning, vision and game IQ, then divides by 3.")]
    public int Mentality;
    [Tooltip("How well can you position?")]
    public int positioning;
    [Tooltip("Can you see into the future?")]
    public int vision;
    [Tooltip("What's your game IQ?")]
    public int gameIQ;
}