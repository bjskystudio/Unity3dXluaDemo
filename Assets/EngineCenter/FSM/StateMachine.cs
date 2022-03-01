using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BaseState<T>
    {
        public virtual void OnEnter(StateMachine<T> machine)
        {

        }

        public virtual void OnExit(StateMachine<T> machine)
        {

        }

        public virtual void Update(StateMachine<T> machine)
        {

        }

        public virtual void LateUpdate(StateMachine<T> machine)
        {

        }

        public virtual void FixedUpdate(StateMachine<T> machine)
        {

        }

        public static BaseState<T> NullState = new BaseState<T>();
    }

    public class StateMachine<T>
    {
        public T _Instance { private set; get; }
        private BaseState<T> mCurrentState;

        public StateMachine(T instance):this(instance, BaseState<T>.NullState)
        { }

        public StateMachine(T instance, BaseState<T> defaultState)
        {
            _Instance = instance;
            mCurrentState = defaultState;
            mCurrentState.OnEnter(this);
        }

        private Stack<BaseState<T>> m_Stack = new Stack<BaseState<T>>();
        public void PushState(BaseState<T> state, bool checkCurrentState = false)
        {
            if(!checkCurrentState || mCurrentState != state)
            {
                m_Stack.Push(state);
            }
            
            while(m_Stack.Count > 0)
            {
                mCurrentState.OnExit(this);
                mCurrentState = m_Stack.Pop();
                mCurrentState.OnEnter(this);
            }
        }

        public void StopMachine(bool resetState = true)
        {
            mCurrentState.OnExit(this);
            if(resetState)
            {
                mCurrentState = BaseState<T>.NullState;
            }
        }

        public void Update()
        {
            mCurrentState.Update(this);
        }

        public void LateUpdate()
        {
            mCurrentState.LateUpdate(this);
        }

        public void FixedUpdate()
        {
            mCurrentState.FixedUpdate(this);
        }
    }
}
