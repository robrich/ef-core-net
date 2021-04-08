using EFCoreOnNet5.Demos;

namespace EFCoreOnNet5
{
	public class App
	{
		private readonly Demo1 demo1;
		private readonly Demo2 demo2;
		private readonly Demo3 demo3;
		private readonly Demo4 demo4;
		private readonly Demo5 demo5;

		public App(Demo1 demo1, Demo2 demo2, Demo3 demo3, Demo4 demo4, Demo5 demo5)
		{
			this.demo1 = demo1 ?? throw new System.ArgumentNullException(nameof(demo1));
			this.demo2 = demo2 ?? throw new System.ArgumentNullException(nameof(demo2));
			this.demo3 = demo3 ?? throw new System.ArgumentNullException(nameof(demo3));
			this.demo4 = demo4 ?? throw new System.ArgumentNullException(nameof(demo4));
			this.demo5 = demo5 ?? throw new System.ArgumentNullException(nameof(demo5));
		}

		public void Start()
		{
			demo1.Start();
			demo2.Start();
			demo3.Start();
			demo4.Start();
			demo5.Start();
		}

	}
}
