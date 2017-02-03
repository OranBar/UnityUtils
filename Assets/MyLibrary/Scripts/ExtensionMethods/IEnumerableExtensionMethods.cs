using System;
using System.Collections.Generic;

public static class IEnumerableExtensionMethods {

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> myEnum, Action<T> action) {
        foreach (var element in myEnum) {
            action(element);
        }

        return myEnum;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> myEnum, Action<int, T> action) {
        int index = 0;
        foreach (var element in myEnum) {
            action(index++, element);
        }

        return myEnum;
    }



}
