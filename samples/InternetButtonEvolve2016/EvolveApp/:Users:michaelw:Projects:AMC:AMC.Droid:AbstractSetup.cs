using System;

using NUnit.Framework;

using Xamarin.UITest;

using System.Threading;

namespace AMC.Tests
{
	[TestFixture(Platform.Android)]
	public abstract class AbstractSetup
	{
		protected IApp app;
		protected Platform platform;

		protected AbstractSetup(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public virtual void BeforeEachTest()
		{
			app = AppInitializer.StartApp();
		}
	}
}