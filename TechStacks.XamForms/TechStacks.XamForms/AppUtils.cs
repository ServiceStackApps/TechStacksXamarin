using System;
using ServiceStack;

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
}

