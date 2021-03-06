// <auto-generated />
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
    [Migration("20191114054415_MedicationAdded")]
    partial class MedicationAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PatientEngTranscription.Domain.Medication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Doage");

                    b.Property<string>("Duration");

                    b.Property<string>("Name");

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

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PatientEngTranscription.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<string>("PhoneNumber");

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
