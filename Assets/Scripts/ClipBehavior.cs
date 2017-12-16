using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBehavior : MonoBehaviour {
    private SpriteRenderer[] cartridge_sprites;
    private Transform[] cartridge_positions;
    private Rigidbody2D[] cartridge_rbs;
    private List<GameObject> cartridge_list;

    [Range(0, 20)]
    public int max_ammo;
    public int ammo_remaining;
    public Sprite cartridge_sprite;
    public Sprite casing_sprite;
    public GameObject cartridge_prefab;

    private void Start () {
        cartridge_list = new List<GameObject>();
        CreatePlaceholders();

    }

    private void Update () {
    }

    private void CreatePlaceholders() {
        Vector3 cartridge_position = new Vector3();
        Quaternion cartridge_rotation = new Quaternion();

        cartridge_position = GameObject.Find("Cartridge Spawn").GetComponent<Transform>().position;
        cartridge_rotation = GameObject.Find("Cartridge Spawn").GetComponent<Transform>().rotation;
        cartridge_list.Add(Instantiate(cartridge_prefab, cartridge_position, cartridge_rotation));
        cartridge_list[0].transform.parent = gameObject.transform;

        for( int i = 0; i < max_ammo - 1; i++ ) {
            cartridge_position = cartridge_list[i].GetComponent<Transform>().position;
            cartridge_position += new Vector3(0.15f, 0, 0);
            cartridge_rotation = cartridge_list[i].GetComponent<Transform>().rotation;

            cartridge_list.Add(Instantiate(cartridge_prefab, cartridge_position, cartridge_rotation));
            cartridge_list[i + 1].transform.parent = gameObject.transform;
        }
        ammo_remaining = max_ammo - 1;

        cartridge_sprites = GetComponentsInChildren<SpriteRenderer>();
        cartridge_positions = GetComponentsInChildren<Transform>();
        cartridge_rbs = GetComponentsInChildren<Rigidbody2D>();

    }

    private IEnumerator DestroyCartridge(GameObject cartridge, float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(cartridge);
    }

    public void Reload() {
        if( gameObject.transform.childCount == 1 ) {
            cartridge_list.Clear();
            CreatePlaceholders();
        }
    }

    public void EjectRound() {
        if( ammo_remaining >= 0 ) {
            cartridge_sprites[ammo_remaining + 1].sprite = casing_sprite;
            cartridge_positions[ammo_remaining + 2].position -= new Vector3(0, 0.1f, 0); // We have to use ammo_remaining + 1
                                                                                         // here because the parent object also
                                                                                         // has a transform component
            cartridge_rbs[ammo_remaining + 1].gravityScale = 0.5f;
            cartridge_rbs[ammo_remaining + 1].AddTorque(-40f);
            StartCoroutine(DestroyCartridge(cartridge_sprites[ammo_remaining + 1].gameObject, 2f));
            ammo_remaining--;
        }
    }

}
