using Arkus.Bpm;
using Arkus.BpmDataAccess;
using Arkus.Core;
using NUnit.Framework;

namespace {NAMESPACE}
{
	/// <summary>
	/// Summary description for TestAreaGateway.
	/// </summary>
	[TestFixture]
	public class Test{CLASS}Gateway
	{
		I{CLASS}Gateway gateway = null;
		[SetUp]
		public void SetUp()
		{
			gateway = new {CLASS}GatewayFactory().NewInstance;
		}

		[Test]
		public void TestConstructor()
		{
			Assert.IsNotNull(new {CLASS}GatewayFactory().NewInstance);
		}

		[Test]
		[ExpectedException(typeof({CLASS}Exception))]
		public void TestLoadWithoutValidId()
		{
			{CLASS} _{CLASS} = new {CLASS}();			
			gateway.Load(_{CLASS});
		}

		[Test]
		public void TestLoad()
		{
			{CLASS} _{CLASS} = new {CLASS}(1);
			gateway.Load(_{CLASS});
		}

		[Test]
		public void TestCreate()
		{
			{CLASS} _{CLASS} = new {CLASS}();
			{TABLE.COLUMNS}
			_{CLASS}.{COLUMN.NAME} = new Object();{/TABLE.COLUMNS}
			Assert.IsTrue(gateway.Create(_{CLASS})) ;
		}

		[Test]
		public void TestDelete()
		{
			{CLASS} _{CLASS}= new {CLASS}(1);
//			Assert.IsTrue(gateway.Delete(_{CLASS}));
		}

		[Test]
		public void TestUpdate()
		{
			{CLASS} _{CLASS} = new {CLASS}(1);
			gateway.Load(_{CLASS});
			Assert.IsTrue(gateway.Update(_{CLASS}));
		}

		[Test]
		public void TestList()
		{
			{CLASS}.{LIST} = gateway.GetList();
			Assert.IsTrue( {CLASS}.{LIST}.Count >= 0 );
		}
	}
}
