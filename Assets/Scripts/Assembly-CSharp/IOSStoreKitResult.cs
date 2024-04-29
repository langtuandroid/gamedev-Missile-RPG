public class IOSStoreKitResult : ISN_Result
{
	private string _ProductIdentifier = string.Empty;

	private InAppPurchaseState _State = InAppPurchaseState.Failed;

	private string _Receipt = string.Empty;

	private string _TransactionIdentifier = string.Empty;

	private string _ApplicationUsername = string.Empty;

	public IOSTransactionErrorCode TransactionErrorCode
	{
		get
		{
			if (_Error != null)
			{
				return (IOSTransactionErrorCode)_Error.Code;
			}
			return IOSTransactionErrorCode.SKErrorNone;
		}
	}

	public InAppPurchaseState State
	{
		get
		{
			return _State;
		}
	}

	public string ProductIdentifier
	{
		get
		{
			return _ProductIdentifier;
		}
	}

	public string ApplicationUsername
	{
		get
		{
			return _ApplicationUsername;
		}
	}

	public string Receipt
	{
		get
		{
			return _Receipt;
		}
	}

	public string TransactionIdentifier
	{
		get
		{
			return _TransactionIdentifier;
		}
	}

	public IOSStoreKitResult(string productIdentifier, ISN_Error e)
		: base(e)
	{
		_ProductIdentifier = productIdentifier;
		_State = InAppPurchaseState.Failed;
	}

	public IOSStoreKitResult(string productIdentifier, InAppPurchaseState state, string applicationUsername = "", string receipt = "", string transactionIdentifier = "")
		: base(true)
	{
		_ProductIdentifier = productIdentifier;
		_State = state;
		_Receipt = receipt;
		_TransactionIdentifier = transactionIdentifier;
		_ApplicationUsername = applicationUsername;
	}
}
