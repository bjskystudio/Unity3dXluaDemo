using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransform 
{
    public MapTransform(MapMachine start, MapMachine end, string fieldName, int fieldIndex = 0)
    {
        m_start = start;
        m_end = end;
        m_fieldName = fieldName;
        m_fieldIndex = fieldIndex;
    }
    public string m_fieldName;
    public int m_fieldIndex;
    public MapMachine m_start;
    public MapMachine m_end;
}
