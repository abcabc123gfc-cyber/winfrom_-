using _02_Project.Model.UserManage;
using _02_Project.Model.BasicSetting;
using _02_Project.Model.Report;
using Microsoft.EntityFrameworkCore;

namespace _02_Project.DBContext
{
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 核心解决：配置 WorkReport 的外键，禁用级联删除
            // ==========================================

            // 1. 报工单 -> 工序 (Process)
            modelBuilder.Entity<WorkReport>()
                .HasOne(w => w.Process)
                .WithMany(p => p.WorkReports)
                .HasForeignKey(w => w.ProcessId)
                .OnDelete(DeleteBehavior.Restrict); // 禁止级联删除

            // 2. 报工单 -> 工价 (WageSetting)
            modelBuilder.Entity<WorkReport>()
                .HasOne(w => w.WageSetting)
                .WithMany(w => w.WorkReports)
                .HasForeignKey(w => w.WageSettingId)
                .OnDelete(DeleteBehavior.Restrict); // 禁止级联删除

            // 3. 报工单 -> 员工 (Employee)
            modelBuilder.Entity<WorkReport>()
                .HasOne(w => w.Employee)
                .WithMany()
                .HasForeignKey(w => w.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. 报工单 -> 工组 (WorkGroup)
            modelBuilder.Entity<WorkReport>()
                .HasOne(w => w.WorkGroup)
                .WithMany(wg => wg.WorkReports)
                .HasForeignKey(w => w.WorkGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 处理多对多连接表 (防止其他级联路径问题)
            // ==========================================

            // 报工单-员工 (WorkReportEmployee)
            modelBuilder.Entity<WorkReportEmployee>()
                .HasOne(wre => wre.WorkReport)
                .WithMany(wr => wr.Employees)
                .HasForeignKey(wre => wre.WorkReportId)
                .OnDelete(DeleteBehavior.Cascade); // 删报工单，关联明细一起删

            modelBuilder.Entity<WorkReportEmployee>()
                .HasOne(wre => wre.Employee)
                .WithMany(e => e.WorkReportEmployees)
                .HasForeignKey(wre => wre.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // 删员工，禁止级联删报工明细

            // 工序-工位 (ProcessWorkStation)
            modelBuilder.Entity<ProcessWorkStation>()
                .HasOne(pws => pws.Process)
                .WithMany(p => p.ProcessWorkStations)
                .HasForeignKey(pws => pws.ProcessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProcessWorkStation>()
                .HasOne(pws => pws.WorkStation)
                .WithMany(ws => ws.ProcessWorkStations)
                .HasForeignKey(pws => pws.WorkStationId)
                .OnDelete(DeleteBehavior.Restrict);

            // 工组-员工 (WorkGroupEmployee)
            modelBuilder.Entity<WorkGroupEmployee>()
                .HasOne(wge => wge.WorkGroup)
                .WithMany(wg => wg.Members)
                .HasForeignKey(wge => wge.WorkGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkGroupEmployee>()
                .HasOne(wge => wge.Employee)
                .WithMany(e => e.WorkGroupEmployees)
                .HasForeignKey(wge => wge.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            // ==========================================
            // 解决工位 (WorkStation) 的多重级联路径冲突
            // ==========================================

            // 车间 -> 工位：打断直接级联，避免报错
            modelBuilder.Entity<WorkStation>()
                .HasOne(ws => ws.Workshop)
                .WithMany(w => w.WorkStations)
                .HasForeignKey(ws => ws.WorkshopId)
                .OnDelete(DeleteBehavior.Restrict);

            // 生产线 -> 工位：保留级联（删除生产线时，底下的工位自动删除）
            modelBuilder.Entity<WorkStation>()
                .HasOne(ws => ws.ProductionLine)
                .WithMany(pl => pl.WorkStations)
                .HasForeignKey(ws => ws.ProductionLineId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        // 用户 管理
        public DbSet<User> Users => Set<User>();
        public DbSet<UserType> UserTypes => Set<UserType>();
        // ===== 基础设置 =====
        /// <summary>车间</summary>
        public DbSet<Workshop> Workshops => Set<Workshop>();

        /// <summary>生产线</summary>
        public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();

        /// <summary>工位</summary>
        public DbSet<WorkStation> WorkStations => Set<WorkStation>();

        /// <summary>工序</summary>
        public DbSet<Process> Processes => Set<Process>();

        /// <summary>工序-工位 连接表（子表单[工位]）</summary>
        public DbSet<ProcessWorkStation> ProcessWorkStations => Set<ProcessWorkStation>();

        /// <summary>员工</summary>
        public DbSet<Employee> Employees => Set<Employee>();

        /// <summary>工组</summary>
        public DbSet<WorkGroup> WorkGroups => Set<WorkGroup>();

        /// <summary>工组-员工 连接表（子表单[员工姓名]）</summary>
        public DbSet<WorkGroupEmployee> WorkGroupEmployees => Set<WorkGroupEmployee>();

        // ===== 工艺路线 =====
        /// <summary>工艺路线</summary>
        public DbSet<CraftRoute> CraftRoutes => Set<CraftRoute>();

        /// <summary>工艺路线明细（工艺流程）</summary>
        public DbSet<CraftRouteDetail> CraftRouteDetails => Set<CraftRouteDetail>();

        // ===== 工价管理 =====
        /// <summary>工价</summary>
        public DbSet<WageSetting> WageSettings => Set<WageSetting>();

        // ===== 报工管理 =====
        /// <summary>报工单</summary>
        public DbSet<WorkReport> WorkReports => Set<WorkReport>();

        /// <summary>报工单-员工 连接表（子表单[员工姓名]）</summary>
        public DbSet<WorkReportEmployee> WorkReportEmployees => Set<WorkReportEmployee>();
    }
}
