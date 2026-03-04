using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   private Rigidbody _rb;
   private float _movementX, _movementY;
   [SerializeField] private float speed = 0f;
   [SerializeField] private TextMeshProUGUI countText;
   [SerializeField] private GameObject winTextObject;
   private int _count;
   private void Start()
   {
      _rb = GetComponent<Rigidbody>();
      _count = 0;
      SetCountText();
      winTextObject.SetActive(false);
   }

   private void OnMove(InputValue movementValue)
   {
      Vector2 movementVector = movementValue.Get<Vector2>();
      _movementX = movementVector.x;
      _movementY = movementVector.y;
   }

   private void FixedUpdate()
   {
      Vector3 movement = new Vector3(_movementX, 0f, _movementY);
      _rb.AddForce(movement * speed);
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Enemy"))
      {
         Destroy(gameObject);
         winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose";
         winTextObject.SetActive(true);
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("PickUp"))
      {
         other.gameObject.SetActive(false);
         _count++;
         SetCountText();
      }
   }

   private void SetCountText()
   {
      countText.text = ($"Count: {_count}");
      if (_count >= 12)
      {
         winTextObject.SetActive(true);
         Destroy(GameObject.FindGameObjectWithTag("Enemy"));
      }
   }
}
