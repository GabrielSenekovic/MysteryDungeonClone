using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database
{
    private static ItemDatabase m_ItemDatabase;
    private static LetterDatabase m_LetterDatabase;
    public static ItemDatabase Item
    {
        get
        {
            if(m_ItemDatabase == null)
            {
                m_ItemDatabase = Resources.Load<ItemDatabase>("Databases/ItemDatabase"); //Here it looks for the database in this folder
            }
            return m_ItemDatabase;
        }
    
    }
    public static LetterDatabase Letter
    {
        get
        {
            if (m_LetterDatabase == null)
            {
                m_LetterDatabase = Resources.Load<LetterDatabase>("Databases/LetterDatabase"); //Here it looks for the database in this folder
            }
            return m_LetterDatabase;
        }

    }
}
