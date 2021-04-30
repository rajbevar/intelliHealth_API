﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientEngTranscription.DataAccess;

namespace PatientEngTranscription.DataAccess.Migrations
{
    [DbContext(typeof(PatientEngTranscriptionContext))]
    [Migration("20191202054814_addedProblemTable")]
    partial class addedProblemTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PatientEngTranscription.Domain.DbEntities.Problems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IsProblem");

                    b.Property<string>("Name");

                    b.Property<DateTime>("RecordedDate");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("PatientEngTranscription.Domain.Medication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Doage");

                    b.Property<string>("Frenquency");

                    b.Property<string>("Name");

                    b.Property<string>("Strength");

                    b.HasKey("Id");

                    b.ToTable("Medication");
                });

            modelBuilder.Entity("PatientEngTranscription.Domain.Notes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDateTime");

                    b.Property<Guid>("DoctorId");

                    b.Property<string>("Note");

                    b.Property<bool>("isIgnored");

                    b.Property<bool>("isParsed");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PatientEngTranscription.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City");

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("ExternalId");

                    b.Property<string>("Firstname");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("State");

                    b.Property<string>("Zipcode");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientEngTranscription.Domain.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<Guid>("RecogProfileId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
