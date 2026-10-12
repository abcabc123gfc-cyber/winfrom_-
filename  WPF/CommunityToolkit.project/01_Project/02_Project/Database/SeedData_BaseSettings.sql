/* =====================================================================
 * 计件工资管理系统 - 基础设置初始数据（Seed Data）
 * 适用表：Workshop(车间) / ProductionLine(生产线) / WorkStation(工位)
 *        / Employee(员工) / WorkGroup(工组) / WorkGroupEmployee(工组-员工)
 *        / Process(工序) / ProcessWorkStation(工序-工位)
 * 幂等设计：各表已存在数据时自动跳过，脚本可重复执行。
 * 插入顺序遵循外键依赖：车间 -> 生产线 -> 工位 -> 员工 -> 工组 -> 工组-员工 -> 工序 -> 工序-工位
 * 级联说明（与 SQLContext 配置一致）：车间->生产线 Cascade；车间->工位 Restrict；
 *   生产线->工位 Cascade；工组->工组-员工 Cascade；工序->工序-工位 Cascade；
 *   工组-员工 的 Employee 侧、工序-工位 的 WorkStation 侧为 Restrict。
 * ===================================================================== */
SET NOCOUNT ON;

/* ---------- 1. 车间 ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Workshop)
BEGIN
    INSERT INTO dbo.Workshop (Name) VALUES
        (N'机加工车间'),
        (N'装配车间'),
        (N'冲压车间');
    PRINT N'[Seed] 已插入车间 3 条';
END

/* ---------- 2. 生产线（所属车间） ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.ProductionLine)
BEGIN
    INSERT INTO dbo.ProductionLine (Name, WorkshopId) VALUES
        (N'数控加工线', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'精密加工线', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'整机装配线', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'电气装配线', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'冲压成型线', (SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间')),
        (N'钣金冲压线', (SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'));
    PRINT N'[Seed] 已插入生产线 6 条';
END

/* ---------- 3. 工位（所属车间 + 所属生产线，二者必须一致） ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.WorkStation)
BEGIN
    INSERT INTO dbo.WorkStation (WorkshopId, ProductionLineId, Name, Remark) VALUES
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'数控加工线'), N'CNC-01 数控车工位', N'数控车床加工，三班运转'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'数控加工线'), N'CNC-02 数控铣工位', N'数控铣床加工，含倒角去毛刺'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'精密加工线'), N'JM-01 精密磨床工位', N'外圆磨/平面磨，公差 IT6'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'精密加工线'), N'JM-02 精镗工位', N'精镗孔，加工中心替代方案'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'整机装配线'), N'ZJ-01 主机装配工位', N'整机合装、紧固与联调'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'整机装配线'), N'ZJ-02 部件装配工位', N'分部件预装，含密封件安装'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'电气装配线'), N'DQ-01 电气布线工位', N'线束制作与布线、端子压接'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'电气装配线'), N'DQ-02 电气调试工位', N'通电测试、参数整定'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'冲压成型线'), N'CY-01 冲床工位', N'125T 冲床，连续模生产'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'冲压成型线'), N'CY-02 成型压机工位', N'400T 油压机，拉延成型'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'钣金冲压线'), N'BJ-01 折弯工位', N'数控折弯机，板材折弯'),
        ((SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'), (SELECT Id FROM dbo.ProductionLine WHERE Name = N'钣金冲压线'), N'BJ-02 点焊工位', N'电阻点焊，钣金拼接');
    PRINT N'[Seed] 已插入工位 12 条';
END

/* ---------- 4. 员工（工号唯一，所属车间） ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Employee)
BEGIN
    INSERT INTO dbo.Employee (Name, EmployeeNo, WorkshopId) VALUES
        (N'张伟', N'E2026001', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'李强', N'E2026002', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'王磊', N'E2026003', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'赵刚', N'E2026004', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'刘洋', N'E2026005', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'陈浩', N'E2026006', (SELECT Id FROM dbo.Workshop WHERE Name = N'机加工车间')),
        (N'周明', N'E2026007', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'吴军', N'E2026008', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'郑凯', N'E2026009', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'孙鹏', N'E2026010', (SELECT Id FROM dbo.Workshop WHERE Name = N'装配车间')),
        (N'钱进', N'E2026011', (SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间')),
        (N'冯超', N'E2026012', (SELECT Id FROM dbo.Workshop WHERE Name = N'冲压车间'));
    PRINT N'[Seed] 已插入员工 12 条';
END

/* ---------- 5. 工组 ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.WorkGroup)
BEGIN
    INSERT INTO dbo.WorkGroup (Name) VALUES
        (N'数控班组'),
        (N'精加工班组'),
        (N'装配一班'),
        (N'装配二班');
    PRINT N'[Seed] 已插入工组 4 条';
END

/* ---------- 6. 工组-员工（子表单[员工姓名]，多对多） ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.WorkGroupEmployee)
BEGIN
    INSERT INTO dbo.WorkGroupEmployee (WorkGroupId, EmployeeId) VALUES
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'数控班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026001')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'数控班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026002')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'数控班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026003')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'精加工班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026004')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'精加工班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026005')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'精加工班组'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026006')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配一班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026007')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配一班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026008')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配一班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026009')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配二班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026010')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配二班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026011')),
        ((SELECT Id FROM dbo.WorkGroup WHERE Name = N'装配二班'), (SELECT Id FROM dbo.Employee WHERE EmployeeNo = N'E2026012'));
    PRINT N'[Seed] 已插入工组-员工 12 条';
END

/* ---------- 7. 工序 ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Process)
BEGIN
    INSERT INTO dbo.Process (Name, Remark) VALUES
        (N'数控车削', N'粗加工，去除大部分余量'),
        (N'数控铣削', N'半精加工，含键槽与平面'),
        (N'精密磨削', N'精加工，控制尺寸公差'),
        (N'装配作业', N'部件预装与整机合装'),
        (N'电气调试', N'通电测试与参数整定'),
        (N'冲压成型', N'板材下料、冲压与折弯成型');
    PRINT N'[Seed] 已插入工序 6 条';
END

/* ---------- 8. 工序-工位（子表单[工位]，多对多） ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.ProcessWorkStation)
BEGIN
    INSERT INTO dbo.ProcessWorkStation (ProcessId, WorkStationId) VALUES
        ((SELECT Id FROM dbo.Process WHERE Name = N'数控车削'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'CNC-01 数控车工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'数控铣削'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'CNC-02 数控铣工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'精密磨削'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'JM-01 精密磨床工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'装配作业'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'ZJ-01 主机装配工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'装配作业'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'ZJ-02 部件装配工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'电气调试'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'DQ-01 电气布线工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'电气调试'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'DQ-02 电气调试工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'冲压成型'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'CY-01 冲床工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'冲压成型'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'CY-02 成型压机工位')),
        ((SELECT Id FROM dbo.Process WHERE Name = N'冲压成型'), (SELECT Id FROM dbo.WorkStation WHERE Name = N'BJ-01 折弯工位'));
    PRINT N'[Seed] 已插入工序-工位 10 条';
END

PRINT N'[Seed] 基础设置初始数据执行完成。';