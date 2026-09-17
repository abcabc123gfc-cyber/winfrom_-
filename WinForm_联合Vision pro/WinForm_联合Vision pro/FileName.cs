#region namespace imports
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using System.Collections.Generic;
#endregion

public class CogToolBlockAdvancedScript : CogToolBlockAdvancedScriptBase
{
    #region Private Member Variables
    private Cognex.VisionPro.ToolBlock.CogToolBlock mToolBlock;
    /// <summary>
    /// 声明list标签
    /// </summary>
    List<CogGraphicLabel> lables;
    /// <summary>
    /// 声明仿射矩形 切图
    /// </summary>
    CogAffineTransformTool cogAffineTransformTool;
    /// <summary>
    /// 声明pma
    /// </summary>
    CogPMAlignTool pMAlignTool;
    /// <summary>
    /// 声明圆
    /// </summary>
    CogFindCircleTool fitCircleTool;
    /// <summary>
    /// 声明测量 点->圆距离
    /// </summary>
    CogDistancePointCircleTool cogDistancePointCircleTool;
    /// <summary>
    /// 测量
    /// </summary>
    CogFixtureTool CogFixtureTool1;
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

        #region 初始化
        lables = new List<CogGraphicLabel>();
        #endregion
        // Run each tool using the RunTool function
        foreach (ICogTool tool in mToolBlock.Tools)
            mToolBlock.RunTool(tool, ref message, ref result);

        #region 实例化工具
        pMAlignTool = mToolBlock.Tools["正面_硬币"] as CogPMAlignTool;

        fitCircleTool = mToolBlock.Tools["CogFindCircleTool1"] as CogFindCircleTool;
        //仿射
        cogAffineTransformTool = mToolBlock.Tools["CogAffineTransformTool1"] as CogAffineTransformTool;
        //距离
        cogDistancePointCircleTool = mToolBlock.Tools["CogDistancePointCircleTool1"] as CogDistancePointCircleTool;
        //测量
        CogFixtureTool1 = mToolBlock.Tools["CogFixtureTool1"] as CogFixtureTool;
        #endregion
        if (fitCircleTool == null)
        {
            message = "圆实例化错误";
            result = CogToolResultConstants.Error;
            return false;
        }
        int i = 0;
        foreach (CogPMAlignResult tool in pMAlignTool.Results)
        {
            i ++;
            //切图
            //cogAffineTransformTool.InputImage = pMAlignTool.InputImage;
            //获取pma 的坐标 每一个
            ICogTransform2D pose = tool.GetPose();
            //创建坐标系
            CogFixtureTool1.RunParams.UnfixturedFromFixturedTransform = pose;
            CogFixtureTool1.Run();
            //导入坐标系
            ICogTransform2D cogTransform2DLinear = CogFixtureTool1.InputImage.PixelFromRootTransform;
            cogAffineTransformTool.InputImage.PixelFromRootTransform = cogTransform2DLinear;

            //cogAffineTransformTool.Region.CenterX = 0; // 在 Fixture 坐标系下，目标中心通常是(0,0)
            //cogAffineTransformTool.Region.CenterY = 0; 
            cogAffineTransformTool.Region.SideXLength = 400;
            cogAffineTransformTool.Region.SideYLength = 400;
            cogAffineTransformTool.Run();
            ICogImage image = cogAffineTransformTool.OutputImage;
            #region 找圆
            fitCircleTool.InputImage = image as CogImage8Grey;
            //fitCircleTool.InputImage.PixelFromRootTransform = cogTransform2DLinear;
            fitCircleTool.Results.GetCircle().CenterX = image.Width/2;
            fitCircleTool.Results.GetCircle().CenterY = image.Height/2;
            //fitCircleTool.Results.GetCircle().CenterX = 0;
            //fitCircleTool.Results.GetCircle().CenterY = 0;
            fitCircleTool.Run();
            #endregion

            #region 输出距离
            //cogDistancePointCircleTool.InputImage = image;
            //cogDistancePointCircleTool.InputImage.PixelFromRootTransform = cogTransform2DLinear;

            //cogDistancePointCircleTool.X = fitCircleTool.Results.GetCircle().CenterX;
            //cogDistancePointCircleTool.Y = fitCircleTool.Results.GetCircle().CenterY;
            cogDistancePointCircleTool.X = tool.GetPose().TranslationX;
            cogDistancePointCircleTool.Y = tool.GetPose().TranslationY;
            cogDistancePointCircleTool.InputCircle = fitCircleTool.Results.GetCircle();
            cogDistancePointCircleTool.Run();
            #endregion
            ////获取pma的 圆形ROI距离
            //CogGraphicLabel label1 = new CogGraphicLabel();
            string radius = (-(cogDistancePointCircleTool.Distance*2)) .ToString("f2");
            //string radius = fitCircleTool.Results.GetCircle().Radius .ToString("f2");
            CogGraphicLabel label = new CogGraphicLabel();
            label.SetXYText(tool.GetPose().TranslationX, tool.GetPose().TranslationY, radius);
            lables.Add(label);

            //if (i == 2)
            //{
            //    return false;
            //}
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
        foreach (CogGraphicLabel x in lables)
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

