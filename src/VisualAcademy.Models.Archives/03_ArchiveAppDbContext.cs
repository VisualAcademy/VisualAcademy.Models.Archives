﻿using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace VisualAcademy.Models.Archives
{
    /// <summary>
    /// [3] DbContext Class
    /// </summary>
    public class ArchiveAppDbContext : DbContext
    {
        #region NuGet Packages
        // .NET 6 이상만 사용할 때에는 해당 버전 이상만 사용
        // PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.10
        // PM> Install-Package Microsoft.Data.SqlClient
        // PM> Install-Package System.Configuration.ConfigurationManager
        // --OR--
        //// PM> Install-Package Microsoft.EntityFrameworkCore
        //// PM> Install-Package Microsoft.EntityFrameworkCore.Tools
        //// PM> Install-Package Microsoft.EntityFrameworkCore.InArchivery 
        //// --OR--
        //// PM> Install-Package Microsoft.AspNetCore.All // 2.1 버전까지만 사용 가능 
        #endregion

        public ArchiveAppDbContext() : base()
        {
            // Empty
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public ArchiveAppDbContext(DbContextOptions<ArchiveAppDbContext> options)
            : base(options)
        {
            // 공식과 같은 코드, 교과서다운 코드
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// 참고 코드: 닷넷 프레임워크 또는 Windows Forms/WPF 기반에서 호출되는 코드 영역
        /// __App.config 또는 Web.config의 연결 문자열 사용
        /// __직접 데이터베이스 연결문자열 설정 가능
        /// __.NET Core 또는 .NET 5 이상에서는 사용하지 않음
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Archives 테이블의 Created, PostDate 열은 자동으로 GetDate() 제약 조건을 부여하기 
            modelBuilder.Entity<Archive>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Archive>().Property(m => m.PostDate).HasDefaultValueSql("GetDate()");
        }

        //[!] ArchiveApp 솔루션 관련 모든 테이블에 대한 참조 
        public DbSet<Archive> Archives { get; set; } = null!;
    }
}
