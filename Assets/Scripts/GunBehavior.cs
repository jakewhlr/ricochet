﻿using System.Collections;
using UnityEngine;

public class GunBehavior : MonoBehaviour {
    private HingeJoint2D sniper_hinge;
    private JointMotor2D sniper_motor;
    private Transform bullet_position;
    private Transform casing_position;
    private GameObject spawned_bullet;
    private GameObject spawned_casing;
    private AudioSource shot_sound;
    private ClipBehavior clip_script;
    private Animator rifle_animator;

    public GameObject clip;
    public GameObject bullet_prefab;
    public GameObject bullet_spawn;
    public GameObject casing_prefab;
    public GameObject casing_spawn;
    public float x_vel_multiplier;
    public float y_vel_multiplier;

    [Range(0f, 1f)]
    public float shot_volume;

    void Start () {
        bullet_position = bullet_spawn.GetComponent<Transform>();
        casing_position = casing_spawn.GetComponent<Transform>();
        shot_sound = GetComponent<AudioSource>();
        shot_sound.volume = shot_volume;
        clip_script = clip.GetComponent<ClipBehavior>();
        rifle_animator = GetComponent<Animator>();
        InitializeMotor();
    }

    void Update () {
        if( Input.GetKey(KeyCode.DownArrow) ) {
            if( Input.GetKey(KeyCode.LeftControl) ) {
                sniper_motor.motorSpeed = 10;
            } else {
                sniper_motor.motorSpeed = 100;
            }
            sniper_hinge.motor = sniper_motor;
        } else if( Input.GetKey(KeyCode.UpArrow) ) {
            if( Input.GetKey(KeyCode.LeftControl) ) {
                sniper_motor.motorSpeed = -10;
            } else {
                sniper_motor.motorSpeed = -100;
            }
            sniper_hinge.motor = sniper_motor;
        } else if( Input.GetKeyDown(KeyCode.Space) ) {
            Fire();
        } else if( Input.GetKeyDown(KeyCode.R) ) {
            clip_script.Reload();
        } else {
            sniper_motor.motorSpeed = 0;
            sniper_hinge.motor = sniper_motor;
        }
    }

    void InitializeMotor() {
        sniper_hinge = GetComponent<HingeJoint2D>();
        sniper_motor = sniper_hinge.motor;
        sniper_motor.motorSpeed = 0;
        sniper_motor.maxMotorTorque = 1000;
        sniper_hinge.motor = sniper_motor;
    }

    void Fire() {
        if( clip_script.ammo_remaining >= 0 && CheckAnimation("Static State") ) {
            rifle_animator.SetTrigger("ShotFired");
            shot_sound.Play();
            LaunchBullet();
            StartCoroutine(DropCasing(0.3f));
            clip_script.EjectRound();
        }
    }

    void LaunchBullet() {
        float y_initial = bullet_spawn.GetComponent<Transform>().eulerAngles.z - 270;
        float x_initial = System.Math.Abs(270 - bullet_spawn.GetComponent<Transform>().eulerAngles.z);
        float y_velocity = y_initial * y_vel_multiplier;
        float x_velocity = (90 - x_initial) * x_vel_multiplier;

        spawned_bullet = Instantiate(bullet_prefab, bullet_position.position, bullet_spawn.GetComponent<Transform>().rotation);
        spawned_bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_velocity, y_velocity));
    }

    private IEnumerator DropCasing(float delay) {
        yield return new WaitForSeconds(delay);
        System.Random rng = new System.Random();
        float y_velocity = rng.Next(0, 20);
        float x_velocity = rng.Next(-20, 0);

        spawned_casing = Instantiate(casing_prefab, casing_position.position, casing_spawn.GetComponent<Transform>().rotation);
        spawned_casing.GetComponent<Rigidbody2D>().AddForce(new Vector2(x_velocity, y_velocity));
        StartCoroutine(DestroyCasing(spawned_casing, 5f));
    }

    private IEnumerator DestroyCasing(GameObject casing, float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(casing);
    }

    private bool CheckAnimation(string state_name) {
        if( rifle_animator.GetCurrentAnimatorStateInfo(0).IsName(state_name) ) {
            return true;
        } else {
            return false;
        }
    }
}
