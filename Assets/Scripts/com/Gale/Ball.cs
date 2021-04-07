using System;
using System.Collections.Generic;
using System.Linq;
using com.Gale.Player;
using com.Gale.Powerups;
using UnityEngine;
using Random = UnityEngine.Random;

namespace com.Gale
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour
    {
        
        public float speed = 5f;

        // Used just in case that the ball gets stuck going vertically.
        public Vector2 constantAppliedMovementFactor = new Vector2(0.05f, 0f);

        private IPowerup _powerup;

        public IPowerup Powerup
        {
            get { return _powerup; }

            set
            {
                if (_powerup != null)
                {
                    _powerup = null;

                    // Reset sprite
                    spriteRenderer.sprite = defaultBallSprite;
                    transform.localScale = ballScale;
                }

                _powerup = value;
            }
        }

        [SerializeField] private LayerMask layerMask;

        public Sprite defaultBallSprite;
        public SpriteRenderer spriteRenderer;
       
        // Quick hack to fixing the ball's scale on DestroyPowerup().
        [SerializeField] private Vector3 ballScale = new Vector3(0.5f, 0.5f, 1.0f);

        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;

        private List<ContactPoint2D> _collisions = new List<ContactPoint2D>();

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
        }

        public void ChangeToRandomVelocity()
        {
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;
        }

        // TODO move this to the GlobalState
        public void OnGoal()
        {
            transform.position = Vector3.zero;
            _rigidbody2D.velocity = Random.insideUnitCircle.normalized * speed;

            var shouldDie = Powerup?.ShouldDieOnGoal();
            if (shouldDie.GetValueOrDefault(true))
            {
                DestroyPowerup();
            }
        }

        
        public void DestroyPowerup()
        {
            Powerup = null;
        }

        private void FixedUpdate()
        {
            // var velocity = CalculateVelocity();
            
            // transform.position += new Vector3(velocity.x, velocity.y) * 0.001f;
            _rigidbody2D.velocity = CalculateVelocity();
            // _rigidbody2D.AddForce(velocity * 0.001f);
        }

        private Vector2 CalculateVelocity()
        {
            // TODO: Don't set rigidbody velocity and figure out if the velocity needs to be mutated here.
            var velocity = _rigidbody2D.velocity;
            return (Powerup?.CalculateBallVelocity(_rigidbody2D)).GetValueOrDefault(velocity + constantAppliedMovementFactor * Mathf.Sign(velocity.x));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
                var collidedPowerup = other.gameObject.GetComponent<IPowerup>();
                collidedPowerup.OnCollectPowerup(this);
                Powerup = collidedPowerup;
                Debug.Log("Collided with a powerup!\n" + Powerup);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
 
            // Bug here, the ball just gets moved out of the object instead of in the direction it came in.
            var distance = _collider2D.Distance(other.collider);
            if (distance.isOverlapped)
            {
                var translation = distance.normal * distance.distance;
                transform.position += new Vector3(translation.x, translation.y);
            }
            
            var contactCount = other.GetContacts(_collisions);
            
            // DON'T USE _contacts.Clear(), since it doesn't work. You can't foreach the contacts.
            var aggregateNormal = Vector2.zero;
            for (var i = 0; i < contactCount; i++)
            {
                aggregateNormal += _collisions[i].normal / contactCount;
            }

            var contactPoint2Ds = _collisions.Take(contactCount).ToArray();

            var vector = Powerup?.OnBallCollision(new BallCollisionDetails
            {
                AggregateNormal = aggregateNormal,
                Ball = this,
                Contacts = contactPoint2Ds,
                GameObject = other.gameObject
            });
            var vectorVal = vector.GetValueOrDefault(Vector2.zero);

            var reflectVector = Vector2.Reflect(_rigidbody2D.velocity, aggregateNormal);
           
            // TODO: change this to be just whether or not it was null (will break compatibility with a few powerups)
            if (vectorVal != Vector2.zero)
            {
                // The VERY obvious bug in this is that the powerup may not handle collisions at all.
                _rigidbody2D.velocity = vectorVal;
                return;
            }
            
            if (other.gameObject.CompareTag("Paddle"))
            {
                var paddle = other.gameObject.GetComponent<Paddle>();
                var position = transform.position;
                var otherPosition = other.transform.position;

                // var angleBetweenBallAndPaddle = Vector2.Angle(otherPosition, position) * Mathf.Deg2Rad;

                var percentFromPaddleCenter =
                    Mathf.Clamp(2 * (position.y - otherPosition.y) / other.transform.localScale.y,
                        -1.0f, 1.0f);

                var reflectAngle = paddle.maxPaddleHitAngle * Mathf.Deg2Rad * percentFromPaddleCenter;
                var newReflectVector = new Vector2(Mathf.Cos(reflectAngle) * reflectVector.magnitude * Mathf.Sign(aggregateNormal.x),
                    Mathf.Sin(reflectAngle) * reflectVector.magnitude);

                _rigidbody2D.velocity = newReflectVector;
                return;
            } 
            // Do normal physics
            _rigidbody2D.velocity = reflectVector;
        }

        // Just used for when the ball gets stuck in a wall.
        private void OnCollisionStay2D(Collision2D other)
        {
            var distance = _collider2D.Distance(other.collider);
            if (!distance.isOverlapped) return;
            
            var translation = distance.distance * distance.normal;
            transform.position += new Vector3(translation.x, translation.y);
 
            
            var contactCount = other.GetContacts(_collisions);
            // DON'T USE _contacts.Clear(), since it doesn't work. You can't foreach the contacts.
            var aggregateNormal = Vector2.zero;
            for (var i = 0; i < contactCount; i++)
            {
                aggregateNormal += _collisions[i].normal / contactCount;
            }

            var dot = Vector2.Dot(_rigidbody2D.velocity, aggregateNormal);
            // If the ball is moving into the wall
            if (dot < 0)
            {
                OnCollisionEnter2D(other);
            }

        }
    }
}
