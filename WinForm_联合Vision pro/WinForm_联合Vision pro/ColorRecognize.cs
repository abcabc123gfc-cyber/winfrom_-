#region namespace imports
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ColorMatch;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using System.Drawing;
//using System.Windows;
#endregion

public class CogToolBlockAdvancedScript_2 : CogToolBlockAdvancedScriptBase
{
    #region Private Member Variables
    private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
    private CogColorMatchTool cogColorMatchTool;
    private CogGraphicLabel label;
    CogPMAlignMultiTool cogPMAlignMultiTool;
    CogFixtureTool cogFixtureTool1;

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

        label = new CogGraphicLabel();
        label.Text = "";
        label.Font = new Font("Arial", 20);
        // Run each tool using the RunTool function
        try
        {
            foreach (ICogTool tool in mToolBlock.Tools)
                mToolBlock.RunTool(tool, ref message, ref result);

            cogPMAlignMultiTool = mToolBlock.Tools["CogPMAlignMultiTool1"] as CogPMAlignMultiTool;
            cogColorMatchTool = mToolBlock.Tools["CogColorMatchTool1"] as CogColorMatchTool;
            cogFixtureTool1 = mToolBlock.Tools["CogFixtureTool1"] as CogFixtureTool;
            cogColorMatchTool.Run();

            if (cogFixtureTool1.InputImage == null || cogPMAlignMultiTool.Results.PMAlignResults == null || cogPMAlignMultiTool.RunStatus.Result == CogToolResultConstants.Error || cogPMAlignMultiTool.Results.PMAlignResults.Count == 0)
            {
                label.SetXYText(100, 100, "Color Match Result: No Match");
                return false;
            }
            else if (cogColorMatchTool.Result.ResultOfBestMatch.Color.Name != "")
            {
                label.SetXYText(100, 100, "Color Match Result: " + cogColorMatchTool.Result.ResultOfBestMatch.Color.Name);
            }
        }
        catch (System.Exception)
        {
            label.SetXYText(100, 100, "Color Match Result: No Match");
            return false;
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
        mToolBlock.AddGraphicToRunRecord(label, lastRecord, "CogImageConvertTool1.InputImage", "");
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

