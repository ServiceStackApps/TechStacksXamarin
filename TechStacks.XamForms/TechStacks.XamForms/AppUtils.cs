using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ServiceStack;
using TechStacks.ServiceModel;
using TechStacks.ServiceModel.Types;

namespace TechStacks.XamForms
{
	public class AppUtils
	{
		private static JsonServiceClient serviceClient;
		public static JsonServiceClient ServiceClient
		{
			get
			{
				return (serviceClient = new JsonServiceClient("http://techstacks.io/"));
			}
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

