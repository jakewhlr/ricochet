using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBehavior : MonoBehaviour {
    private SpriteRenderer[] cartridge_sprites;
    private Transform[] cartridge_positions;
    private Rigidbody2D[] cartridge_rbs;

    [Range(0, 5)]
    public int ammo_remaining;

    public Sprite cartridge_sprite;
    public Sprite casing_sprite;

    void Start () {
        cartridge_sprites = GetComponentsInChildren<SpriteRenderer>();
        cartridge_positions = GetComponentsInChildren<Transform>();
        cartridge_rbs = GetComponentsInChildren<Rigidbody2D>();
        ammo_remaining = cartridge_sprites.Length - 1;
    }
	
    void Update () {
    }

    public void EjectRound() {
        if( ammo_remaining >= 0 ) {
            cartridge_sprites[ammo_remaining].sprite = casing_sprite;
            cartridge_positions[ammo_remaining + 1].position -= new Vector3(0, 0.1f, 0); // We have to use ammo_remaining + 1
                                                                                         // here because the parent object also
                                                                                         // has a transform component
            cartridge_rbs[ammo_remaining].gravityScale = 0.5f;
            cartridge_rbs[ammo_remaining].AddTorque(-40f);
            ammo_remaining--;
        }
    }
}
