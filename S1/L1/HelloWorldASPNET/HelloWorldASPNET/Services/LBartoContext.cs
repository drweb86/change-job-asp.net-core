using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HelloWorldASPNET.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloWorldASPNET.Services
{
    // dotnet ef migrations add v1
    // dotnet ef database update
    public class LBartoContext: DbContext
    {
        public LBartoContext(DbContextOptions<LBartoContext> options):
            base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEntityTypeConfiguration();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person2> Persons { get; set; }


    }

    public interface IEntityTypeConfiguration<T> where T : class
    {
        void Configure(EntityTypeBuilder<T> entityTypeBuilder);
    }

    public static class EntityTypeConfigurationExtensions
    {
        private static readonly MethodInfo entityMethod = typeof(ModelBuilder).GetTypeInfo().GetMethods().Single(x => (x.Name == "Entity") && (x.IsGenericMethod == true) && (x.GetParameters().Length == 0));

        private static Type FindEntityType(Type type)
        {
            var interfaceType = type.GetInterfaces().First(x => (x.GetTypeInfo().IsGenericType == true) && (x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            return interfaceType.GetGenericArguments().First();
        }

        private static readonly Dictionary<Assembly, IEnumerable<Type>> typesPerAssembly = new Dictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyConfiguration<T>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<T> configuration) where T : class
        {
            var entityType = FindEntityType(configuration.GetType());

            dynamic entityTypeBuilder = entityMethod
                .MakeGenericMethod(entityType)
                .Invoke(modelBuilder, new object[0]);

            configuration.Configure(entityTypeBuilder);

            return modelBuilder;
        }

        public static ModelBuilder UseEntityTypeConfiguration(this ModelBuilder modelBuilder)
        {
            IEnumerable<Type> configurationTypes;
            var asm = Assembly.GetEntryAssembly();

            if (typesPerAssembly.TryGetValue(asm, out configurationTypes) == false)
            {
                typesPerAssembly[asm] = configurationTypes = asm
                    .GetExportedTypes()
                    .Where(x => (x.GetTypeInfo().IsClass == true) && (x.GetTypeInfo().IsAbstract == false) && (x.GetInterfaces().Any(y => (y.GetTypeInfo().IsGenericType == true) && (y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))));
            }

            var configurations = configurationTypes.Select(x => Activator.CreateInstance(x));

            foreach (dynamic configuration in configurations)
            {
                ApplyConfiguration(modelBuilder, configuration);
            }

            return modelBuilder;
        }
    }

    public class MyEntityTypeConfiguration : IEntityTypeConfiguration<Person2>
    {
        public void Configure(EntityTypeBuilder<Person2> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Persons2");
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Id).HasColumnName("PersonId").ValueGeneratedOnAdd();

            entityTypeBuilder.Property(x => x.Age).HasColumnName("Age");
            entityTypeBuilder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(200);
        }
    }
}
