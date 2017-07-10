using System;
using System.Collections;

public class UniqueSingleton<T> where T : UniqueSingleton<T>, new()
{
    private static T m_instance = null;

    public static T Instance
    {
        get
        {
            if (null == m_instance)
            {
                m_instance = new T();
            }

            return m_instance;
        }
    }
}