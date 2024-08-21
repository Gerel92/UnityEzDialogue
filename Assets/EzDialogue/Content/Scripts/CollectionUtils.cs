using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionUtils
{
    #region List
    public static void AddUnique<T>(this List<T> list, T item)
    {
        if (!list.Contains(item)) list.Add(item);
    }

    /// <summary>
    /// If index is superior to list size, increase list size. Then sets item at index
    /// </summary>
    /// <param name="defaultValue"> When increasing the list size, sets new element with this value </param>
    public static void SetElementWithSizeToFit<T>(this List<T> list, T item, int index, T defaultValue = default)
    {
        for (int i = list.Count; i <= index; i++)
            list.Add(defaultValue);

        list[index] = item;
    }

    /// <summary>
    /// If index is superior to list size, increase list size. Then gets at index
    /// </summary>
    /// <param name="defaultValue"> When increasing the list size, sets new element with this value </param>
    public static T GetElementWithSizeToFit<T>(this List<T> list, int index, T defaultValue = default)
    {
        for (int i = list.Count; i <= index; i++)
            list.Add(defaultValue);

        return list[index];
    }
    #endregion

    #region Dictionary
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary == null)
            throw new ArgumentNullException(nameof(dictionary));

        if (dictionary.ContainsKey(key)) dictionary[key] = value;
        else dictionary.Add(key, value);
    }

    /// <summary>
    /// Add once to the list associated to key, if list doesn't exist, create a new one. 
    /// For more information, see List<T>.AddOnce(T value)
    /// </summary>
    public static void AddToListOnce<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
        if (dictionary.TryGetValue(key, out List<TValue> list))
            list.AddUnique(value);
        else
            dictionary.Add(key, new List<TValue>() { value });
    }

    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue valueIfNull = default)
    {
        return dictionary.TryGetValue(key, out TValue value) ? value : valueIfNull;
    }
    #endregion
}
