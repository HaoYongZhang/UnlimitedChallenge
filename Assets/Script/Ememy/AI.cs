using UnityEngine;
using System.Collections;
using Utility;

namespace EnemyClass
{
    public class AI : MonoBehaviour
    {
        Enemy _enemy;

        public GameObject _player;
        public Animator _animator;
        public Rigidbody _rigidbody;
        public Action actionState = Action.idle;
        public Sense senseState = Sense.none;

        //是否正在攻击期间
        bool isDuringAttack;

        void Start()
        {
            _enemy = GetComponent<Enemy>();
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
            if (distance <= (float.Parse(_enemy.data["senseDistance"])))
            {
                //主角进入攻击距离，进行攻击
                if (distance <= (float.Parse(_enemy.data["attackDistance"])))
                {
                    senseState = Sense.attackRange;
                }
                //主角不在攻击距离，进行追击
                else
                {
                    senseState = Sense.senseRange;
                }
            }
            //主角不在感知范围
            else
            {
                senseState = Sense.none;
            }
        }


        /// <summary>
        /// 思考
        /// </summary>
        void think()
        {
            if (GetComponent<Enemy>().property.hp < 0)
            {
                actionState = Action.death;
            }
            else
            {
                switch (senseState)
                {
                    case Sense.none:
                        {
                            actionState = Action.idle;
                        }
                        break;
                    case Sense.senseRange:
                        {
                            faceTo(_player);
                            actionState = Action.chase;
                        }
                        break;
                    case Sense.attackRange:
                        {
                            faceTo(_player);
                            actionState = Action.attack;
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// 行动
        /// </summary>
        void action()
        {
            switch (actionState)
            {
                case Action.idle:
                    {
                        idle();
                    }
                    break;
                case Action.chase:
                    {
                        chase();
                    }
                    break;
                case Action.attack:
                    {
                        attack();
                    }
                    break;
                case Action.death:
                    {
                        death();
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
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * _enemy.property.moveSpeed * Time.deltaTime);
        }

        void attack()
        {
            if (!isDuringAttack)
            {
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
            float attackDistance = float.Parse(_enemy.data["attackDistance"]);

            float distance = (_player.transform.position - this.transform.position).magnitude;

            if (distance <= attackDistance)
            {
                Debug.Log(attackDistance);
                DamageType damageType = PropertyUtil.GetEnum<DamageType>(_enemy.data["damageType"]);

                DamageManager.CommonAttack<Enemy, Hero>(gameObject, Global.hero.gameObject, damageType);
            }
            else
            {
                endAttack();
                actionState = Action.chase;
            }
        }

        //攻击完成
        void endAttack()
        {
            isDuringAttack = false;
            _animator.SetBool("attack", false);
        }

        void faceTo(GameObject obj)
        {
            Vector3 point = new Vector3(obj.transform.position.x, transform.position.y, obj.transform.position.z);
            transform.LookAt(point);
        }

        void death()
        {
            GetComponent<Animator>().SetBool("death", true);

            //死亡时销毁血条
            if(GetComponent<Enemy>().hpSlider != null)
            {
                Destroy(GetComponent<Enemy>().hpSlider.gameObject);
            }
        }

        void endDeath()
        {
            Destroy(gameObject, 3);
        }
    }
}