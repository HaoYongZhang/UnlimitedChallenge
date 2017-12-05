using UnityEngine;
using System.Collections;
using EnemyClass;

namespace EnemyClass
{
    public class AI : MonoBehaviour
    {
        public GameObject _player;
        public Animator _animator;
        public Rigidbody _rigidbody;
        public State state = State.idle;

        //感知距离
        public float senseDistance = 50f;
        //攻击距离
        public float attackDistance = 15f;
        //移动速度
        public float moveSpeed = 20f;
        //是否正在攻击期间
        bool isDuringAttack;

        void Start()
        {
            _player = Global.hero.gameObject;
            _animator = gameObject.GetComponent<Animator>();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            sense();
            think();
            action();
        }

        void FixedUpdate()
        {
            if (isInAnimation("Chase"))
            {
                move();
            }

        }

        /// <summary>
        /// 感知
        /// </summary>
        void sense()
        {
            //判断敌人与主角之间的距离
            float distance = (_player.transform.position - this.transform.position).magnitude;
            //主角进入感知距离
            if (distance <= senseDistance)
            {
                //Debug.Log("主角进入感知距离");
                //主角进入攻击距离，进行攻击
                if(distance <= attackDistance)
                {
                    //Debug.Log("主角进入攻击距离，进行攻击");
                    faceTo(_player);
                    state = State.attack;
                }
                //主角不在攻击距离，进行追击
                else
                {
                    //Debug.Log("主角不在攻击距离，进行追击");
                    faceTo(_player);
                    state = State.chase;
                }
            }
            //主角不在感知范围
            else
            {
                //Debug.Log("主角不在感知范围");
                state = State.idle;
            }
        }


        /// <summary>
        /// 思考
        /// </summary>
        void think()
        {

        }


        /// <summary>
        /// 行动
        /// </summary>
        void action()
        {
            switch (state)
            {
                case State.idle:
                    {
                        idle();
                    }
                    break;
                case State.chase:
                    {
                        chase();
                    }
                    break;
                case State.attack:
                    {
                        attack();
                    }
                    break;
            }
        }

        void idle()
        {
            if (!isDuringAttack)
            {
                _animator.SetBool("chase", false);
                _animator.SetBool("attack", false);
            }
        }

        void chase()
        {
            if(!isDuringAttack)
            {
                _animator.SetBool("chase", true);
            }

        }

        void move()
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * moveSpeed * Time.deltaTime);
        }

        void attack()
        {
            if (!isDuringAttack)
            {
                Debug.Log("出发攻击");
                _animator.SetBool("chase", false);
                _animator.SetBool("attack", true);
                isDuringAttack = true;
            }
        }

        bool isInAnimation(string animationName)
        {
            AnimatorStateInfo stateinfo = _animator.GetCurrentAnimatorStateInfo(0);

            return stateinfo.IsName("Base Layer." + animationName);
        }

        void startAttack()
        {
            
        }

        void inAttack()
        {
            
        }

        //攻击完成
        void endAttack()
        {
            Debug.Log("结束攻击");
            isDuringAttack = false;
            _animator.SetBool("attack", false);
        }

        void faceTo(GameObject obj)
        {
            Vector3 point = new Vector3(obj.transform.position.x, transform.position.y, obj.transform.position.z);
            transform.LookAt(point);
        }
    }
}