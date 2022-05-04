using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class Controller : MonoBehaviour
    {
        public State currentState;  //Apuntadosr al estado actual
        public State remainState;


        public bool ActiveAI { get; set; }

        void Start()
        {
            ActiveAI = true;  //Para activar AI
        }
        private Animator _animatorController;
        private InputSystemKeyboard _inputSystem;
        private CharacterEngine _charEng;
        private FieldOfView _fieldView;
        private EnemyWarriorNavigation _enNav;

        private void Awake()
        {
            _animatorController = GetComponent<Animator>();
            _inputSystem = GetComponent<InputSystemKeyboard>();
            _charEng = GetComponent<CharacterEngine>();
            _fieldView = GetComponent<FieldOfView>();
            _enNav = GetComponent<EnemyWarriorNavigation>();
        }

        void Update()
        {
            if (!ActiveAI)
            {
                return;
            }

            currentState.UpdateState(this);
        }

        public void Transition(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
            }
        }

        public void SetAnimation(string animation, bool value)
        {
            _animatorController.SetBool(animation, value);
        }

        public float ReturnHor()
        {
            return _inputSystem.ReturnAxHor();
        }

        public float ReturnVer()
        {
            return _inputSystem.ReturnAxVer();
        }

        public bool ReturnCrouched()
        {
            return _charEng.ReturnCrouch();
        }

        public bool ReturnJumped()
        {
            return _charEng.ReturnJump();
        }

        public bool ReturnRunning()
        {
            return _charEng.ReturnRun();
        }

        public bool ReturnSeeing()
        {
            return _fieldView.ReturnSeePlayer();
        }

        public bool ReturnGuarding()
        {
            return _enNav.ReturnGuard();
        }

        public bool ReturnPatroling()
        {
            return _enNav.ReturnPatrol();
        }
    }
}
