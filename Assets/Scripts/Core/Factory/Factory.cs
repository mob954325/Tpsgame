using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory<T> : MonoBehaviour where T : Product
{
    /// <summary>
    /// 팩토리에서 관리하는 T타입 리스트
    /// </summary>
    private List<T> products;

    /// <summary>
    /// 팩토리에서 생성 할 준비된 타입 큐
    /// </summary>
    private Queue<T> readyQueue;

    public GameObject originalProduct;

    public int capacity = 8;

    // 1. 제품 생성
    // 2. 제품 초기화
    // 2.1 리스트 초기화
    // 2.2 레디큐 초기화
    // 3. 제품 소환 코드
    
    public void Init()
    {
        products = new List<T>(capacity);
        readyQueue = new Queue<T>(capacity);

        for(int i = 0; i < capacity; i++)
        {
            // 리스트 초기화
            CreateProduct(i);
        }
    }

    /// <summary>
    /// 팩토리가 관리하는 오브젝트 생성
    /// </summary>
    /// <param name="pos">위치 값</param>
    /// <param name="rot">회전 값</param>
    /// <returns>생성한 오브젝트의 T 타입</returns>
    protected T SpawnProduct(Vector3? pos = null, Quaternion? rot = null)
    {
        if(readyQueue.Count < 1)
        {
            AddCapacity();
            Debug.Log($"{gameObject.name} 용량 증가");
        }

        T curProduct = readyQueue.Dequeue();
        curProduct.OnDeactive += () => { readyQueue.Enqueue(curProduct); }; // 비활성화 시 다시 큐로 복귀

        GameObject obj = curProduct.gameObject;
        obj.SetActive(true);
        obj.transform.position = pos.GetValueOrDefault();
        obj.transform.rotation = rot.GetValueOrDefault();

        return curProduct;
    }

    private void CreateProduct(int numbering = -1)
    {
        GameObject product = Instantiate(originalProduct, this.gameObject.transform);
        product.gameObject.name = $"{originalProduct.gameObject.name}_{numbering}";
        products.Add(product.GetComponent<T>());
        readyQueue.Enqueue(product.GetComponent<T>());
        product.SetActive(false);
    }

    private void AddCapacity()
    {
        List<T> preProducts = new List<T>(capacity);
        foreach(T item in products)
        {
            preProducts.Add(item);
        }

        capacity *= 2;

        products = new List<T>(capacity);

        foreach(T item in preProducts)
        {
            products.Add(item);
            readyQueue.Enqueue(item.GetComponent<T>());
        }

        for(int i = 0; i < capacity / 2; i++)
        {
            CreateProduct(i + 1 + capacity / 2);
        }
    }
}
