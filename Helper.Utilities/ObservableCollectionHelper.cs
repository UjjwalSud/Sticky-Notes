using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace Helper.Utilities
{
    public static class ObservableCollectionHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            ObservableCollection<T> newObservableCollection = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                newObservableCollection.Add(cur);
            }
            return newObservableCollection;
        }

        public static List<T> ToList<T>(this ObservableCollection<T> enumerable)
        {
            List<T> newList = new List<T>();

            foreach (var cur in enumerable)
            {
                newList.Add(cur);
            }
            return newList;
        }
    }
}
