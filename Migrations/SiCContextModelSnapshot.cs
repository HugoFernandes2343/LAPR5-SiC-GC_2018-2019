﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiC.Models;

namespace SiC.Migrations
{
    [DbContext(typeof(SiCContext))]
    partial class SiCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SiC.Models.Catalog", b =>
                {
                    b.Property<int>("CatalogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatalogDescription");

                    b.Property<string>("CatalogName");

                    b.HasKey("CatalogId");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("SiC.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.Property<int?>("parentCategoryId");

                    b.HasKey("CategoryId");

                    b.HasIndex("parentCategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SiC.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("SiC.Models.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("aestheticParameter");

                    b.Property<string>("collectionName");

                    b.HasKey("CollectionId");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("SiC.Models.Combination", b =>
                {
                    b.Property<int>("CombinationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("containedProductProductId");

                    b.Property<int?>("containingProductProductId");

                    b.Property<bool>("required");

                    b.HasKey("CombinationId");

                    b.HasIndex("containedProductProductId");

                    b.HasIndex("containingProductProductId");

                    b.ToTable("Combination");
                });

            modelBuilder.Entity("SiC.Models.Dimension", b =>
                {
                    b.Property<int>("DimensionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepthMeasureId");

                    b.Property<int?>("HeightMeasureId");

                    b.Property<int?>("ProductId");

                    b.Property<int?>("WidthMeasureId");

                    b.HasKey("DimensionId");

                    b.HasIndex("DepthMeasureId");

                    b.HasIndex("HeightMeasureId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WidthMeasureId");

                    b.ToTable("Dimension");
                });

            modelBuilder.Entity("SiC.Models.Factory", b =>
                {
                    b.Property<int>("FactoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityId");

                    b.Property<string>("Description");

                    b.HasKey("FactoryId");

                    b.HasIndex("CityId");

                    b.ToTable("Factory");
                });

            modelBuilder.Entity("SiC.Models.Finishing", b =>
                {
                    b.Property<int>("FinishingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.HasKey("FinishingId");

                    b.ToTable("Finishing");
                });

            modelBuilder.Entity("SiC.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.HasKey("MaterialId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("SiC.Models.MaterialFinishing", b =>
                {
                    b.Property<int>("MaterialId");

                    b.Property<int>("FinishingId");

                    b.HasKey("MaterialId", "FinishingId");

                    b.HasIndex("FinishingId");

                    b.ToTable("MaterialFinishing");
                });

            modelBuilder.Entity("SiC.Models.Measure", b =>
                {
                    b.Property<int>("MeasureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Value");

                    b.Property<double>("ValueMax");

                    b.Property<bool>("isDiscrete");

                    b.HasKey("MeasureId");

                    b.ToTable("Measure");
                });

            modelBuilder.Entity("SiC.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OrderName");

                    b.Property<string>("address");

                    b.Property<double>("cost");

                    b.Property<DateTime>("date");

                    b.Property<string>("status");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("SiC.Models.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FinishingId");

                    b.Property<int?>("MaterialId");

                    b.Property<DateTime>("date");

                    b.Property<string>("designation");

                    b.Property<double>("price");

                    b.HasKey("PriceId");

                    b.HasIndex("FinishingId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Price");
                });

            modelBuilder.Entity("SiC.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatalogId");

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("CollectionId");

                    b.Property<int?>("OrderId");

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.HasKey("ProductId");

                    b.HasIndex("CatalogId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CollectionId");

                    b.HasIndex("OrderId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SiC.Models.ProductMaterial", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("MaterialId");

                    b.HasKey("ProductId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("ProductMaterial");
                });

            modelBuilder.Entity("SiC.Models.Restriction", b =>
                {
                    b.Property<int>("RestrictionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CombinationId");

                    b.Property<string>("description");

                    b.Property<string>("type")
                        .IsRequired();

                    b.HasKey("RestrictionId");

                    b.HasIndex("CombinationId");

                    b.ToTable("Restriction");

                    b.HasDiscriminator<string>("type").HasValue("Restriction");
                });

            modelBuilder.Entity("SiC.Models.RestrictionDim", b =>
                {
                    b.HasBaseType("SiC.Models.Restriction");

                    b.Property<float>("x");

                    b.Property<float>("y");

                    b.Property<float>("z");

                    b.ToTable("RestrictionDim");

                    b.HasDiscriminator().HasValue("RDIM");
                });

            modelBuilder.Entity("SiC.Models.RestrictionMat", b =>
                {
                    b.HasBaseType("SiC.Models.Restriction");

                    b.Property<int?>("containedMaterialMaterialId");

                    b.Property<int?>("containingMaterialMaterialId");

                    b.HasIndex("containedMaterialMaterialId");

                    b.HasIndex("containingMaterialMaterialId");

                    b.ToTable("RestrictionMat");

                    b.HasDiscriminator().HasValue("RMAT");
                });

            modelBuilder.Entity("SiC.Models.Category", b =>
                {
                    b.HasOne("SiC.Models.Category", "parent")
                        .WithMany()
                        .HasForeignKey("parentCategoryId");
                });

            modelBuilder.Entity("SiC.Models.Combination", b =>
                {
                    b.HasOne("SiC.Models.Product", "containedProduct")
                        .WithMany()
                        .HasForeignKey("containedProductProductId");

                    b.HasOne("SiC.Models.Product", "containingProduct")
                        .WithMany()
                        .HasForeignKey("containingProductProductId");
                });

            modelBuilder.Entity("SiC.Models.Dimension", b =>
                {
                    b.HasOne("SiC.Models.Measure", "Depth")
                        .WithMany()
                        .HasForeignKey("DepthMeasureId");

                    b.HasOne("SiC.Models.Measure", "Height")
                        .WithMany()
                        .HasForeignKey("HeightMeasureId");

                    b.HasOne("SiC.Models.Product")
                        .WithMany("dimensions")
                        .HasForeignKey("ProductId");

                    b.HasOne("SiC.Models.Measure", "Width")
                        .WithMany()
                        .HasForeignKey("WidthMeasureId");
                });

            modelBuilder.Entity("SiC.Models.Factory", b =>
                {
                    b.HasOne("SiC.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("SiC.Models.MaterialFinishing", b =>
                {
                    b.HasOne("SiC.Models.Finishing", "Finishing")
                        .WithMany("MaterialFinishings")
                        .HasForeignKey("FinishingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiC.Models.Material", "Material")
                        .WithMany("MaterialFinishings")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiC.Models.Price", b =>
                {
                    b.HasOne("SiC.Models.Finishing")
                        .WithMany("Prices")
                        .HasForeignKey("FinishingId");

                    b.HasOne("SiC.Models.Material")
                        .WithMany("Prices")
                        .HasForeignKey("MaterialId");
                });

            modelBuilder.Entity("SiC.Models.Product", b =>
                {
                    b.HasOne("SiC.Models.Catalog")
                        .WithMany("Products")
                        .HasForeignKey("CatalogId");

                    b.HasOne("SiC.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("SiC.Models.Collection")
                        .WithMany("products")
                        .HasForeignKey("CollectionId");

                    b.HasOne("SiC.Models.Order")
                        .WithMany("orderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("SiC.Models.ProductMaterial", b =>
                {
                    b.HasOne("SiC.Models.Material", "Material")
                        .WithMany("ProductMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiC.Models.Product", "Product")
                        .WithMany("ProductMaterials")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiC.Models.Restriction", b =>
                {
                    b.HasOne("SiC.Models.Combination", "combination")
                        .WithMany()
                        .HasForeignKey("CombinationId");
                });

            modelBuilder.Entity("SiC.Models.RestrictionMat", b =>
                {
                    b.HasOne("SiC.Models.Material", "containedMaterial")
                        .WithMany()
                        .HasForeignKey("containedMaterialMaterialId");

                    b.HasOne("SiC.Models.Material", "containingMaterial")
                        .WithMany()
                        .HasForeignKey("containingMaterialMaterialId");
                });
#pragma warning restore 612, 618
        }
    }
}
