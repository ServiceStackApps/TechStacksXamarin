using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceStack;

namespace TechStacks.XamForms
{
	public class AppUtils
	{
		private static JsonHttpClient serviceClient;
		public static JsonHttpClient ServiceClient
		{
			get { return serviceClient ?? (serviceClient = new JsonHttpClient("http://techstacks.io/")); }
		}
	}

    public static class Extensions
    {
        public static void UpdateDataSource<T>(this ObservableCollection<T> collection, List<T> Data)
        {
            collection.Clear();
            foreach (T item in Data)
            {
                collection.Add(item);
            }
        }
    }
}

