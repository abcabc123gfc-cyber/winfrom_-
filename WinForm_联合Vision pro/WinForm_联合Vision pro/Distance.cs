#region namespace imports
using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.ToolBlock;
using System.Collections.Generic;
#endregion

public class CogToolBlockAdvancedScript_1 : CogToolBlockAdvancedScriptBase
{
    #region Private Member Variables
    private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
    private CogBlobTool cogBlobTool = null;
    private int[] answer = { 1, 4, 0, 3, 1 };
    private int score = 0;
    private List<CogGraphicLabel> lable;
    private List<CogBlobResult> CogBlobTools = new List<CogBlobResult>();
    private List<CogBlobResult> CogBlobToolsTemp = new List<CogBlobResult>();
    private List<List<CogBlobResult>> CogBlobToolResults = new List<List<CogBlobResult>>();
    #endregion

    /// <summary>
    /// Called when the parent tool is run.
    /// Add code here to customize or replace the normal run behavior.
    /// </summary>
    /// <param name="message">Sets the Message in the tool's RunStatus.</param>
    /// <param name="result">Sets the Result in the tool's RunStatus</param>
    /// <returns>True if the tool should run normally,
    ///          False if GroupRun customizes run behavior</returns>
    public override bool GroupRun(ref string message, ref CogToolResultConstants result)
    {
        // To let the execution stop in this script when a debugger is attached, uncomment the following lines.
        //#if DEBUG
        //        if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
        //#endif
        lable = new List<CogGraphicLabel>();

        // Run each tool using the RunTool function
        foreach (ICogTool tool in mToolBlock.Tools)
            mToolBlock.RunTool(tool, ref message, ref result);
        cogBlobTool = mToolBlock.Tools["CogBlobTool1"] as CogBlobTool;

        #region 初始化数组
       
        CogBlobTools.Clear();
        CogBlobToolsTemp.Clear();
        score = 0;
        CogBlobToolResults.Clear();
        #endregion
        var blobs = cogBlobTool.Results.GetBlobs();
        int count = blobs.Count;

        #region 添加到集合中 25个
        for (int i = 0; i < count; i++)
        {
            CogBlobTools.Add(cogBlobTool.Results.GetBlobs()[i]);
        }
        #endregion
        //对y轴进行遍历 都不为空
        for (int i = 0; i < count; i++)
        {
            for (int j = i; j < count; j++)
            {
                if (CogBlobTools[i].CenterOfMassY > CogBlobTools[j].CenterOfMassY)
                {
                    var cogBlobTooltemp = CogBlobTools[i];
                    CogBlobTools[i] = CogBlobTools[j];
                    CogBlobTools[j] = cogBlobTooltemp;
                }
            }
        }
        //切割y轴 对相邻的y轴存入一个集合当中 从小到大 存入
        for (int i = 0; i < CogBlobTools.Count; i++)
        {
            CogBlobToolsTemp.Add(CogBlobTools[i]);
            if (CogBlobToolsTemp.Count != 0 && CogBlobToolsTemp.Count == 5)
            {
                CogBlobToolResults.Add(new List<CogBlobResult>(CogBlobToolsTemp)); // 添加副本
                CogBlobToolsTemp.Clear();
            }
        }
        //对 x 轴进行排序
        for (int i = 0; i < CogBlobToolResults.Count; i++)
        {
            var group = CogBlobToolResults[i];
            for (int j = 0; j < group.Count - 1; j++)
            {
                for (int k = j + 1; k < group.Count; k++)
                {
                    if (group[k].CenterOfMassX < group[j].CenterOfMassX)
                    {
                        var t = group[k];
                        group[k] = group[j];
                        group[j] = t;
                    }
                }
            }
        }
        //
        bool isSelect = true;
        for (int i = 0; i < CogBlobToolResults.Count; i++)
        {
            var group = CogBlobToolResults[i];
            int maxIdx = 0;
            for (int j = 1; j < group.Count; j++)
            {
                if (group[j].Area > group[maxIdx].Area && group[j].Area > 500)
                {
                    maxIdx = j;
                    isSelect = true;
                }
            }
            // 打分
            if (i < answer.Length && maxIdx == answer[i] && isSelect)
            {
                score += 20;
                //isSelect = false;
            }
        }
        CogGraphicLabel lbl = new CogGraphicLabel();
        lbl.Text = score.ToString();
        lbl.X = 30;
        lbl.Y = 30 + 20;
        lbl.Font = new System.Drawing.Font("楷体", 20);
        lbl.Color = score >= 60 ? CogColorConstants.Green : CogColorConstants.Red;
        lable.Add(lbl);

        return false;
    }

    #region When the Current Run Record is Created
    /// <summary>
    /// Called when the current record may have changed and is being reconstructed
    /// </summary>
    /// <param name="currentRecord">
    /// The new currentRecord is available to be initialized or customized.</param>
    public override void ModifyCurrentRunRecord(Cognex.VisionPro.ICogRecord currentRecord)
    {
    }
    #endregion

    #region When the Last Run Record is Created
    /// <summary>
    /// Called when the last run record may have changed and is being reconstructed
    /// </summary>
    /// <param name="lastRecord">
    /// The new last run record is available to be initialized or customized.</param>
    public override void ModifyLastRunRecord(Cognex.VisionPro.ICogRecord lastRecord)
    {
        foreach (var item in lable)
        {
            mToolBlock.AddGraphicToRunRecord(item, lastRecord, "CogImageConvertTool1.InputImage", "");
        }

    }
    #endregion

    #region When the Script is Initialized
    /// <summary>
    /// Perform any initialization required by your script here
    /// </summary>
    /// <param name="host">The host tool</param>
    public override void Initialize(Cognex.VisionPro.ToolGroup.CogToolGroup host)
    {
        // DO NOT REMOVE - Call the base class implementation first - DO NOT REMOVE
        base.Initialize(host);


        // Store a local copy of the script host
        this.mToolBlock = ((Cognex.VisionPro.ToolBlock.CogToolBlock)(host));
    }
    #endregion

}

