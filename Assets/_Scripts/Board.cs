using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public const int BoardSize = 15;

    [SerializeField]
    private Field fieldPrefab;
    [SerializeField]
    private float fieldSpacing = 0.25f;

    void Awake()
    {
        SpawnFields();
    }

    private void SpawnFields()
    {
        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                SpawnField(x, y);
            }
        }
    }

    private void SpawnField(int x, int y)
    {
        Vector3 position = CalculateFieldPosition(x, y);
        Field field = Instantiate(fieldPrefab, position, Quaternion.identity, transform);
        field.Initialize(x, y);
    }

    private Vector3 CalculateFieldPosition(int x, int y)
    {
        float offset = -fieldSpacing * (BoardSize - 1) / 2.0f;
        return new Vector3(x * fieldSpacing + offset, y * fieldSpacing + offset, 0.0f);
    }
}
