using System.Collections.Generic;
using UnityEngine;

namespace EngineCenter.Container
{
    // not thread safe
    public class SwapChain<T>
    {
        private List<T> m_FrontBuffer = new List<T>();
        private List<T> m_BackBuffer = new List<T>();
        private List<T> m_TempList;

        public List<T> GetData()
        {
            return m_FrontBuffer;
        }

        public bool Contins(T element)
        {
            return m_FrontBuffer.Contains(element);
        }

        public bool Contins_Back(T element)
        {
            return m_BackBuffer.Contains(element);
        }

        public void PushData(T element)
        {
            m_BackBuffer.Add(element);
        }

        public void Swap()
        {
            m_FrontBuffer.Clear();
            m_TempList = m_FrontBuffer;
            m_FrontBuffer = m_BackBuffer;
            m_BackBuffer = m_TempList;
        }

        public void ForceClear()
        {
            m_FrontBuffer.Clear();
            m_BackBuffer.Clear();
        }
    }
}
