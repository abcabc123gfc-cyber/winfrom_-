#region namespace imports
using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using System.Collections.Generic;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
    #region Private Member Variables
    private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
    //声明list标签
    List<CogGraphicLabel> lable;
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
        // #if DEBUG
        // if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
        // #endif


        lable = new List<CogGraphicLabel>();
        // Run each tool using the RunTool function
        foreach (ICogTool tool in mToolBlock.Tools)
            mToolBlock.RunTool(tool, ref message, ref result);


        //声明pma
        CogPMAlignTool pMAlignTool = mToolBlock.Tools["正面_硬币"] as CogPMAlignTool;
        //声明圆
        CogFindCircleTool fitCircleTool = mToolBlock.Tools["CogFindCircleTool1"] as CogFindCircleTool;
        if (fitCircleTool == null)
        {
            message = "圆实例化错误";
            result = CogToolResultConstants.Error;
            return false;
        }
        foreach (CogPMAlignResult tool in pMAlignTool.Results)
        {
            fitCircleTool.Results.GetCircle().CenterX = tool.GetPose().TranslationX;
            fitCircleTool.Results.GetCircle().CenterY = tool.GetPose().TranslationY;
            //获取pma的 圆形ROI距离
            //fitCircleTool.Results.GetCircle().Radius = tool.GetPose().
            fitCircleTool.Run();
            CogGraphicLabel label1 = new CogGraphicLabel();

            label1.SetXYText(tool.GetPose().TranslationX, tool.GetPose().TranslationY, fitCircleTool.Results.GetCircle().Radius + "");
            lable.Add(label1);


        }


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
        foreach (CogGraphicLabel x in lable)
        {
            mToolBlock.AddGraphicToRunRecord(x, lastRecord, "CogImageConvertTool1.InputImage", "");
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

