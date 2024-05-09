using UnityEngine;

public class CloudKitUseExample : BaseIOSFeaturePreview
{
	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Cloud Kit", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Create Record"))
		{
			CK_RecordID id = new CK_RecordID("1");
			CK_Record cK_Record = new CK_Record(id, "Post");
			cK_Record.SetObject("PostText", "Sample point of interest");
			cK_Record.SetObject("PostTitle", "My favorite point of interest");
			CK_Database publicDB = ISN_Singleton<ISN_CloudKit>.Instance.PublicDB;
			publicDB.SaveRecrod(cK_Record);
			publicDB.ActionRecordSaved += Database_ActionRecordSaved;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Delete Record"))
		{
			CK_RecordID recordId = new CK_RecordID("1");
			CK_Database publicDB2 = ISN_Singleton<ISN_CloudKit>.Instance.PublicDB;
			publicDB2.DeleteRecordWithID(recordId);
			publicDB2.ActionRecordDeleted += Database_ActionRecordDeleted;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Fetch Record"))
		{
			CK_RecordID recordId2 = new CK_RecordID("1");
			CK_Database publicDB3 = ISN_Singleton<ISN_CloudKit>.Instance.PublicDB;
			publicDB3.FetchRecordWithID(recordId2);
			publicDB3.ActionRecordFetchComplete += Database_ActionRecordFetchComplete;
		}
	}

	private void Database_ActionRecordFetchComplete(CK_RecordResult res)
	{
		res.Database.ActionRecordFetchComplete -= Database_ActionRecordFetchComplete;
		if (res.IsSucceeded)
		{
			Debug.Log("Database_ActionRecordFetchComplete:");
			Debug.Log("Post Title: " + res.Record.GetObject("PostTitle"));
		}
		else
		{
			Debug.Log("Database_ActionRecordFetchComplete, Error: " + res.Error.Description);
		}
	}

	private void Database_ActionRecordDeleted(CK_RecordDeleteResult res)
	{
		res.Database.ActionRecordDeleted -= Database_ActionRecordDeleted;
		if (res.IsSucceeded)
		{
			Debug.Log("Database_ActionRecordDeleted, Success: ");
		}
		else
		{
			Debug.Log("Database_ActionRecordDeleted, Error: " + res.Error.Description);
		}
	}

	private void Database_ActionRecordSaved(CK_RecordResult res)
	{
		res.Database.ActionRecordSaved -= Database_ActionRecordSaved;
		if (res.IsSucceeded)
		{
			Debug.Log("Database_ActionRecordSaved:");
			Debug.Log("Post Title: " + res.Record.GetObject("PostTitle"));
		}
		else
		{
			Debug.Log("Database_ActionRecordSaved, Error: " + res.Error.Description);
		}
	}
}
