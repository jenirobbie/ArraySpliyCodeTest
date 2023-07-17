using System.Linq;

namespace Shared
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Takes an enumerable and splits it into an array of N enumerable items, split evenly where possible 
        /// </summary>
        /// <typeparam name="T">Enumereble item type</typeparam>
        /// <param name="enumerableToSplit">The enumerable that is being split</param>
        /// <param name="numOfPartsToSplitInto">the number of items for the enumerable to be split into</param>
        /// <returns>Returns an array of enumerables split into N parts.
        /// If enumerable to split is empty or  numOfPartsToSplitInto is 0 an empty array of enumerable is returned.</returns>
        /// <exception cref="ArgumentException">Will throw exception if number of items to split is less than 0</exception>
        public static IEnumerable<T>[] Split<T>(this IEnumerable<T> enumerableToSplit, int numOfPartsToSplitInto)
        {

            if (numOfPartsToSplitInto < 0)
                throw new ArgumentException("Number of elements to split enumerable must be zero or greater");

            var enumerableLength = enumerableToSplit.Count();
            if (enumerableLength == 0 || numOfPartsToSplitInto == 0)
                return new IEnumerable<T>[] { };

            if (numOfPartsToSplitInto == 1)
                return new IEnumerable<T>[] { enumerableToSplit };

            return GetSplitItem(enumerableToSplit.ToArray(), enumerableLength, numOfPartsToSplitInto).ToArray();
        }

        private static IEnumerable<IEnumerable<T>> GetSplitItem<T>(T[] arrayToSplitToSplit, int enumerableLength, int numOfPartsToSplitInto)
        {
            var numOfItemsInEachArray = Math.Round(enumerableLength / (double)numOfPartsToSplitInto, MidpointRounding.AwayFromZero);
            var splitArray = new List<T>();

            for (int i = 0; i < enumerableLength; i++)
            {
                if (splitArray.Count() < numOfItemsInEachArray)
                {
                    splitArray.Add(arrayToSplitToSplit[i]);
                }
                else
                {
                    yield return splitArray;
                    splitArray = new()
                    {
                        arrayToSplitToSplit[i]
                    };
                }
            }
            //Return final array part
            yield return splitArray;
        }
    }
}