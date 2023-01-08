using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Animator _animator;

        private int _umbrellaLayer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _umbrellaLayer = _animator.GetLayerIndex("Umbrella");
        }

        private void FlipSprite(float xVelocity)
        {
            if (xVelocity > 0)
            {
                _renderer.flipX = true;
            }
            else if (xVelocity < 0)
            {
                _renderer.flipX = false;
            }
        }

        public void UpdateAnimations(Player player, float speed, bool isGrounded, bool isThrown)
        {
            if(player.HoldUpInput && !isThrown)
            {
                _animator.SetLayerWeight(_umbrellaLayer, 1);
            }
            else
            {
                _animator.SetLayerWeight(_umbrellaLayer, 0);
            }

            _animator.SetBool("OnGround", isGrounded);

            _animator.SetFloat("Speed", Mathf.Abs(speed));
            FlipSprite(player.MoveInput.x);
        }

        public void Jump()
        {
            _animator.SetTrigger("Jump");
        }

        public void Throw()
        {
            _animator.SetTrigger("Throw");
        }

        public void EnterPortal()
        {
            _animator.SetTrigger("Enter");
        }
    }
}
