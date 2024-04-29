using System;

namespace Keiwando.BigInteger
{
	public class BigIntegerTest
	{
		public static void RunTests()
		{
			TestAddition();
			TestExponentiation();
			CustomTest();
		}

		private static void TestAddition()
		{
			BigInteger leftSide = new BigInteger("1000");
			BigInteger bigInteger = BigInteger.Add(leftSide, new BigInteger("1"));
			Assert(bigInteger == 1001, "static Add not working - result: " + bigInteger);
		}

		private static void TestExponentiation()
		{
			BigInteger bigInteger = 410;
			BigInteger bigInteger2 = 29;
			BigInteger bigInteger3 = BigInteger.Pow(bigInteger, bigInteger2);
			bigInteger3 = bigInteger.Pow(bigInteger2);
			Console.WriteLine(string.Concat(bigInteger, "^", bigInteger2, " = ", bigInteger3));
		}

		private static void CustomTest()
		{
			BigInteger bigInteger = new BigInteger("1");
			Console.WriteLine(bigInteger.GetDataAsString());
			Console.WriteLine(uint.MaxValue);
		}

		private static void Main(string[] args)
		{
			RunTests();
			Console.Write("All tests run successfully!");
		}

		private static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new Exception(message);
			}
		}
	}
}
