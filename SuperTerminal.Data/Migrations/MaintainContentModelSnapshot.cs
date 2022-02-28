﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperTerminal.Data.Maintain;

namespace SuperTerminal.Data.Migrations
{
    [DbContext(typeof(MaintainContent))]
    partial class MaintainContentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("SuperTerminal.Data.Entitys.TestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("主键");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasComment("创建时间");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasComment("是否已经删除");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasComment("名字");

                    b.Property<int>("Range")
                        .HasColumnType("int")
                        .HasComment("范围");

                    b.Property<string>("Tel")
                        .HasColumnType("longtext")
                        .HasComment("电话");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int")
                        .HasComment("更新人");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)")
                        .HasComment("更新时间");

                    b.HasKey("Id");

                    b.ToTable("TestModel");
                });

            modelBuilder.Entity("SuperTerminal.Data.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("主键");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasComment("创建时间");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasComment("是否已经删除");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext")
                        .HasComment("别名");

                    b.Property<int>("OSArchitecture")
                        .HasColumnType("int")
                        .HasComment("系统架构");

                    b.Property<string>("OSDescription")
                        .HasColumnType("longtext")
                        .HasComment("系统名称");

                    b.Property<int>("OSPlatform")
                        .HasColumnType("int")
                        .HasComment("系统类型");

                    b.Property<string>("PassWord")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("密码");

                    b.Property<string>("PrivIp")
                        .HasColumnType("longtext")
                        .HasComment("内网IP");

                    b.Property<string>("PubIp")
                        .HasColumnType("longtext")
                        .HasComment("公网IP");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int")
                        .HasComment("更新人");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)")
                        .HasComment("更新时间");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("用户名");

                    b.Property<int>("UserType")
                        .HasColumnType("int")
                        .HasComment("用户类型");

                    b.HasKey("Id");

                    b.ToTable("SysUser");

                    b
                        .HasComment("用户表");
                });
#pragma warning restore 612, 618
        }
    }
}
